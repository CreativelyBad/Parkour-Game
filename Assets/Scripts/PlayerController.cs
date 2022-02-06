using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
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
    public Canvas pauseScreen;
    public GameObject groundCheck;
    public Animator animator;
    public GameObject eHolder;
    public Animator sceneTransitionAnim;
    public SFXManager sfxManager;

    [Header("Values")]
    public float speed = 5f;
    public float jumpVelocity = 12f;
    public float totalCoins = 0f;

    [Header("Other")]
    [SerializeField] private LayerMask platformLayerMask;
    public bool isPaused;

    [Header("Health System")]
    public int health;
    public int numOfHearts;
    public Image[] hearts;
    public Sprite fullHeart;
    public Sprite emptyHeart;

    // private variables
    private Vector2 move;
    private bool isComplete;
    private bool isGrounded;
    private float doubleJumpAmount;

    void Start()
    {
        // get components needed
        rb = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();
        
        // call SetXScale to set the direction player is facing
        SetXScale();

        // set coin counter to 0
        coinCounter.SetText(totalCoins.ToString());

        // set game to not paused on start
        isPaused = false;
        pauseScreen.enabled = false;

        isComplete = false;

        doubleJumpAmount = 2;

        eHolder.SetActive(false);
    }

    void Update()
    {
        // check whether or not game is paused
        if (!isPaused)
        {
            // basic gameplay functions
            Move();
            Jump();
            HealthSystem();

            // check whether player is grappling
            // if not let player shoot
            if (grapplingHook.GetComponent<GrapplingHook>().isGrappling == false)
            {
                Shoot();
            }

            if (isComplete && Input.GetKeyDown(KeyCode.E))
            {
                LevelComplete();
                sfxManager.audioSource.PlayOneShot(sfxManager.portalCip);
            }
        }

        Pause();

        // chech if game is over
        if (health <= 0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            sfxManager.audioSource.PlayOneShot(sfxManager.deathClip);

            //GameOver("GameOverScreen");
        }

        if (isComplete)
        {
            if (transform.localScale.x == 1)
            {
                eHolder.transform.localScale = new Vector3(1, 1, 1);
            }
            else
            {
                eHolder.transform.localScale = new Vector3(-1, 1, 1);
            }
        }
    }

    private void HealthSystem()
    {
        // make sure health doesn't go above the number of hearts
        if (health > numOfHearts)
        {
            health = numOfHearts;
        }

        // set which hearts appear
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

    private void GameOver(string screen)
    {
        // load game over screen
        SceneManager.LoadScene(screen);
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

        if (x == 0 || doubleJumpAmount != 2)
        {
            animator.SetBool("isMoving", false);
        }
        else
        {
            animator.SetBool("isMoving", true);
        }
    }

    private void Jump()
    {
        if (doubleJumpAmount == 0)
        {
            isGrounded = false;
        }
        else
        {
            isGrounded = true;
        }

        // check if grounded and if jump key is pressed
        if (isGrounded && (Input.GetKeyDown(KeyCode.Space)))
        {
            rb.velocity = new Vector2(0, jumpVelocity);
            doubleJumpAmount--;
            sfxManager.audioSource.PlayOneShot(sfxManager.jumpClip);
        }
    }

    private void Shoot()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            // set projectile position on spawn
            projectile.transform.position = new Vector3(firePoint.transform.position.x, 
                firePoint.transform.position.y, firePoint.transform.position.z);
            
            // set projectile rotation
            projectile.transform.rotation = gun.transform.rotation;

            // spawn in projectile
            Instantiate(projectile);

            sfxManager.audioSource.PlayOneShot(sfxManager.shootClip);
        }
    }

    private void Pause()
    {
        if (Input.GetKeyDown(KeyCode.P) || Input.GetKeyDown(KeyCode.Escape))
        {
            if (!isPaused)
            {
                // turn on pause screen and set paused bool = true
                pauseScreen.enabled = true;
                isPaused = true;
                animator.SetBool("isMoving", false);
            }
            else
            {
                // turn off pause screen and set paused bool = false
                pauseScreen.enabled = false;
                isPaused = false;
            }
        }
    }

    private void SetXScale()
    {
        // set x variable in other objects to the players current x scale
        projectile.GetComponent<Projectile>().x = transform.localScale.x;
        gun.GetComponent<Gun>().x = transform.localScale.x;
        grapplingHook.GetComponent<GrapplingHook>().x = transform.localScale.x;
    }

    private void LevelComplete()
    {
        // load next level
        if (SceneManager.GetActiveScene().buildIndex + 1 < 12)
        {
            StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex + 1));

            //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
        else
        {
            SceneManager.LoadScene("MenuScreen");
        }
    }

    IEnumerator LoadLevel(int levelIndex)
    {
        sceneTransitionAnim.SetTrigger("Start");

        yield return new WaitForSeconds(1);

        SceneManager.LoadScene(levelIndex);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //lose health when shot
        if (collision.tag == "EnemyProjectile")
        {
            health--;
            sfxManager.audioSource.PlayOneShot(sfxManager.damageClip);
        }
        
        // heart pickup
        if (collision.tag == "Heart" && health < numOfHearts)
        {
            health++;
            sfxManager.audioSource.PlayOneShot(sfxManager.heartPickupClip);
            Destroy(collision.gameObject);
        }
        
        // coin pickup
        if (collision.tag == "Coin")
        {
            totalCoins += 5;
            coinCounter.SetText(totalCoins.ToString());
            sfxManager.audioSource.PlayOneShot(sfxManager.coinPickupClip);
            Destroy(collision.gameObject);
        }

        // restart
        if (collision.tag == "DeathBarrier")
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            sfxManager.audioSource.PlayOneShot(sfxManager.deathClip);
            //health--;
        }

        // complete level
        if (collision.tag == "Portal")
        {
            isComplete = true;
            eHolder.SetActive(true);
        }

        // pickup grappling hook
        if (collision.tag == "GrapplingHook")
        {
            Destroy(collision.gameObject);
            grapplingHook.GetComponent<GrapplingHook>().canGrapple = true;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Platform" || collision.tag == "Ground")
        {
            doubleJumpAmount = 2;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Portal")
        {
            isComplete = false;
            eHolder.SetActive(false);
        }
    }

    //private void OnTriggerExit2D(Collider2D collision)
    //{
    //    if (collision.tag == "Platform" || collision.tag == "Ground")
    //    {
    //        doubleJumpAmount--;
    //    }
    //}

    //private bool IsGrounded()
    //{
    //    float extraHeightTest = 0.1f;
    //    Color rayColor;

    //    // cast ray to find if player is on ground
    //    RaycastHit2D raycastHit = Physics2D.Raycast(boxCollider.bounds.center, Vector2.down, 
    //        boxCollider.bounds.extents.y + extraHeightTest, platformLayerMask);

    //    // set colour based on if player is grounded or not
    //    if (raycastHit.collider != null)
    //    {
    //        rayColor = Color.green;
    //    }
    //    else
    //    {
    //        rayColor = Color.red;
    //    }

    //    // draw visible ray
    //    Debug.DrawRay(boxCollider.bounds.center, Vector2.down * (boxCollider.bounds.extents.y + extraHeightTest));

    //    return raycastHit.collider != null;
    //}
}
