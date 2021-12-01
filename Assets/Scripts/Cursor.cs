using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cursor : MonoBehaviour
{
    private Camera cam;

    private void Start()
    {
        cam = Camera.main;
        UnityEngine.Cursor.visible = false;
    }

    void Update()
    {
        CursorPosition();
        UnityEngine.Cursor.visible = false;
    }

    private void CursorPosition()
    {
        Vector2 mouse = cam.ScreenToWorldPoint(Input.mousePosition);
        transform.position = mouse;
    }
}
