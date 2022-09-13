using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
public class PlatformPlayerController : MonoBehaviour
{
    [Header("Objetos de Personaje")]
    private Rigidbody2D playerRb;
    private Animator animator;
    public Text killedEnemiesText;

    [Header("Parametros de Personaje")]
    [Range(0f, 100f)]
    public float moveSpeed;   //velocidad de movimiento
    [Range(0f, 120f)]
    public float maxSpeed;   //Velocidad maxima
    [Range(0f, 200f)]
    public float jumpForce;   //Fuerza de salto

    public bool isGrounded;
    public bool isMoving;
    public bool isPunching;
    public bool isCrouching;
    public int killedEnemies;

    public bool jump = false;
    public float ySpeed;
    public float xSpeed;

    public static PlatformPlayerController instance;
    // Start is called before the first frame update
    void Start()
    {
        //GameObject.Find("GameManager(Clone)").GetComponent<GameObject>().SetActive(false);
        instance = this;
        playerRb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        animator.SetBool("grounded", isGrounded);
        animator.SetFloat("yVelocity", playerRb.velocity.y);
        animator.SetFloat("xVelocity", Mathf.Abs(playerRb.velocity.x));
        animator.SetBool("moving", isMoving);
        animator.SetBool("crouching", isCrouching);
        animator.SetBool("punch", isPunching);

        ySpeed = playerRb.velocity.y;
        xSpeed = playerRb.velocity.x;

        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (isGrounded)
            {
                jump = true;
            }
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            isPunching = true;
        }
        if (Input.GetKeyDown(KeyCode.S) && !isMoving)
        {
            isCrouching = true;
        }
    }

    void FixedUpdate()
    {
        Vector3 fixedVelocity = playerRb.velocity;
        fixedVelocity *= 0.93f;

        float Force = Input.GetAxis("Horizontal");

        if (xSpeed > 0 || xSpeed < 0)
        {
            isMoving = true;
        } else
        {
            isMoving = false;
        }

        if (Force > 0.1)
        {
            transform.localScale = new Vector3(1f, 1f, 1f);  //Volteamos a la derecha
        }
        if (Force < -0.1)
        {
            transform.localScale = new Vector3(-1f, 1f, 1f); //Volteamos a la izquierda
        }

        playerRb.AddForce(Vector2.right * moveSpeed * Force);

        float limitedSpeedX = Mathf.Clamp(fixedVelocity.x, -maxSpeed, maxSpeed);
        float limitedSpeedY = Mathf.Clamp(fixedVelocity.y, -maxSpeed, maxSpeed);
        playerRb.velocity = new Vector2(limitedSpeedX, limitedSpeedY);

        if (jump)
        {
            playerRb.velocity = new Vector2(limitedSpeedX, limitedSpeedY);
            playerRb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            jump = false;
        }
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Platform")
        {
            isGrounded = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Platform")
        {
            isGrounded = false;
        }
    }
    public void updateKilledEnemies()
    {
        killedEnemies+=1;
        killedEnemiesText.text = killedEnemies.ToString();
    }
}
