using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    [SerializeField] private GameObject[] enimies;
    private Vector3[] initialPosition;

    private void Awake()
    {
        initialPosition = new Vector3[enimies.Length];

        for(int i = 0; i < enimies.Length; i++)
        {
            if(enimies[i] != null)
            {
                initialPosition[i] = enimies[i].transform.position;
            }
        }
    }

    public void ActivateRoom(bool _status)
    {
        for (int i = 0; i < enimies.Length; i++)
        {
            if (enimies[i] != null)
            {
               enimies[i].SetActive(_status);
                enimies[i].transform.position= initialPosition[i];
            }
        }
    }
}
