using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class GemCollect : MonoBehaviour
{

    
    [SerializeField] private PointCollection point;
  //  [SerializeField] private Image totalGem;
    [SerializeField] public Image currentGem;
    public Text count;
   
    private void Start()
    {

      // totalGem.fillAmount = point.count/10;

    }

    private void Update()
    {
       
        count.text = point.count.ToString();
       
        currentGem.fillAmount = point.count/10;
       if(point.count==10)
        {
            currentGem.fillAmount = 0;
        }
        
    }
}
