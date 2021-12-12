using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    public Rigidbody2D rb;
    public CircleCollider2D circleCollider;
    public float projectileSpeed = 20f;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        // set velocity
        rb.velocity = rb.GetRelativeVector(Vector2.right * projectileSpeed);

        // destroy object after 1 second
        GameObject.Destroy(gameObject, 1f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // destroy object if it hits the player or ground
        if (collision.tag == "Ground" || collision.tag == "Player")
        {
            GameObject.Destroy(gameObject);
        }
    }
}
