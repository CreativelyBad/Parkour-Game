using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MenuManager : MonoBehaviour
{
    public TMP_Text coinCount;

    void Start()
    {
        UnityEngine.Cursor.visible = true;
        coinCount.text = PlayerPrefs.GetInt("CoinTotal").ToString();
    }
}
