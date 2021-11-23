using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalaEnemyRightController : MonoBehaviour
{
    public float velocityX = 30f;
    
    private Rigidbody2D rb;

    private const string TAG_PLAYER = "Player";

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Destroy(gameObject, 3);
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = Vector2.right * velocityX;
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == TAG_PLAYER)
        {
            Destroy(this.gameObject);
            rb.velocity = Vector2.right * velocityX;
        }  
    }
}
