using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrapplingHook : MonoBehaviour
{
    public bool isGrappling = false;
    public bool hasGrappled = false;
    public Transform player;
    public Transform firePoint;
    public float x;
    public float shootSpeed = 30f;

    [Header("Positions")]
    public Vector3 grapplingPosition;
    public float grapplingRotation = 0.0f;
    public Vector3 notGrapplingPosition;
    public float notGrapplingRotation = -118.7f;

    [Header("References")]
    public Rigidbody2D rb;
    public BoxCollider2D boxCollider;
    public DistanceJoint2D distanceJoint;

    private void Start()
    {
        rb.isKinematic = true;
        player.GetComponent<DistanceJoint2D>();
        distanceJoint.enabled = false;
    }

    private void Update()
    {
        GrapplingInput();
        CheckDistance();
    }

    private void CheckDistance()
    {
        float distance = Vector3.Distance(player.transform.position, transform.position);

        if (distance > 10f)
        {
            distanceJoint.enabled = false;

            rb.isKinematic = true;
            rb.velocity = new Vector2(0, 0);
            hasGrappled = false;
            isGrappling = false;
            IsGrappling();
        }
    }

    private void GrapplingInput()
    {
        // start grapple
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            isGrappling = true;
            IsGrappling();
        }

        // end grapple
        if (Input.GetKeyUp(KeyCode.Mouse1))
        {
            distanceJoint.enabled = false;

            isGrappling = false;
            hasGrappled = false;
            IsGrappling();
            rb.isKinematic = true;
        }

        // shoot grapple
        if (Input.GetKeyDown(KeyCode.Mouse0) && isGrappling == true)
        {
            hasGrappled = true;
            transform.SetParent(null);
            rb.isKinematic = false;
            rb.velocity = rb.GetRelativeVector(Vector2.right * shootSpeed * x);
        }

        // end grapple shot
        if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            distanceJoint.enabled = false;

            rb.isKinematic = true;
            rb.velocity = new Vector2(0, 0);
            isGrappling = false;
            hasGrappled = false;
            IsGrappling();
        }

        if (Input.GetKeyDown(KeyCode.LeftControl) && distanceJoint.enabled == true)
        {
            hasGrappled = false;
            player.transform.position = transform.position;
            IsGrappling();
            distanceJoint.enabled = false;
        }
    }

    private void IsGrappling()
    {
        if (isGrappling)
        {
            transform.SetParent(firePoint);

            transform.localPosition = grapplingPosition;
            transform.localRotation = Quaternion.Euler(0, 0, grapplingRotation);

            transform.localScale = new Vector3(1, 1, 1);
        }
        else if (!isGrappling)
        {
            transform.SetParent(player);

            transform.localPosition = notGrapplingPosition;
            transform.rotation = Quaternion.Euler(0, 0, notGrapplingRotation * x);

            transform.localScale = new Vector3(1, 1, 1);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Ground" && hasGrappled == true)
        {
            rb.isKinematic = true;
            isGrappling = false;
            rb.velocity = new Vector2(0, 0);

            distanceJoint.enabled = true;
            distanceJoint.connectedAnchor = transform.position;
            distanceJoint.anchor = firePoint.transform.localPosition;
        }
    }
}
