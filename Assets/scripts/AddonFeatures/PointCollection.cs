using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PointCollection : MonoBehaviour
{
    //public GameObject Point;
    private Health health;
    private GemCollect gemCollect;
    private Healthbar healthbar;
    
    public float count { get; private set; }

 

    public Text text;

    private void Update()
    {
     
       // text.text = count.ToString();
        text.text = Mathf.Abs(count).ToString();



    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
       if( collision.gameObject.tag == "Points")
        {
           
            Destroy( collision.gameObject );
            count++;
            
        }
        //Debug.Log(count);

        if (count == 10)
        {

            GetComponent<Health>().AddHealth(1);
           

        }
        if(count > 10)
        {
            count = 1;
        }
    }
}
