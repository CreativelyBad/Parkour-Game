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

    private List<GameObject> upgrades = new List<GameObject>();

    private int coinCount;

    public TextMeshProUGUI coinDisplay;
    public TextMeshProUGUI coinDisplay2;

    public GameObject[] darkens;
    public GameObject[] costs;

    private void Start()
    {
        throwableUpgrade.GetComponent<Button>().interactable = !IntToBool(PlayerPrefs.GetInt("CanThrow", 0));
        healthUpgrade.GetComponent<Button>().interactable = !IntToBool(PlayerPrefs.GetInt("HasHealthUpgrade", 0));
        shieldUpgrade.GetComponent<Button>().interactable = !IntToBool(PlayerPrefs.GetInt("CanShield", 0));

        upgrades.Add(throwableUpgrade);
        upgrades.Add(healthUpgrade);
        upgrades.Add(shieldUpgrade);

        coinCount = PlayerPrefs.GetInt("CoinTotal", 0);

        for (int i = 0; i < upgrades.Count; i++)
        {
            if (upgrades[i].GetComponent<Button>().interactable)
            {
                darkens[i].SetActive(false);
                costs[i].SetActive(true);
            }
            else
            {
                darkens[i].SetActive(true);
                costs[i].SetActive(false);
            }
        }
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

            darkens[0].SetActive(true);
            costs[0].SetActive(false);
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

            darkens[1].SetActive(true);
            costs[1].SetActive(false);
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

            darkens[2].SetActive(true);
            costs[2].SetActive(false);
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
