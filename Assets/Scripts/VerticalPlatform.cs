using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VerticalPlatform : MonoBehaviour
{
    public PlatformEffector2D effector;
    private float waitTime;
    private float startWaitTime = 0.1f;

    private void Start()
    {
        effector = GetComponent<PlatformEffector2D>();
    }

    private void Update()
    {
        // reset wait time and rotaional offset when left shift is let go
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            waitTime = startWaitTime;
            effector.rotationalOffset = 0f;
        }

        // when left shift is held down wait then move through platform
        if (Input.GetKey(KeyCode.LeftShift))
        {
            if (waitTime <= 0)
            {
                effector.rotationalOffset = 180f;
                waitTime = startWaitTime;
            }
            else
            {
                waitTime -= Time.deltaTime;
            }
        }
    }
}
