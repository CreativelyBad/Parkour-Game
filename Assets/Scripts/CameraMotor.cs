using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMotor : MonoBehaviour
{
    public Transform target;
    public float smoothSpeed = 2f;
    public Vector3 offset;
    public bool followY = true;

    private void LateUpdate()
    {
        // check whether to follow on x or on y
        if (followY)
        {
            SetCameraPositionY();
        }
        else
        {
            SetCameraPositionX();
        }
    }

    private void SetCameraPositionY()
    {
        // follow player
        Vector3 position = transform.position;
        position.y = (target.position + offset).y;

        Vector3 desiredPosition = position;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);
        transform.position = smoothedPosition;

        // make sure camera doesn't move past the bottom of the level
        if (transform.position.y < 0)
        {
            CheckPosition();
        }
    }
    
    private void SetCameraPositionX()
    {
        // follow player
        Vector3 position = transform.position;
        position.x = (target.position + offset).x;

        Vector3 desiredPosition = position;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);
        transform.position = smoothedPosition;

        // make sure camera doesn't move past the back of the level
        if (transform.position.x < 0)
        {
            CheckPosition();
        }
    }

    private void CheckPosition()
    {
        // set camera position to 0 on the y
        transform.position = new Vector3(0, 0, -10);
    }
}
