using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowableExplosive : MonoBehaviour
{
    public Sprite[] canisters;
    private SpriteRenderer spriteRenderer;
    private Rigidbody2D rb;
    public CircleCollider2D circleCollider;
    private CapsuleCollider2D capsuleCollider;
    public float force;
    private GameObject player;
    private GameObject enemy;
    public GameObject explosion;
    private Animator anim;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        capsuleCollider = GetComponent<CapsuleCollider2D>();
        circleCollider.enabled = false;

        rb = GetComponent<Rigidbody2D>();
        rb.velocity = transform.TransformDirection(Vector2.right * force);

        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = canisters[Random.Range(0, 4)];

        anim = explosion.GetComponent<Animator>();
    }

    private void Update()
    {
        explosion.transform.rotation = Quaternion.Euler(0, 0, transform.rotation.z);

        StartCoroutine(ExplodeWait());
    }

    IEnumerator ExplodeWait()
    {
        yield return new WaitForSeconds(3);

        Explode();
    }

    public void Explode()
    {
        StartCoroutine(ExplodeAnimate());
    }

    IEnumerator ExplodeAnimate()
    {
        GetComponent<SpriteRenderer>().enabled = false;
        anim.SetTrigger("Explode");

        circleCollider.enabled = true;

        yield return new WaitForSeconds(0.5f);

        Destroy(gameObject, 0.1f);
    }
}
