using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MenuManager : MonoBehaviour
{
    public TMP_Text coinCount;
    public TMP_Text coinCount2;

    void Start()
    {
        UnityEngine.Cursor.visible = true;
        coinCount.text = PlayerPrefs.GetInt("CoinTotal").ToString();
        coinCount2.text = PlayerPrefs.GetInt("CoinTotal").ToString();
    }
}
