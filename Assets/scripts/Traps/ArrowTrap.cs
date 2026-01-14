using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowTrap : MonoBehaviour
{
    [SerializeField] private float attackCooldown;
    [SerializeField] private Transform FirePoint;
    [SerializeField] private GameObject[] Arrow;
    private float coolDownTimer;
    public GameComplete Complete;

    [Header("SFX")]
   // [SerializeField] private AudioClip arrowSound;
    [SerializeField] private AudioSource arrowSound1;
    private void Attack()
    {
        coolDownTimer = 0;

      //  AudioManager.instance.PlaySound(arrowSound);
        arrowSound1.Play();
        Arrow[FindFireBall()].transform.position = FirePoint.position;
        Arrow[FindFireBall()].GetComponent<EnemyProjectiles>().ActivateProjectiles();
    }

    private int FindFireBall()
    {
        for(int i = 0; i < Arrow.Length; i++)
        {
            if (!Arrow[i].activeInHierarchy)
            {
                return i;
            }
        }
        return 0;
    }

    private void Update()
    {
        coolDownTimer += Time.deltaTime;
        if(coolDownTimer >= attackCooldown)
        {
            Attack();
        }


        if(Complete.Finish==true)
        {
            arrowSound1.Stop();
        }
    }


}
