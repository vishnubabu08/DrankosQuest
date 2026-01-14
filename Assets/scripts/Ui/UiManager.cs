using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UiManager : MonoBehaviour
{
    [Header("Game Over")]
    [SerializeField] private GameObject gameOverScreen;
    [SerializeField] private AudioClip gameOverSound;

    [Header("Pause")]
    [SerializeField] private GameObject pauseScreen;



    private void Awake()
    {
        gameOverScreen.SetActive(false);
        pauseScreen.SetActive(false);
    }
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if (pauseScreen.activeInHierarchy)
                PauseGame(false);

            else
                PauseGame(true);    
        }
    }
    #region Game Over
    public void GameOver()
    {
        gameOverScreen.SetActive(true);
        AudioManager.instance.PlaySound(gameOverSound);
    }

    public void Restart()
    {
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        SceneManager.LoadScene("Levels");
        PauseGame(false );
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(1);
    }

    public void Quit()
    {
        Application.Quit();
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }

    #endregion

    #region Pause

   public void PauseGame(bool status)
    {
        pauseScreen.SetActive(status);
        if(status )
        Time.timeScale = 0f;
        else
            Time.timeScale = 1f;
    }

    public void changeSoundVolume()
    {
        AudioManager.instance.changeSoundVolume(0.2f);
    }

    public void MusicVolume()
    {
        AudioManager.instance.changeMusicVolume(0.2f);
    } 
    #endregion
}


