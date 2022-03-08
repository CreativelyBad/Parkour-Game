using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowableExplosive : MonoBehaviour
{
    public Sprite[] canisters;
    private SpriteRenderer spriteRenderer;
    private Rigidbody2D rb;
    private CircleCollider2D circleCollider;
    public float force;
    private GameObject player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        circleCollider = GetComponent<CircleCollider2D>();
        circleCollider.enabled = false;

        rb = GetComponent<Rigidbody2D>();
        rb.velocity = transform.TransformDirection(Vector2.right * force);

        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = canisters[Random.Range(0, 4)];
    }

    private void Update()
    {
        StartCoroutine(ExplodeWait());
    }

    IEnumerator ExplodeWait()
    {
        yield return new WaitForSeconds(3);

        Explode();
    }

    public void Explode()
    {
        circleCollider.enabled = true;

        Destroy(gameObject);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            Destroy(collision.gameObject);
        }
    }
}
