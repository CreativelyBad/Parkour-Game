using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MenuTimerSet : MonoBehaviour
{
    public TextMeshProUGUI timeDisplay;
    private TimeSpan timePlaying;

    void Start()
    {
        timePlaying = TimeSpan.FromSeconds(PlayerPrefs.GetFloat("GameTime", 0));
        timeDisplay.text = "00:00.00";
        timeDisplay.text = timePlaying.ToString("mm':'ss'.'ff");
    }
}
