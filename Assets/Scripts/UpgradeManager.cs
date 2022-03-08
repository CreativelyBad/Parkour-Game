using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UpgradeManager : MonoBehaviour
{
    public GameObject throwableUpgrade;
    public GameObject healthUpgrade;
    public GameObject sheildUpgrade;

    private int throwableCost = 500;
    private int healthCost = 300;
    private int sheildCost = 800;

    private int coinCount;

    public TextMeshProUGUI coinDisplay;
    public TextMeshProUGUI coinDisplay2;

    private void Start()
    {
        throwableUpgrade.GetComponent<Button>().interactable = !IntToBool(PlayerPrefs.GetInt("CanThrow", 0));
        coinCount = PlayerPrefs.GetInt("CoinTotal", 0);
    }

    public void OnThrowUpgrade()
    {
        if (coinCount >= throwableCost)
        {
            coinCount -= throwableCost;
            PlayerPrefs.SetInt("CanThrow", 1);
            throwableUpgrade.GetComponent<Button>().interactable = false;
            PlayerPrefs.SetInt("CoinTotal", coinCount);
            coinDisplay.text = coinCount.ToString();
            coinDisplay2.text = coinCount.ToString();
        }
    }

    private void OnHealthUpgrade()
    {
        // code
    }

    private void OnSheildUpgrade()
    {
        // code
    }

    bool IntToBool(int input)
    {
        if (input == 1)
            return true;
        else
            return false;
    }
}
