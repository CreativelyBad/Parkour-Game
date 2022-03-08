using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public Rigidbody2D rb;
    public CircleCollider2D cirleCollider;
    public float projectileSpeed = 20f;
    public float x = 0f;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        // set velocity
        rb.velocity = rb.GetRelativeVector(Vector2.right * projectileSpeed * x);

        // destroy object after 1 second
        GameObject.Destroy(gameObject, 1f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // destroy object if it hits the enemy or ground
        if (collision.tag == "Ground" || collision.tag == "Enemy")
        {
            GameObject.Destroy(gameObject);
        }

        if (collision.tag == "Canister")
        {
            collision.GetComponent<ThrowableExplosive>().Explode();
        }
    }
}
