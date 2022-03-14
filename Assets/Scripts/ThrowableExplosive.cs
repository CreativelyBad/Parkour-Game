using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class ThrowableExplosive : MonoBehaviour
{
    public Sprite[] canisters;
    private SpriteRenderer spriteRenderer;
    private Rigidbody2D rb;
    public CircleCollider2D circleCollider;
    private CapsuleCollider2D capsuleCollider;
    public float force;
    private GameObject player;
    public GameObject explosion;
    private Animator anim;
    public Color[] lightColours;
    public GameObject cansiterLight;
    private int selection;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        capsuleCollider = GetComponent<CapsuleCollider2D>();
        circleCollider.enabled = false;

        rb = GetComponent<Rigidbody2D>();
        rb.velocity = transform.TransformDirection(Vector2.right * force);

        spriteRenderer = GetComponent<SpriteRenderer>();
        selection = Random.Range(0, 4);
        spriteRenderer.sprite = canisters[selection];
        cansiterLight.GetComponent<Light2D>().color = lightColours[selection];

        anim = explosion.GetComponent<Animator>();
    }

    private void Update()
    {
        explosion.transform.rotation = Quaternion.Euler(0, 0, transform.rotation.z);

        if (player.transform.localScale.x == 1)
        {
            explosion.GetComponent<SpriteRenderer>().flipY = false;
        }
        else
        {
            explosion.GetComponent<SpriteRenderer>().flipY = true;
        }

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
        cansiterLight.SetActive(false);
        GetComponent<SpriteRenderer>().enabled = false;
        anim.SetTrigger("Explode");

        circleCollider.enabled = true;

        yield return new WaitForSeconds(0.5f);

        Destroy(gameObject, 0.1f);
    }
}
