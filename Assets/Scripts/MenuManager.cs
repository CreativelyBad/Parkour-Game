using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MenuManager : MonoBehaviour
{
    public TMP_Text coinCount;
    public TMP_Text coinCount2;

    public AudioSource musicSource;
    public AudioClip[] songs;

    void Start()
    {
        ChooseMusic();

        UnityEngine.Cursor.visible = true;
        coinCount.text = PlayerPrefs.GetInt("CoinTotal").ToString();
        coinCount2.text = PlayerPrefs.GetInt("CoinTotal").ToString();
    }

    private void ChooseMusic()
    {
        int songSelected = UnityEngine.Random.Range(0, songs.Length);

        musicSource.PlayOneShot(songs[songSelected]);

        Debug.Log(songs[songSelected]);
    }
}
