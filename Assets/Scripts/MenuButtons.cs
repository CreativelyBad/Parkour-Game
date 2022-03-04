using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuButtons : MonoBehaviour
{
    public void OnPlay(Canvas levelSelect)
    {
        levelSelect.enabled = true;
    }

    public void MenuScreenDisabled(Canvas menuScreen)
    {
        menuScreen.enabled = false;
    }

    public void OnOptions(Canvas optionsScreen)
    {
        optionsScreen.enabled = true;
    }

    public void OnUpgrades(Canvas upgradesScreen)
    {
        upgradesScreen.enabled = true;
    }

    public void OnCredits(Canvas creditsScreen)
    {
        creditsScreen.enabled = true;
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
        optionsScreen.enabled = false;
    }

    public void OnResume(Canvas pauseScreen)
    {
        pauseScreen.enabled = false;
    }

    public void SetIsPaused(GameObject playerController)
    {
        playerController.GetComponent<PlayerController>().isPaused = false;
        TimerController.instance.BeginTimer();
    }

    public void OnReturn(string menu)
    {
        SceneManager.LoadScene(menu);
        PlayerPrefs.SetFloat("GameTime", TimerController.instance.elapsedTime);
        PlayerPrefs.Save();
    }

    public void OnLevelSelect(string level)
    {
        SceneManager.LoadScene(level);
    }

    public void OnReset(GameObject resetCanvas)
    {
        resetCanvas.SetActive(true);
    }

    public void OnConfirmReset()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        PlayerPrefs.SetInt("LevelsUnlocked", 1);
        PlayerPrefs.SetInt("CoinTotal", 0);
        PlayerPrefs.SetFloat("GameTime", 0f);
        PlayerPrefs.Save();
    }

    public void OnDenyReset(GameObject resetCanvas)
    {
        resetCanvas.SetActive(false);
    }

    public void OnSwitchRight(GameObject levelButtons)
    {
        levelButtons.transform.position -= new Vector3(1920, 0, 0);
    }
    
    public void OnSwitchLeft(GameObject levelButtons)
    {
        levelButtons.transform.position += new Vector3(1920, 0, 0);
    }

    public void OnOpenLink(string link)
    {
        Application.OpenURL(link);
        Debug.Log("Link Opened: " + link);
    }
}
