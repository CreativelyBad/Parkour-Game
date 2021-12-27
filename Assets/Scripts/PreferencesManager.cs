using System;
using UnityEngine;

public class PreferencesManager : MonoBehaviour
{
    public Canvas optionsScreen;

    void Start()
    {
        LoadPrefs();
    }

    private void OnApplicationQuit()
    {
        SavePrefs();
    }

    bool IntToBool(int value)
    {
        if (value != 0)
            return true;
        else
            return false;
    }

    public void SavePrefs()
    {
        PlayerPrefs.SetInt("graphicsIndex", optionsScreen.GetComponent<SettingsMenu>().graphicsIndex);
        PlayerPrefs.SetInt("isFullscreen", optionsScreen.GetComponent<SettingsMenu>().fullscreen);
        //PlayerPrefs.SetInt("resIndex", optionsScreen.GetComponent<SettingsMenu>().resIndex);
        PlayerPrefs.Save();
    }

    private void LoadPrefs()
    {
        var graphicsIndex = PlayerPrefs.GetInt("graphicsIndex", 2);
        var fullscreen = PlayerPrefs.GetInt("isFullscreen", 1);
        //var resIndex = PlayerPrefs.GetInt("resIndex", 0);
        optionsScreen.GetComponent<SettingsMenu>().SetQuality(graphicsIndex);
        optionsScreen.GetComponent<SettingsMenu>().SetFullscreen(IntToBool(fullscreen));
        //optionsScreen.GetComponent<SettingsMenu>().SetResolution(resIndex);
    }
}
