using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Audio;
using System;

public class SettingsMenu : MonoBehaviour
{
    public TMP_Dropdown resolutionDropdown;
    public Toggle isFullscreenToggle;
    public AudioMixer audioMixer;
    public Slider masterSlider;
    public Slider musicSlider;
    public Slider sfxSlider;

    Resolution[] resolutions;

    private void Start()
    {
        ResolutionOnStart();

        SetFullscreen(IntToBool(PlayerPrefs.GetInt("isFullscreen")));

        SetMasterVolume(PlayerPrefs.GetFloat("volumeMaster") * -1f);
        masterSlider.value = PlayerPrefs.GetFloat("volumeMaster") * -1f;
        SetMusicVolume(PlayerPrefs.GetFloat("volumeMusic") * -1f);
        musicSlider.value = PlayerPrefs.GetFloat("volumeMusic") * -1f;
        SetSFXVolume(PlayerPrefs.GetFloat("volumeSFX") * -1f);
        sfxSlider.value = PlayerPrefs.GetFloat("volumeSFX") * -1f;
    }

    private void ResolutionOnStart()
    {
        resolutions = Screen.resolutions;

        resolutionDropdown.ClearOptions();

        List<string> options = new List<string>();

        int currentResolutionIndex = 0;
        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + "x" + resolutions[i].height + " " +
                resolutions[i].refreshRate + "Hz";

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

    public void SetFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;

        PlayerPrefs.SetInt("isFullscreen", BoolToInt(isFullscreen));
        PlayerPrefs.Save();
    }

    public void SetMasterVolume(float volume)
    {
        audioMixer.SetFloat("volumeMaster", volume);

        PlayerPrefs.SetFloat("volumeMaster", volume * -1f);
        PlayerPrefs.Save();
    }
    
    public void SetMusicVolume(float volume)
    {
        audioMixer.SetFloat("volumeMusic", volume);

        PlayerPrefs.SetFloat("volumeMusic", volume * -1f);
        PlayerPrefs.Save();
    }
    
    public void SetSFXVolume(float volume)
    {
        audioMixer.SetFloat("volumeSFX", volume);

        PlayerPrefs.SetFloat("volumeSFX", volume * -1f);
        PlayerPrefs.Save();
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
