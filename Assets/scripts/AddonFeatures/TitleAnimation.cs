using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleAnimation : MonoBehaviour
{

    [SerializeField] private GameObject canvas;

    public AudioSource audioSource;
    private void Start()
    {
        audioSource.Play();
    }


    public void Update()
    {
        StartCoroutine(activeCanvas());
    }

    private IEnumerator activeCanvas()
    {
        yield return new WaitForSeconds(15f);
        canvas.SetActive(true);

       
    }

    public void GameStart()
    {
        SceneManager.LoadScene(1);
    }
    public void Play()
    {
        SceneManager.LoadScene(2);
    }

}
