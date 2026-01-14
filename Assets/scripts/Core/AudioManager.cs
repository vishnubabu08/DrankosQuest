using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance { get; private set; }
    private AudioSource sourse;
    private AudioSource musicSource;


    private void Awake()
    {
       musicSource = transform.GetChild(0).GetComponent<AudioSource>();
        sourse = GetComponent<AudioSource>();
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (instance != null && instance!=this)
        {
             Destroy(gameObject);
        }

        changeMusicVolume(0);
        changeSoundVolume(0);
    }

    public void PlaySound(AudioClip _sound)
    {
        sourse .PlayOneShot(_sound);
    }

    public void changeSoundVolume(float _change)
    {
      changeSourceVolume(1,"soundVolume",_change,sourse);
    }

    public void changeMusicVolume(float _change)
    {
        changeSourceVolume(0.3f, "musicVolume", _change, musicSource);

    }

    private void changeSourceVolume(float baseVolume,string volumeName,float change,AudioSource source)
    {
        float currentVolume = PlayerPrefs.GetFloat(volumeName,1);
        currentVolume += change;

        if (currentVolume > 1)
            currentVolume = 0;

        else if (currentVolume < 0)
            currentVolume = 1;

        float finalVolume = currentVolume * baseVolume;
    source.volume = finalVolume;

        PlayerPrefs.SetFloat(volumeName,currentVolume);

    }
  
}
