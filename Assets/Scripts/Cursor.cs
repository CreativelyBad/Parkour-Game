using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cursor : MonoBehaviour
{
    private Camera cam;
    private SpriteRenderer spriteRenderer;
    public GameObject playerController;

    private void Start()
    {
        cam = Camera.main;
        UnityEngine.Cursor.visible = false;
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        CursorPosition();
        
        // check whether or not cursor or sprite should be on or off
        if (playerController.GetComponent<PlayerController>().isPaused == true)
        {
            UnityEngine.Cursor.visible = true;
            spriteRenderer.enabled = false;
        }
        else
        {
            UnityEngine.Cursor.visible = false;
            spriteRenderer.enabled = true;
        }

    }

    private void CursorPosition()
    {
        // set aiming cursor position to mouse position
        Vector2 mouse = cam.ScreenToWorldPoint(Input.mousePosition);
        transform.position = mouse;
    }
}
