using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuButtons : MonoBehaviour
{
    // button functions to be called OnClick()

    public void OnPlay(string level)
    {
        SceneManager.LoadScene(level);
    }

    public void OnOptions(Canvas menuScreen)
    {
        menuScreen.enabled = false;
    }

    public void OnOptions2(Canvas optionsScreen)
    {
        optionsScreen.enabled = true;
    }

    public void OnQuit()
    {
        Application.Quit();
    }

    public void OnBack(Canvas menuScreen)
    {
        menuScreen.enabled = true;
    }

    public void OnBack2(Canvas optionsScreen)
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
    }

    public void OnReturn(string menu)
    {
        SceneManager.LoadScene(menu);
    }
}
