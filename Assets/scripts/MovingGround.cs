using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingGround : MonoBehaviour
{
    public float Speed;
    private Rigidbody2D rb ;
    private Vector2 platformVelocity;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>(); 
        platformVelocity = rb.linearVelocity;
    }

    private void Update()
    {
        if (transform.position.x < 179)
        {
            Speed = 0;
        
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player" )
        {
           rb.linearVelocity = Vector3.right * Speed*Time.deltaTime;
            
        }

       
    }

}
