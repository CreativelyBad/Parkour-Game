using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UpgradeManager : MonoBehaviour
{
    public GameObject throwableUpgrade;
    public GameObject healthUpgrade;
    public GameObject shieldUpgrade;

    private int throwableCost = 300;
    private int healthCost = 100;
    private int shieldCost = 500;

    private int coinCount;

    public TextMeshProUGUI coinDisplay;
    public TextMeshProUGUI coinDisplay2;

    private void Start()
    {
        throwableUpgrade.GetComponent<Button>().interactable = !IntToBool(PlayerPrefs.GetInt("CanThrow", 0));
        healthUpgrade.GetComponent<Button>().interactable = !IntToBool(PlayerPrefs.GetInt("HasHealthUpgrade", 0));
        shieldUpgrade.GetComponent<Button>().interactable = !IntToBool(PlayerPrefs.GetInt("CanShield", 0));

        coinCount = PlayerPrefs.GetInt("CoinTotal", 0);
    }

    public void OnThrowUpgrade()
    {
        if (coinCount >= throwableCost)
        {
            coinCount -= throwableCost;
            PlayerPrefs.SetInt("CoinTotal", coinCount);
            PlayerPrefs.SetInt("CanThrow", 1);
            throwableUpgrade.GetComponent<Button>().interactable = false;
            PlayerPrefs.SetInt("CoinTotal", coinCount);
            coinDisplay.text = coinCount.ToString();
            coinDisplay2.text = coinCount.ToString();
            PlayerPrefs.Save();
        }
    }

    public void OnHealthUpgrade()
    {
        if (coinCount >= healthCost)
        {
            coinCount -= healthCost;
            PlayerPrefs.SetInt("CoinTotal", coinCount);
            PlayerPrefs.SetInt("HasHealthUpgrade", 1);
            PlayerPrefs.SetInt("Health", 7);
            coinDisplay.text = coinCount.ToString();
            coinDisplay2.text = coinCount.ToString();
            healthUpgrade.GetComponent<Button>().interactable = false;
            PlayerPrefs.Save();
        }
    }

    public void OnSheildUpgrade()
    {
        if (coinCount >= shieldCost)
        {
            coinCount -= shieldCost;
            PlayerPrefs.SetInt("CoinTotal", coinCount);
            PlayerPrefs.SetInt("CanShield", 1);
            coinDisplay.text = coinCount.ToString();
            coinDisplay2.text = coinCount.ToString();
            shieldUpgrade.GetComponent<Button>().interactable = false;
            PlayerPrefs.Save();
        }
    }

    bool IntToBool(int input)
    {
        if (input == 1)
            return true;
        else
            return false;
    }
}
