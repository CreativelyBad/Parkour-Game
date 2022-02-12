using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelButtons : MonoBehaviour
{
    public GameObject unlocked;
    public GameObject locked;
    private Button buttonComponent;

    private void Start()
    {
        buttonComponent = GetComponent<Button>();

        if (buttonComponent.interactable)
        {
            unlocked.SetActive(true);
            locked.SetActive(false);
        }
        else
        {
            unlocked.SetActive(false);
            locked.SetActive(true);
        }
    }
}
