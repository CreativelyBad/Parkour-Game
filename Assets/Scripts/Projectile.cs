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
        rb.velocity = rb.GetRelativeVector(Vector2.right * projectileSpeed * x);

        GameObject.Destroy(gameObject, 1f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Ground" || collision.tag == "Enemy")
        {
            GameObject.Destroy(gameObject);
        }
    }
}
