using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class BalaController : MonoBehaviour
{
   
    public float velocityX = 30f;
    private Rigidbody2D rb;
    private ScoreController game;

    private const string TAG_ENEMY = "Enemy";
    private const string TAG_ENEMYFinal = "EnemyFinal";

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        game = FindObjectOfType<ScoreController>();
        Destroy(gameObject, 2);
        
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = Vector2.right * velocityX;
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == TAG_ENEMY)
        {
            Destroy(this.gameObject);
            Destroy(other.gameObject);
            game.SumarScore(5);
            rb.velocity = Vector2.right * velocityX;
        }  
    }
}
