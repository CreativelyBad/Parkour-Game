using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerController : MonoBehaviour
{
    [Header("References")]
    public Rigidbody2D rb;
    public BoxCollider2D boxCollider;
    public GameObject projectile;
    public GameObject firePoint;
    public GameObject gun;
    public GameObject grapplingHook;
    public TextMeshProUGUI coinCounter;

    [Header("Values")]
    public float speed = 5f;
    public float jumpVelocity = 12f;
    public float totalCoins = 0f;

    [Header("Other")]
    [SerializeField] private LayerMask platformLayerMask;

    [Header("Health System")]
    public int health;
    public int numOfHearts;
    public Image[] hearts;
    public Sprite fullHeart;
    public Sprite emptyHeart;

    // private variables
    private Vector2 move;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();
        SetXScale();
        coinCounter.SetText(totalCoins.ToString());
    }

    void Update()
    {
        Move();
        Jump();
        HealthSystem();

        if (grapplingHook.GetComponent<GrapplingHook>().isGrappling == false)
        {
            Shoot();
        }

        if (health <= 0)
        {
            GameOver();
        }

        QuitGame();
    }

    private void HealthSystem()
    {
        if (health > numOfHearts)
        {
            health = numOfHearts;
        }

        for (int i = 0; i < hearts.Length; i++)
        {
            if (i < health)
            {
                hearts[i].sprite = fullHeart;
            }
            else
            {
                hearts[i].sprite = emptyHeart;
            }

            if (i < numOfHearts)
            {
                hearts[i].enabled = true;
            }
            else
            {
                hearts[i].enabled = false;
            }
        }
    }

    private void GameOver()
    {
        Debug.Log("Game Over");
    }

    private void Move()
    {
        // movement
        float x = Input.GetAxis("Horizontal");

        move = new Vector2(x, 0);
        transform.Translate(move * speed * Time.deltaTime);

        // flip player
        if (x > 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
            SetXScale();
        }
        else if (x < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
            SetXScale();
        }
    }

    private void Jump()
    {
        // jump
        if (IsGrounded() && (Input.GetKeyDown(KeyCode.Space)))
        {
                rb.velocity = new Vector2(0, jumpVelocity);
        }
    }

    private bool IsGrounded()
    {
        float extraHeightTest = 0.1f;
        Color rayColor;

        RaycastHit2D raycastHit = Physics2D.Raycast(boxCollider.bounds.center, Vector2.down, 
            boxCollider.bounds.extents.y + extraHeightTest, platformLayerMask);

        if (raycastHit.collider != null)
        {
            rayColor = Color.green;
        }
        else
        {
            rayColor = Color.red;
        }

        Debug.DrawRay(boxCollider.bounds.center, Vector2.down * (boxCollider.bounds.extents.y + extraHeightTest));

        return raycastHit.collider != null;
    }

    private void Shoot()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) || Input.GetKeyDown(KeyCode.K))
        {
            projectile.transform.position = new Vector3(firePoint.transform.position.x, 
                firePoint.transform.position.y, firePoint.transform.position.z);
            projectile.transform.rotation = gun.transform.rotation;

            Instantiate(projectile);
        }
    }

    private void SetXScale()
    {
        projectile.GetComponent<Projectile>().x = transform.localScale.x;
        gun.GetComponent<Gun>().x = transform.localScale.x;
        grapplingHook.GetComponent<GrapplingHook>().x = transform.localScale.x;
    }

    private void QuitGame()
    {
        if (Input.GetKey(KeyCode.P))
        {
            Application.Quit();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //lose health when shot
        if (collision.tag == "EnemyProjectile")
        {
            health--;
        }
        
        // heart pickup
        if (collision.tag == "Heart" && health < numOfHearts)
        {
            health++;
            Destroy(collision.gameObject);
        }
        
        // coin pickup
        if (collision.tag == "Coin")
        {
            totalCoins += 5;
            coinCounter.SetText(totalCoins.ToString());
            Destroy(collision.gameObject);
        }
    }
}
