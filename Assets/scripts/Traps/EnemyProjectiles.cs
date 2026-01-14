using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectiles : EnemyDamage
{
    [SerializeField] private float speed;
    [SerializeField] private float resetTime;

    private float lifeTime;
    private Animator anim;
    private bool hit;
    private BoxCollider2D coll;
    private void Awake()
    {
        anim = GetComponent<Animator>();
        coll = GetComponent<BoxCollider2D>();
    }
    public void ActivateProjectiles()
    {
        hit = false;
        lifeTime = 0;

          gameObject.SetActive(true);
        coll.enabled = true;
    }

    private void Update()
    {
        if (hit)  return;
        float movementSpeeed = speed * Time.deltaTime;
        transform.Translate(movementSpeeed,0,0);

        lifeTime += Time.deltaTime;
        if(lifeTime > resetTime)
        {
            gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        hit = true;
        base.OnTriggerEnter2D(collision);
       //gameObject.SetActive(false) ;
       coll.enabled = false;

        if(anim != null)
        {
            anim.SetTrigger("explodes");
        }
        else
        {
            gameObject.SetActive(false);
        }
    }

    private void Deactivate()
    {
        gameObject.SetActive(false); 
    }
}
