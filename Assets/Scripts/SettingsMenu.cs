using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SettingsMenu : MonoBehaviour
{
    public TMP_Dropdown resolutionDropdown;
    public TMP_Dropdown graphicsDropdown;
    public Toggle isFullscreenToggle;
    public int graphicsIndex;
    public int fullscreen;
    //public int resIndex;

    Resolution[] resolutions;

    private void Start()
    {
        ResolutionOnStart();

        graphicsDropdown.value = graphicsIndex; 
        SetQuality(graphicsIndex);

        isFullscreenToggle.isOn = IntToBool(fullscreen);
        SetFullscreen(IntToBool(fullscreen));
    }

    private void ResolutionOnStart()
    {
        resolutions = Screen.resolutions;

        resolutionDropdown.ClearOptions();

        List<string> options = new List<string>();

        int currentResolutionIndex = 0;
        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + "x" + resolutions[i].height;
            options.Add(option);

            if (resolutions[i].width == Screen.width &&
                resolutions[i].height == Screen.height)
            {
                currentResolutionIndex = i;
            }
        }

        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();
    }

    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }

    public void SetQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
        graphicsIndex = qualityIndex;
    }

    public void SetFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
        fullscreen = BoolToInt(isFullscreen);
    }

    bool IntToBool(int value)
    {
        if (value != 0)
            return true;
        else
            return false;
    }

    int BoolToInt(bool value)
    {
        if (value)
            return 1;
        else
            return 0;
    }
}
