using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public float velocityX = 8;
    public float velocityY = 8;
    private int contVidas = 0;
    private bool isDead = false;
    private float tiempo;
    
    public Slider vidaSlider;

    //public GameObject gameOver;   

    public GameObject bala;
    private const string TAG_BALAENEMY = "balaEnemy";
    
    private SpriteRenderer sr;
    private Rigidbody2D rb;
    private Animator animator;
    private AudioSource audioSource;

    
    private const int ANIMATION_IDLE = 0;
    private const int ANIMATION_SHOOT = 1;
    private const int ANIMATION_DEAD= 2;

    //para cuando se electrocute
    private bool electrocutado = false;
    private float tiempoHerido = 0f;

    //SONIDOS
    public List<AudioClip> audioClips;
    
    
    private const int ELECTRICIDAD = 7;
    private const int LAYER_BORDE = 8;


    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
     
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = new Vector2(0, 0);
        changeAnimation(ANIMATION_IDLE);

        //Movimientos Player
        if (Input.GetKey(KeyCode.RightArrow))
        {
            rb.velocity = new Vector2(velocityX, rb.velocity.y);
            sr.flipX = false;
        }

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            rb.velocity = new Vector2(-velocityX, rb.velocity.y);
        }

        if (Input.GetKey(KeyCode.UpArrow))
        {
            rb.velocity = new Vector2(rb.velocity.x, velocityY);
        }
         
        if (Input.GetKey(KeyCode.DownArrow))
        {
            rb.velocity = new Vector2(rb.velocity.x, -velocityY);
        }

        //disparo
        if (Input.GetKeyUp(KeyCode.Z) && (sr.flipX == false))
        {
            changeAnimation(ANIMATION_SHOOT);
            var position = new Vector2(transform.position.x + 2, transform.position.y);
            var rotation = bala.transform.rotation;
            Instantiate(bala, position, rotation);
            audioSource.PlayOneShot(audioClips[0]);
        }

        //parpadea por electrocutarse
        if (electrocutado && tiempoHerido < 3f)
        {
            tiempoHerido += Time.deltaTime;
            sr.enabled = !sr.enabled;
        }
        //regresa a su estado normal
        else if (electrocutado && tiempoHerido >= 3f)
        {
            electrocutado = false;
            sr.enabled = true;
            tiempoHerido = 0f;
        }

        if (isDead == true)
        {
            changeAnimation(ANIMATION_DEAD);
            tiempo += Time.deltaTime;
            if (tiempo >= 2)
            {
                Destroy(this.gameObject);
                tiempo = 0;
                GameOptionsController.mostrarGameOver();
            }
            audioSource.PlayOneShot(audioClips[1]);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == TAG_BALAENEMY)
        {
            contVidas = contVidas + 1;
            vidaSlider.value -= 10f;
            Debug.Log("cont: "+ contVidas);
            if (contVidas == 10)
            {
                isDead = true;
            }
        }
        //Ir a nivel 1
        if(other.gameObject.name =="Level1")
        {
            Debug.Log("Ingresando a Nivel 01");
            SceneManager.LoadScene(2);//Nivel 01

        }
        //Ir a nivel 2
        if(other.gameObject.name =="Level2")
        {
            SceneManager.LoadScene(3);//Nivel 02
        }
        //Ir a nivel 3
        if(other.gameObject.name =="Level3")
        {
            SceneManager.LoadScene(4);//Nivel 03
        }

        //termina el nivel
        if(other.gameObject.name =="Salida")
        {
            SceneManager.LoadScene(1);//Mapa de niveles
        }

        //se electrocuta
        if (other.gameObject.layer == ELECTRICIDAD && !electrocutado)
        {
            Debug.Log("heridoo");
            electrocutado= true;
            audioSource.PlayOneShot(audioClips[2]);
            vidaSlider.value -= 5f;
        }

        //colisiona con el borde y muere
        if (other.gameObject.layer ==LAYER_BORDE)
        {
            Debug.Log("muriendoo");
            changeAnimation(ANIMATION_DEAD);
            isDead = true;
            audioSource.PlayOneShot(audioClips[1]);
        }
    }
    
    private void changeAnimation(int animation)
    {
        animator.SetInteger("Estado", animation);
    }
}
