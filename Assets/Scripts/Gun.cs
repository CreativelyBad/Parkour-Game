using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public GameObject playerController;
    public float x = 0f;

    private void Start()
    {
        transform.rotation = Quaternion.Euler(0, 0, 0);
    }

    void Update()
    {
        if (!playerController.GetComponent<PlayerController>().isPaused)
        {
            //ShootDirection();
            MouseAim();
        }
    }

    private void MouseAim()
    {
        // aim at mouse
        Vector3 difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        difference.Normalize();
        float rotation_z = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;

        // fix if inverted
        if (x == 1)
        {
            transform.rotation = Quaternion.Euler(0f, 0f, rotation_z);
        }
        else if (x == -1)
        {
            transform.rotation = Quaternion.Euler(0f, 0f, rotation_z - 180);
        }
    }
    
    // old aiming system
    private void ShootDirection()
    {
        Vector2 mouse = Input.mousePosition * 10;
        Vector2 mScreen = Camera.main.ScreenToViewportPoint(new Vector2(mouse.x, mouse.y)) * Mathf.Rad2Deg;

        Vector2 mPosition = new Vector2(0f, mScreen.y);
        transform.rotation = Quaternion.Euler(0f, 0f, mPosition.y * x);
    }
}
