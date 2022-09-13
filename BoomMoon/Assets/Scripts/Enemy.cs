using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Animator animator;

    public int maxHealth = 100;
    int currentHealth;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        animator.SetTrigger("Hurt");

        //Animación de daño

        if (currentHealth <= 0)
        {
            Die();
            currentHealth = 100;
        }
    }

    void Die()
    {
        Debug.Log("Enemigo muerto");

        //Animación de muerte

        animator.SetBool("isDead", true);

        //Desabilitar al enemigo

        GetComponent<Collider2D>().enabled = false;
        this.enabled = false;
        Destroy(this, 3);
        PlatformPlayerController.instance.updateKilledEnemies();
    }
}
