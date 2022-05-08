using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicManager : MonoBehaviour
{
    public static MusicManager instance;

    public AudioSource musicSource;
    public AudioClip[] songs;

    private int songSelected;

    void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
    }

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (songSelected > 1 && SceneManager.GetActiveScene().buildIndex != 0)
        {
            return;
        }

        musicSource.Stop();

        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            ChooseMenuMusic();
        }
        else
        {
            InGameMusic();
        }
    }

    private void ChooseMenuMusic()
    {
        songSelected = UnityEngine.Random.Range(0, 2);
        musicSource.clip = songs[songSelected];
        musicSource.Play();
    }

    public void InGameMusic()
    {
        songSelected = 2;
        musicSource.clip = songs[2];
        musicSource.Play();
    }
}
