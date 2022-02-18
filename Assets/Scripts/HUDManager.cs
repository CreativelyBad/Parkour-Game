using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HUDManager : MonoBehaviour
{
    private Canvas hudCanvas;

    private void Start()
    {
        hudCanvas = GetComponent<Canvas>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F1))
        {
            hudCanvas.enabled = !hudCanvas.enabled;
        }
    }
}
