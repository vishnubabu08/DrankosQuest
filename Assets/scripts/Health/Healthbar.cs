using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;




public class Healthbar : MonoBehaviour
{
    [SerializeField] private Health PlayerHealth;

    [SerializeField] public Image totalhealthBar;
    [SerializeField] public Image currenthealthBar;

    private void Start()
    {
        totalhealthBar.fillAmount = PlayerHealth.currentHealth / 10;
    }

    private void Update()
    {
        currenthealthBar.fillAmount = PlayerHealth.currentHealth/10;
    }
}
