using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Personaje : MonoBehaviour
{
    public float velocidad;
    public float dummy;
    public float shotsReceived;
    //Cuando expongamos un objeto
    //Que esperemos nos manden del editor
    //Hay posibilidad que sea nulo
    public Text text;
    public Text counterText;
    // Ciclo de vida
    // Existen metodos que se ejecutan en momentos especificos en la vida de un script

    // idioms - Estandares de esritura en lenguaje

    // Vamos a empezar a clonar (instanciar) objetos
    // Es clon y necesitamos un original
    public GameObject balaOriginal;

    // Se ejecuta al inicio de la vida del script
    void Awake() 
    {
        Debug.Log("Hello AWAKE");
    }

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Hello START");
    }

    // Update is called once per frame
    // Corre cada loop (procesar logica, desplegar graficos)
    // Depende de la combinacion de hardware y software, es la frecuencia de ejecucion
    // Realtime al menos 30fps (cuadros por segundo)
    // deseable 60 fps+
    void Update()
    {
        // Aqui queremos que:
        // El juego se sienta responsivo
        //Transform referencia al componente en el mismo GameObject
        // Transform nombre de la clase que define al componente
        // Las operaciones especiales (translacion, escala, etc): movimiento suave
        //transform.Translate(1f*Time.deltaTime, vertical, 0);
        //Importante recordar manejo de cambios de frecuencia
        //Time.deltaTime cantidad del tiempo en segundos que 
        //transcurrio entre el cuadro anterior y el actual

        // manejo de entrada
        // polling vs events, lo mejor es polling (nosotros preguntarle)
        if (Input.GetKeyDown(KeyCode.Space)){
            //Detona cuando el cuadro anterior esta libre o el 
            //cuadro actual está presionado
            print("Acabas de presionar el cuadro");
            //Instantiate(balaOriginal, transform.position, transform.rotation);
        };
        if(Input.GetKey(KeyCode.Space)){
            //Detona cuando el cuadro anterior esta libre o el 
            //cuadro actual está presionado  
            print("Suelta al cuadro");

            //Instantiate(balaOriginal, new Vector3(transform.position.x, transform.position.y-5, transform.position.z) , transform.rotation);
            Instantiate(balaOriginal);
        }
        if(Input.GetKeyUp(KeyCode.Space)){
            //Detona cuando el cuadro anterior esta libre o el 
            //cuadro actual está presionado
            print("Ya lo soltaste");
        };
        // Valores en el rango -1,1, los ejes sirven para abstraer el input
        //Estos valores tienen una desaceleracion 
        float horizontal = Input.GetAxis("Horizontal");
        // float horizontal = Input.GetAxisRaw("Horizontal"); con esto se toma sin desaceleracion
        float vertical = Input.GetAxis("Vertical");
        // float vertical = Input.GetAxisRaw("Vertical"); con esto se toma sin desaceleracion

        transform.Translate(
            velocidad * horizontal * Time.deltaTime, 
            velocidad * vertical * Time.deltaTime, 
            0,
            Space.World);



        text.fontSize = 18;
        text.fontStyle = FontStyle.Italic;
        text.text = (1.0f / Time.deltaTime).ToString();
        //this.GetComponent<BoxCollider>().

        //Cuando un involucrado en la colision es trigger
        // el engine de de la fisica ya no lo toma en cuenta para el movimiento
    }

    /* Late update corre cada frame, pero despues 
    de TODOS los updates */
    void LateUpdate() 
    {
 
    }

    /* Es un update arreglado,
    más bien fijo */
    void FixedUpdate() 
    {

    }

    //Deteccion de colisiones (Con motor de fisica)
    //1.- Todos los involucrados tienen componente collider
    //2.- Al menos uno de ellos tiene rigidbody (Y se debe mover)
    // Hay 3 momentos en la vida de la colision
    // OnCollisionEnter
    // OnCollisionStay
    // OnCollisionExit
    void OnCollisionEnter(Collision c)
    {
        //Collision tiene ifo detallada de la colision
        print("OnCollisionEnter");
        print("ENTER " + c.contacts[0]);
        print("Transform" + c.transform.name);
        print("" + c.transform.tag);
        print("" + c.gameObject.layer);
        if(c.transform.tag.Equals("bala"))
        {
            c.gameObject.SetActive(false);
            shotsReceived++;
            counterText.text=shotsReceived+"";
        }
    }
    void OnCollisionStay(Collision c)
    {
        print("OnCollisionStay");
    }
    void OnCollisionExit(Collision c)
    {
        print("OnCollisionExit");
    }

    void OnTriggerEnter(Collider c)
    {
        print("OnTriggerEnter" + c.transform.name);
        print("" + c.transform.tag);
        print("" + c.gameObject.layer);
    }

    void OnTriggerStay(Collider c)
    {
        print("OnTriggerStay");
    }

    void OnTriggerExit(Collider c)
    {
        print("OnTriggerExit");
    }


}
