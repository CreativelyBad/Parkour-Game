using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuButtons : MonoBehaviour
{
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

    public void OnQuit2(string level)
    {
        SceneManager.LoadScene(level);
    }
}
