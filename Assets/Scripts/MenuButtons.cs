using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuButtons : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip selectClip;

    public void OnPlay(Canvas levelSelect)
    {
        audioSource.PlayOneShot(selectClip);
        levelSelect.enabled = true;
    }

    public void MenuScreenDisabled(Canvas menuScreen)
    {
        menuScreen.enabled = false;
    }

    public void OnOptions(Canvas optionsScreen)
    {
        audioSource.PlayOneShot(selectClip);
        optionsScreen.enabled = true;
    }

    public void OnQuit()
    {
        Application.Quit();
    }

    public void MenuScreenEnabled(Canvas menuScreen)
    {
        menuScreen.enabled = true;
    }

    public void OnBack(Canvas optionsScreen)
    {
        audioSource.PlayOneShot(selectClip);
        optionsScreen.enabled = false;
    }

    public void OnResume(Canvas pauseScreen)
    {
        audioSource.PlayOneShot(selectClip);
        pauseScreen.enabled = false;
    }

    public void SetIsPaused(GameObject playerController)
    {
        playerController.GetComponent<PlayerController>().isPaused = false;
    }

    public void OnReturn(string menu)
    {
        audioSource.PlayOneShot(selectClip);
        SceneManager.LoadScene(menu);
    }

    public void OnLevelSelect(string level)
    {
        audioSource.PlayOneShot(selectClip);
        SceneManager.LoadScene(level);
    }
}
