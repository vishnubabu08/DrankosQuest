using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRespawn : MonoBehaviour
{
    [SerializeField] private AudioClip checkpointSound;
    private Transform currentCheckpoint;
    private Health playerHealth;
    private UiManager uiManager;

    private void Awake()
    {
        playerHealth = GetComponent<Health>();
        uiManager= FindObjectOfType<UiManager>();
    }

    /* public void CheckRespawn()
     {

         if(currentCheckpoint ==  null)
         {

             uiManager.GameOver();
             return;
         }


         transform.position = currentCheckpoint.position;
         playerHealth.Respawn();

     }*/

    public void CheckRespawn()
    {
        if (currentCheckpoint == null)
        {
            uiManager.GameOver();
            return;
        }

        // Place player slightly above checkpoint
        transform.position = currentCheckpoint.position + Vector3.up * 0.5f;

        // Reset health and animations
        playerHealth.Respawn();

        // Reset Rigidbody physics
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.linearVelocity = Vector2.zero;
            rb.WakeUp();
        }
    }



    /*
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.transform.tag == "Checkpoints")
            {
                currentCheckpoint = collision.transform;
                AudioManager.instance.PlaySound(checkpointSound);
                collision.GetComponent<Collider2D>().enabled = false;
                collision.GetComponent<Animator>().SetTrigger("appear");
            }
        }*/


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Checkpoints"))
        {
            // Use child "RespawnPoint" if available
            Transform respawn = collision.transform.Find("RespawnPoint");
            if (respawn != null)
                currentCheckpoint = respawn;
            else
                currentCheckpoint = collision.transform; // fallback

            AudioManager.instance.PlaySound(checkpointSound);
            collision.GetComponent<Collider2D>().enabled = false;
            collision.GetComponent<Animator>().SetTrigger("appear");
        }
    }
}

