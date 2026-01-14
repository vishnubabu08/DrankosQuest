using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameComplete : MonoBehaviour
{
    public GameObject complete;
    public ParticleSystem ParticleSystem1;
    public ParticleSystem ParticleSystem2;
    public ParticleSystem Diamond;
    public GameObject Tressure;
    public AudioSource audio;
    public AudioSource heroic;
    public bool Finish = false;
    private void Start()
    {
        ParticleSystem1.Stop();
        ParticleSystem2.Stop();
        Diamond.Stop();
        complete.SetActive(false);
    }
   /* private void OnCollisionEnter2D(Collision2D collision)
    {
      if(collision.gameObject.tag == "Player")
        {
            //complete.SetActive(true);

            ParticleSystem1.Play();
            ParticleSystem2.Play();

            StartCoroutine(Delay());
        }
    }*/
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            //complete.SetActive(true);
            Finish = true;
            ParticleSystem1.Play();
            ParticleSystem2.Play();
            audio.Play();
            heroic.Stop();
            StartCoroutine(Delay());
        }
    }
    private IEnumerator Delay()
    {
        yield return new WaitForSeconds(2f);
        Tressure.SetActive(false );
        ParticleSystem1.Stop();
        ParticleSystem2.Stop();
        Diamond.Play();
        yield return new WaitForSeconds(3f);
        complete.SetActive(true);
    }

}
