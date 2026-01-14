/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeEnemy : MonoBehaviour
{
    [Header("Attack Parameters")]
    [SerializeField] private float attackCooldown; 
    [SerializeField] private int damage;
    [SerializeField] private float range;

    [Header("Collider Parameters")]
    [SerializeField] private float colliderDistance;
    [SerializeField] BoxCollider2D boxcollider;

    [Header("Player Layer")]
    [SerializeField] LayerMask playerLayer;
    private float cooldownTimer= Mathf.Infinity;

    [Header("Attack Sound")]
    [SerializeField] AudioClip attackSound; 

    private Animator anim;
    private Health plyerHealth;
    private EnemyPatrol enemyPatrol;


    private void Awake()
    {
        anim = GetComponent<Animator>();
        enemyPatrol = GetComponentInParent<EnemyPatrol>();
    }


    private void Update()
    {
        cooldownTimer += Time.deltaTime;

        if (PlyerInsight())
        {
           if(cooldownTimer >= attackCooldown && plyerHealth.currentHealth>0)
           {
                cooldownTimer = 0;
                anim.SetTrigger("MeleeAttack");
                AudioManager.instance.PlaySound(attackSound);
           }
        }
       if(enemyPatrol!= null)
            enemyPatrol.enabled =!PlyerInsight();
        

    }

    private bool PlyerInsight()
    {
        RaycastHit2D hit =
           Physics2D.BoxCast(boxcollider.bounds.center + transform.right * range * transform.localScale.x * colliderDistance,
           new Vector3(boxcollider.bounds.size.x * range,boxcollider.bounds.size.y, boxcollider.bounds.size.z),
           0, Vector2.left, 0, playerLayer);

        if (hit.collider != null)
            plyerHealth = hit.transform.GetComponent<Health>();

        return hit.collider != null;
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(boxcollider.bounds.center + transform.right*range * transform.localScale.x * colliderDistance,
           new Vector3(boxcollider.bounds.size.x * range, boxcollider.bounds.size.y, boxcollider.bounds.size.z));
    }

    private void DamagePlayer()
    {
        if(PlyerInsight())
        {
            plyerHealth.TakeDamage(damage);
        }
    }
}
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeEnemy : MonoBehaviour
{
    [Header("Attack Parameters")]
    [SerializeField] private float attackCooldown;
    [SerializeField] private int damage;
    [SerializeField] private float range;

    [Header("Collider Parameters")]
    [SerializeField] private float colliderDistance;
    [SerializeField] private BoxCollider2D boxcollider;

    [Header("Player Layer")]
    [SerializeField] private LayerMask playerLayer;
    private float cooldownTimer = Mathf.Infinity;

    [Header("Attack Sound")]
    [SerializeField] private AudioClip attackSound;

    private Animator anim;
    private Health plyerHealth;
    private EnemyPatrol enemyPatrol;
    private Transform player; // store player reference

    private void Awake()
    {
        anim = GetComponent<Animator>();
        enemyPatrol = GetComponentInParent<EnemyPatrol>();
    }

    private void Update()
    {
        cooldownTimer += Time.deltaTime;

        if (PlyerInsight())
        {
            // Rotate towards player before attacking
            RotateTowardsPlayer();

            if (cooldownTimer >= attackCooldown && plyerHealth.currentHealth > 0)
            {
                cooldownTimer = 0;
                anim.SetTrigger("MeleeAttack");
                AudioManager.instance.PlaySound(attackSound);
            }
        }

        if (enemyPatrol != null)
            enemyPatrol.enabled = !PlyerInsight();
    }

    private void RotateTowardsPlayer()
    {
        if (player == null) return;

        // Flip enemy towards player (only X axis)
        if (player.position.x < transform.position.x)
        {
            // Player is on the left → face left
            transform.localScale = new Vector3(-1 * Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }
        else
        {
            // Player is on the right → face right
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }
    }

    private bool PlyerInsight()
    {
        RaycastHit2D hit =
           Physics2D.BoxCast(boxcollider.bounds.center + transform.right * range * transform.localScale.x * colliderDistance,
           new Vector3(boxcollider.bounds.size.x * range, boxcollider.bounds.size.y, boxcollider.bounds.size.z),
           0, Vector2.left, 0, playerLayer);

        if (hit.collider != null)
        {
            plyerHealth = hit.transform.GetComponent<Health>();
            player = hit.transform; // Save reference for rotation
        }

        return hit.collider != null;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(
            boxcollider.bounds.center + transform.right * range * transform.localScale.x * colliderDistance,
            new Vector3(boxcollider.bounds.size.x * range, boxcollider.bounds.size.y, boxcollider.bounds.size.z)
        );
    }

    private void DamagePlayer()
    {
        if (PlyerInsight())
        {
            plyerHealth.TakeDamage(damage);
        }
    }
}
