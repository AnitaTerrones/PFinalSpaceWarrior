using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemigoTwoController : MonoBehaviour
{
    private float vidaEnemyTwo; 

    private float tiempo;
    private float tiempo2; 
    private float tiempo3;  
    public float distanciaJugador;
    public float rangoVision;

    public float velocityY = 5;
    private bool isDead = false;
    public GameObject balaEnemigo;
    public GameObject balaEnemigoVariant;
    public Transform player;
    private const string TAG_ENEMY_PLAYER = "balaPlayer";
    private const int ANIMATION_DESTROY = 1; 
    private Rigidbody2D rb;
    private SpriteRenderer sr;
    private Animator animator;

    private AudioSource audioSource;

    //SONIDOS
    public List<AudioClip> Sonido;
    private ScoreController game;

    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>(); 
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        game = FindObjectOfType<ScoreController>();

    }

    // Update is called once per frame
    void Update()
    {
        distanciaJugador = Vector2.Distance(player.position, rb.position);
        if (distanciaJugador < rangoVision )
        {
            if (sr.flipX == false)
            {
                tiempo += Time.deltaTime;
                if (tiempo >= 1)
                {
                    var position = new Vector2(transform.position.x +1, transform.position.y+6);
                    var rotation = balaEnemigo.transform.rotation;
                    Instantiate(balaEnemigo, position, rotation);
                    var positiona= new Vector2(transform.position.x+1, transform.position.y-1);
                    var rotationa = balaEnemigo.transform.rotation;
                    Instantiate(balaEnemigo, positiona, rotationa);
                    tiempo = 0;
                }
                tiempo2 += Time.deltaTime; 
                if (tiempo2 >= 3)
                {
                    var position = new Vector2(transform.position.x +1, transform.position.y+2);
                    var rotation = balaEnemigoVariant.transform.rotation;
                    Instantiate(balaEnemigoVariant, position, rotation);
                    tiempo2 = 0;
                } 
            } 
        }

        if (isDead == true)
        {
            changeAnimation(ANIMATION_DESTROY);
            tiempo3 += Time.deltaTime;
            if (tiempo3 >= 1)
            {
                Destroy(this.gameObject);
                tiempo3 = 0;
            }
            audioSource.PlayOneShot(Sonido[0]);
        }
        
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == TAG_ENEMY_PLAYER)
        {
            vidaEnemyTwo = vidaEnemyTwo + 1;
            Debug.Log("vida enemigo: " + vidaEnemyTwo);
            Destroy(other.gameObject);
            if (vidaEnemyTwo == 40)
            {
                isDead = true;
                game.SumarScore(50);

            }
        }  
    }

    private void changeAnimation(int animation)
    {
        animator.SetInteger("Estado", animation);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, rangoVision);
        
    }
}
