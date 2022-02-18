using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LevelManager : MonoBehaviour
{
    public Button[] levelButtons;
    public int levelsUnlocked;
    public int totalLevels;
    public TextMeshProUGUI levelsCount;

    private void Awake()
    {
        totalLevels = levelButtons.Length;

        levelsUnlocked = PlayerPrefs.GetInt("LevelsUnlocked", 1);

        levelsCount.text = levelsUnlocked + "/" + totalLevels;

        for (int i = 0; i < levelButtons.Length; i++)
        {
            levelButtons[i].interactable = false;
        }

        for (int i = 0; i < levelsUnlocked; i++)
        {
            levelButtons[i].interactable = true;
        }
    }
}
