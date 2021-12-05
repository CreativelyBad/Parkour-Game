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
        Vector2 mouse = cam.ScreenToWorldPoint(Input.mousePosition);
        transform.position = mouse;
    }
}
