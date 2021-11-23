using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AlienController : MonoBehaviour
{
    
    public float velocityX = 5;

    private float tiempo;
    public float velocidad;

    private bool isDead = false;
    
    public float distanciaJugador;
    public float rangoVision;
    
    public bool mirandoDerecha;
   
    public GameObject balaAlien;
    public GameObject balaAlienRight; 
    public Transform player;
    private const string TAG_ENEMY_PLAYER = "balaPlayer";
    public Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        
        if (mirandoDerecha == true)
        {
            rb.velocity = new Vector2(velocityX, rb.velocity.y); 
            tiempo += Time.deltaTime;
            if (tiempo >= 1)
            {
                var position = new Vector2(transform.position.x , transform.position.y);
                var rotation = balaAlienRight.transform.rotation;
                Instantiate(balaAlienRight, position, rotation);
                tiempo = 0;
            }
        }
        if (mirandoDerecha == false)
        {
            rb.velocity = new Vector2(-velocityX, rb.velocity.y);
            tiempo += Time.deltaTime;
            if (tiempo >= 1)
            {
                var position = new Vector2(transform.position.x , transform.position.y);
                var rotation = balaAlien.transform.rotation;
                Instantiate(balaAlien, position, rotation);
                tiempo = 0;
            }
        } 
        
        MirarPlayer();
        distanciaJugador = Vector2.Distance(player.position, rb.position);
        if (distanciaJugador < rangoVision )
        {
            Vector2 objetivo = new Vector2(player.position.x, player.position.y);
            Vector2 nuevaPos = Vector2.MoveTowards(rb.position, objetivo, velocidad * Time.deltaTime);
            rb.MovePosition(nuevaPos);
            
        }
    }

    public void MirarPlayer()
    {
        Vector2 girar = transform.localScale;
        if (distanciaJugador < rangoVision)
        {
            if (transform.position.x < player.position.x && !mirandoDerecha)
            {
                Girar();
                mirandoDerecha = true;
            }
            else if (transform.position.x > player.position.x && mirandoDerecha)
            {
                Girar();
                mirandoDerecha = false;
            } 
        }
    }

    public void Girar()
    {
        transform.Rotate(0,180,0);
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, rangoVision);
        
    }
}
