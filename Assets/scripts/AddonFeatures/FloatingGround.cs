using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingGround : MonoBehaviour
{
    public Camera Camera;
    private void OnCollisionEnter2D(Collision2D collision)
    {
       
        if(collision.gameObject.tag == "Float")
        {
            StartCoroutine(Float());

             IEnumerator Float()
            {
                yield return new WaitForSeconds(.3f);
                collision.gameObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;

            }

        }
       else if (collision.gameObject.tag == "Camera")
        {
            Camera.orthographicSize = 10;
        }

        else if (collision.gameObject.tag == "Ground")
        {
            Camera.orthographicSize = 6.141266f;
        }
    }

 

}
