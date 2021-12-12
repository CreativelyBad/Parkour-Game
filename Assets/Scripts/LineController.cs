using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineController : MonoBehaviour
{
    public LineRenderer lineRenderer;
    public GameObject firePoint;
    public GameObject ropeDraw;
    public GameObject grapplingHook;

    private void Update()
    {
        // control line renderer component
        if (grapplingHook.GetComponent<GrapplingHook>().hasGrappled)
        {
            lineRenderer.enabled = true;
            lineRenderer.SetPosition(0, firePoint.transform.position + new Vector3(0, 0, -0.1f));
            lineRenderer.SetPosition(1, ropeDraw.transform.position + new Vector3(0, 0, -0.1f));
        }
        else
        {
            lineRenderer.enabled = false;
        }
    }
}
