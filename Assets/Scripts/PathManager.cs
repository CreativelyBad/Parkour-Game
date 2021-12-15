using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathManager : MonoBehaviour
{
    public GameObject[] points;
    private Camera cam;

    private void Start()
    {
        cam = Camera.main;
    }

    private void Update()
    {
        for (int i = 0; i < points.Length; i++)
        {
            if (Mathf.Abs(cam.transform.position.x) >= Mathf.Abs(points[i].transform.position.x))
            {
                cam.GetComponent<CameraMotor>().followY = true;
            }

            if (Mathf.Abs(cam.transform.position.y) >= Mathf.Abs(points[i].transform.position.y))
            {
                cam.GetComponent<CameraMotor>().followY = false;
            }
        }
    }
}
