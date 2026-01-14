using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    public GameObject Level;
    public GameObject LevelFail;
    public bool Wall = false;
    private int count = 0;
    public Text text;
    private PointCollection points;
    private void Start()
    {
        Level.SetActive(false);
        LevelFail.SetActive(false);
        UpdateText();

       
        points = FindObjectOfType<PointCollection>(); // Automatically finds the instance
      
    }
    


    void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.gameObject.CompareTag("Level"))
        {
            int pointValue = (int)points.count;
            Debug.Log("collided");
            Debug.Log((int)points.count);
            if (pointValue== 10)
            {
                Wall = true;
                Debug.Log("win");
                count++;
                UpdateText();
                Level.SetActive(true);
                collision.gameObject.GetComponent<Collider2D>().enabled = false;
                StartCoroutine(Deactivate());
            }
            else
            {
                LevelFail.SetActive(true);
                StartCoroutine(Deactivate());
            }
        }
    }

    private void UpdateText()
    {
        text.text = count.ToString();
    }

    private IEnumerator Deactivate()
    {
        yield return new WaitForSeconds(3f);
        Level.SetActive(false);
        LevelFail.SetActive(false);
    }
}
