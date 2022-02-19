using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SwitchButtonManager : MonoBehaviour
{
    public GameObject switchRight;
    public GameObject switchLeft;
    public GameObject[] levelScreens;
    public int currentLevelScreen;

    private void Start()
    {
        currentLevelScreen = 0;
    }

    private void Update()
    {
        if (currentLevelScreen == 0)
        {
            switchLeft.GetComponent<Image>().enabled = false;
            switchLeft.GetComponent<Button>().enabled = false;
        }
        else
        {
            switchLeft.GetComponent<Image>().enabled = true;
            switchLeft.GetComponent<Button>().enabled = true;
        }

        if (currentLevelScreen == levelScreens.Length - 1)
        {
            switchRight.GetComponent<Image>().enabled = false;
            switchRight.GetComponent<Button>().enabled = false;
        }
        else
        {
            switchRight.GetComponent<Image>().enabled = true;
            switchRight.GetComponent<Button>().enabled = true;
        }
    }

    public void AddCurrentScreen()
    {
        currentLevelScreen++;
    }
    
    public void SubtractCurrentScreen()
    {
        currentLevelScreen--;
    }
}
