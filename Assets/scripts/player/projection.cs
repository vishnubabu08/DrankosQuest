using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class projection : MonoBehaviour
{
    [SerializeField] private float speed;
    private float direction;
    private bool hit;

    private Animator anim;
    private BoxCollider2D boxcolider;
   public float lifetime;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        boxcolider = GetComponent<BoxCollider2D>();
    }

    private void Update()
    {
        if (hit) return;
        
            float movementSpeed = speed * Time.deltaTime * direction;
            transform.Translate(movementSpeed,0,0);

        lifetime += Time.deltaTime;
        if(lifetime>5)gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        hit = true;
        boxcolider.enabled = false;
        anim.SetTrigger("explodes");

        Health health = collision.GetComponent<Health>();
        if (health != null)   // ✅ safe check
        {
            health.TakeDamage(1);
        }
    }

    public void setDirection(float _direction)
    {
        lifetime = 0;
        direction = _direction; 
        gameObject.SetActive(true);
        hit = false;
        boxcolider.enabled = true;

        float localScaleX = transform.localScale.x;
        if (Mathf.Sign(localScaleX) != _direction)
            localScaleX = -localScaleX;
        transform.localScale = new Vector3(localScaleX,transform.localScale.y,transform.localScale.z);


    }
    private void Deactivate()
    {
        gameObject.SetActive(false);
    }

}
