using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UnlockLevel : MonoBehaviour
{
    private GameObject player;
    private bool isComplete;
    private int levelsComplete;
    private int totalLevels = 10;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        levelsComplete = PlayerPrefs.GetInt("LevelsUnlocked", 1);
    }

    private void Update()
    {
        Physics2D.IgnoreCollision(player.GetComponent<EdgeCollider2D>(), GetComponent<BoxCollider2D>());

        if (isComplete && Input.GetKeyDown(KeyCode.E))
        {
            if (SceneManager.GetActiveScene().buildIndex >= levelsComplete + 1)
            {
                if (levelsComplete != totalLevels)
                {
                    PlayerPrefs.SetInt("LevelsUnlocked", levelsComplete + 1);
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            isComplete = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            isComplete = false;
        }
    }
}
