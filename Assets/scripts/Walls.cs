using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Walls : MonoBehaviour
{
    public LevelManager Level;

    private void Start()
    {
        Level = FindObjectOfType<LevelManager>();

        if (Level == null)
        {
            Debug.LogError(" LevelManager not found!");
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Player collided with wall");

            if (Level != null && Level.Wall)
            {
                Debug.Log("Wall is true, disabling object");
                gameObject.SetActive(false);
            }
        }
    }
}
