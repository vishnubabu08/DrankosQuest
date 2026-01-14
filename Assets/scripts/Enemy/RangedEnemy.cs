/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedEnemy : MonoBehaviour
{
    [Header("Attack Parameters")]
    [SerializeField] private float attackCooldown;
    [SerializeField] private int damage;
    [SerializeField] private float range;

    [Header("Ranged Attack")]
    [SerializeField] private Transform firepoint;
    [SerializeField] private GameObject[] fireballs;

    [Header("Collider Parameters")]
    [SerializeField] private float colliderDistance;
    [SerializeField] BoxCollider2D boxcollider;

    [Header("Player Layer")]
    [SerializeField] LayerMask playerLayer;
    private float cooldownTimer = Mathf.Infinity;

    [Header("Player Layer")]
    [SerializeField] private AudioClip fireBallSound;


    private Animator anim;
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
            if (cooldownTimer >= attackCooldown )
            {
                cooldownTimer = 0;
                anim.SetTrigger("RangAttack");
            }
        }
        if (enemyPatrol != null)
            enemyPatrol.enabled = !PlyerInsight();


    }

    private void RangeAttack()
    {
        AudioManager.instance.PlaySound(fireBallSound);
        cooldownTimer = 0;
        fireballs[0].transform.position= firepoint.position;
        fireballs[0] .GetComponent<EnemyProjectiles>().ActivateProjectiles();

    }

    private int FindFireball()
    {
        for (int i = 0;i<fireballs.Length;i++)
        {
            if (!fireballs[i].activeInHierarchy)
                return i;
        }
        return 0;
    }

    private bool PlyerInsight()
    {
        RaycastHit2D hit =
           Physics2D.BoxCast(boxcollider.bounds.center + transform.right * range * transform.localScale.x * colliderDistance,
           new Vector3(boxcollider.bounds.size.x * range, boxcollider.bounds.size.y, boxcollider.bounds.size.z),
           0, Vector2.left, 0, playerLayer);

       

        return hit.collider != null;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(boxcollider.bounds.center + transform.right * range * transform.localScale.x * colliderDistance,
           new Vector3(boxcollider.bounds.size.x * range, boxcollider.bounds.size.y, boxcollider.bounds.size.z));
    }

}
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedEnemy : MonoBehaviour
{
    [Header("Attack Parameters")]
    [SerializeField] private float attackCooldown;
    [SerializeField] private int damage;
    [SerializeField] private float range;

    [Header("Ranged Attack")]
    [SerializeField] private Transform firepoint;
    [SerializeField] private GameObject[] fireballs;

    [Header("Collider Parameters")]
    [SerializeField] private float colliderDistance;
    [SerializeField] BoxCollider2D boxcollider;

    [Header("Player Layer")]
    [SerializeField] LayerMask playerLayer;
    private float cooldownTimer = Mathf.Infinity;

    [Header("Player Layer")]
    [SerializeField] private AudioClip fireBallSound;

    private Animator anim;
    private EnemyPatrol enemyPatrol;

    private Transform player; // ✅ store player transform

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
            FacePlayer(); // ✅ rotate/flip towards player

            if (cooldownTimer >= attackCooldown)
            {
                cooldownTimer = 0;
                anim.SetTrigger("RangAttack");
            }
        }

        if (enemyPatrol != null)
            enemyPatrol.enabled = !PlyerInsight();
    }

    private void RangeAttack()
    {
        AudioManager.instance.PlaySound(fireBallSound);
        cooldownTimer = 0;
        int fireballIndex = FindFireball();
        fireballs[fireballIndex].transform.position = firepoint.position;
        fireballs[fireballIndex].GetComponent<EnemyProjectiles>().ActivateProjectiles();
    }

    private int FindFireball()
    {
        for (int i = 0; i < fireballs.Length; i++)
        {
            if (!fireballs[i].activeInHierarchy)
                return i;
        }
        return 0;
    }

    private bool PlyerInsight()
    {
        RaycastHit2D hit =
           Physics2D.BoxCast(boxcollider.bounds.center + transform.right * range * transform.localScale.x * colliderDistance,
           new Vector3(boxcollider.bounds.size.x * range, boxcollider.bounds.size.y, boxcollider.bounds.size.z),
           0, Vector2.left, 0, playerLayer);

        if (hit.collider != null)
        {
            player = hit.transform; // ✅ store player transform when detected
            return true;
        }
        return false;
    }

    private void FacePlayer()
    {
        if (player == null) return;

        // if player is on the right side of enemy
        if (player.position.x > transform.position.x)
            transform.localScale = new Vector3(1, 1, 1);  // facing right
        else
            transform.localScale = new Vector3(-1, 1, 1); // facing left
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(boxcollider.bounds.center + transform.right * range * transform.localScale.x * colliderDistance,
           new Vector3(boxcollider.bounds.size.x * range, boxcollider.bounds.size.y, boxcollider.bounds.size.z));
    }
}
