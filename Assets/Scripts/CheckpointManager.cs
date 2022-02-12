using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointManager : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    public Sprite checkpointOn;
    public Sprite checkpointOff;

    private bool isActivated;

    private void Start()
    {
        isActivated = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isActivated)
        {
            return;
        }

        if (collision.tag == "Player")
        {
            isActivated = true;
            ActivateCheckpoint();
        }
    }

    private void ActivateCheckpoint()
    {
        if (isActivated)
        {
            spriteRenderer.sprite = checkpointOn;
        }
        else
        {
            spriteRenderer.sprite = checkpointOff;
        }
    }
}
