using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;

public class Health : MonoBehaviour
{
    [Header("Health")]
    [SerializeField] private float startingHealth;
    public float currentHealth { get; private set; }
    private Animator anim;
    private bool dead;

    [Header("Iframes")]
   [SerializeField] private float iframeDuration;
    [SerializeField] private int numberOfFlashes;
    private SpriteRenderer spritRend;

    [Header("Death Sound")]
    [SerializeField] private AudioClip deathSound;

    [Header("Hurt Sound")]
    [SerializeField] private AudioClip HurtSound;



    [Header("components")]
    [SerializeField] private Behaviour[] components;
    private bool invulnerable;

    public GameObject MobControl;
    private void Awake()
    {
        currentHealth = startingHealth;
        anim = GetComponent<Animator>();
        spritRend = GetComponent<SpriteRenderer>();
    }
    private void Update()
    {
        if (transform.position.y < -40|| transform.position.y >80 && !dead)
        {
            // Instantly kill the player
            TakeDamage(currentHealth);
        }
    }
    public void TakeDamage(float _damage)
    {

        if(invulnerable)return;

        currentHealth = Mathf.Clamp(currentHealth-_damage, 0,startingHealth);
           

        if(currentHealth > 0)
        {
            anim.SetTrigger("hurt");
            StartCoroutine(Invunerability());
            AudioManager.instance.PlaySound(HurtSound);
        }

        else
        {
            if (!dead)
            {
                MobControl.SetActive(false);
               

                if(GetComponent<playermovement>() != null)
                GetComponent<playermovement>().enabled = false;

                if (GetComponentInParent<EnemyPatrol>() != null)
                    GetComponentInParent<EnemyPatrol>().enabled = false;

                if (GetComponent<MeleeEnemy>() != null)
                    GetComponent<MeleeEnemy>().enabled = false;

                foreach(Behaviour components in components)
                {
                    components.enabled = false;
                }

                anim.SetBool("grounded", true);
                anim.SetTrigger("die");

                dead = true;
                AudioManager.instance.PlaySound(deathSound);
            }
        }
    }

    public void AddHealth(float _value)
    {
          currentHealth =Mathf.Clamp(currentHealth + _value,0, startingHealth);
    }


    public void Respawn()
    {
        MobControl.SetActive(true);

        dead = false;
        AddHealth(startingHealth);
        anim.ResetTrigger("die");
        anim.Play("playeranimtion");
        //StartCoroutine(Invunerability());

        foreach (Behaviour components in components)
        {
            components.enabled = true;
        }
    }
    private IEnumerator Invunerability()
    {
        invulnerable = true;
        Physics2D.IgnoreLayerCollision(8,9,true);

        for(int i = 0; i < numberOfFlashes; i++)
        {
            spritRend.color = new Color(1, 0, 0, 0.5f);
            yield return new WaitForSeconds(iframeDuration/(numberOfFlashes));
            spritRend.color= Color.white;
            yield return new WaitForSeconds(iframeDuration / (numberOfFlashes));
        }
        Physics2D.IgnoreLayerCollision(8, 9, false);

        invulnerable = false;
    }
   private void Deactivate()
    {
        gameObject.SetActive(false);
    }
}
