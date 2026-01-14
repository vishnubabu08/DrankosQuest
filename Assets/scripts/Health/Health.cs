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

        // If you forgot to assign it in the inspector, assume it is THIS object
        if (MobControl == null)
        {
            MobControl = this.gameObject;
        }
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
        if (invulnerable) return;

        currentHealth = Mathf.Clamp(currentHealth - _damage, 0, startingHealth);

        if (currentHealth > 0)
        {
            anim.SetTrigger("hurt");
            StartCoroutine(Invunerability());
            if (HurtSound != null) AudioManager.instance.PlaySound(HurtSound); // Safe check for sound too
        }
        else
        {
            if (!dead)
            {
                // --- FIX STARTS HERE ---
                // Only try to disable MobControl if it has been assigned
                if (MobControl != null)
                {
                    MobControl.SetActive(false);
                }
                else
                {
                    // If MobControl is missing, disable the object this script is attached to as a fallback
                    Debug.LogWarning("MobControl was not assigned on " + gameObject.name + ". Disabling self instead.");
                    gameObject.SetActive(false);
                }
                // --- FIX ENDS HERE ---

                if (GetComponent<playermovement>() != null)
                    GetComponent<playermovement>().enabled = false;

                if (GetComponentInParent<EnemyPatrol>() != null)
                    GetComponentInParent<EnemyPatrol>().enabled = false;

                if (GetComponent<MeleeEnemy>() != null)
                    GetComponent<MeleeEnemy>().enabled = false;

                foreach (Behaviour components in components)
                {
                    components.enabled = false;
                }

                anim.SetBool("grounded", true);
                anim.SetTrigger("die");

                dead = true;
                if (deathSound != null) AudioManager.instance.PlaySound(deathSound);
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
