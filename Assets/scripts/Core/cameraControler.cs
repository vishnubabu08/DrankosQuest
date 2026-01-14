using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraControler : MonoBehaviour
{

     [SerializeField] private Transform Player;


      private void Update()
      {
        transform.position = new Vector3(Player.transform.position.x,transform.position.y,transform.position.z); 
      }
  


}
