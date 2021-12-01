using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMotor : MonoBehaviour
{
    public Transform target;

    public float smoothSpeed = 2f;
    public Vector3 offset;

    private void LateUpdate()
    {
        SetCameraPosition();
    }

    private void SetCameraPosition()
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

    private void CheckPosition()
    {
        // set camera position to 0 on the y
        transform.position = new Vector3(0, 0, -10);
    }
}
