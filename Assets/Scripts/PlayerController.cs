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
    public GameObject canister;
    public GameObject throwSpawn;

    [Header("Values")]
    public float speed = 5f;
    public float jumpVelocity = 12f;
    public int totalCoins = 0;

    [Header("Other")]
    [SerializeField] private LayerMask platformLayerMask;
    public bool isPaused;

    [Header("Health System")]
    public int health;
    public int maxHealth;
    public int numOfHearts;
    public List<Image> hearts;
    public Sprite fullHeart;
    public Sprite fullExtraHeart;
    public Sprite emptyHeart;

    // private variables
    private Vector2 move;
    private bool isComplete;
    private bool isGrounded;
    private float doubleJumpAmount;

    // checkpoints
    private bool checkpointActviated;
    private Vector3 checkpointLocation;

    // throwable
    private float lastThrow = 0;
    private float throwCoolDown = 2;
    private bool canThrow;

    // health upgrade
    private GameObject heartsHolder;
    private GameObject extraHeartsHolder;

    // shield upgrade
    private bool canShield;
    private GameObject shield;
    private Animator shieldAnimator;
    private float shieldUpTime = 0;
    private float shieldMaxTime = 3;
    private float shieldCooldown = 3;
    private float shieldLastUp = 0;
    private bool canRelease = false;
    private Slider shieldCooldownSlider;
    private GameObject shieldCooldownObj;

    bool IntToBool(int input)
    {
        if (input == 1)
            return true;
        else
            return false;
    }

    void Start()
    {
        // get components needed
        rb = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();
        
        // call SetXScale to set the direction player is facing
        SetXScale();

        // set coin counter to total coins
        totalCoins = PlayerPrefs.GetInt("CoinTotal", 0);
        coinCounter.SetText(totalCoins.ToString());

        // set game to not paused on start
        isPaused = false;
        pauseScreen.enabled = false;

        isComplete = false;

        doubleJumpAmount = 2;

        eHolder.SetActive(false);

        // checkpoints
        checkpointActviated = false;
        checkpointLocation = Vector3.zero;

        TimerController.instance.BeginTimer();

        canThrow = IntToBool(PlayerPrefs.GetInt("CanThrow", 0));

        // shield upgrade
        shield = GameObject.Find("ShieldHolder");
        shield.SetActive(false);
        shieldAnimator = shield.GetComponentInChildren<Animator>();
        canShield = IntToBool(PlayerPrefs.GetInt("CanShield", 0));
        shieldCooldownSlider = GameObject.Find("ShieldCooldown").GetComponent<Slider>();
        shieldCooldownObj = GameObject.Find("ShieldCooldown");
        if (canShield)
        {
            shieldCooldownObj.SetActive(true);
        }
        else
        {
            shieldCooldownObj.SetActive(false);
        }

        heartsHolder = GameObject.FindGameObjectWithTag("HeartsHolder");
        extraHeartsHolder = GameObject.FindGameObjectWithTag("ExtraHeartsHolder");
        if (IntToBool(PlayerPrefs.GetInt("HasHealthUpgrade", 0)))
        {
            // if has health upgrade
            heartsHolder.transform.localPosition = new Vector3(-80, 540, 0);
            extraHeartsHolder.SetActive(true);
            hearts.Add(GameObject.Find("ExtraHeart1").GetComponent<Image>());
            hearts.Add(GameObject.Find("ExtraHeart2").GetComponent<Image>());
            maxHealth = 7;
            health = maxHealth;
            numOfHearts = maxHealth;
        }
        else
        {
            heartsHolder.transform.localPosition = new Vector3(0, 540, 0);
            extraHeartsHolder.SetActive(false);
        }

        health = PlayerPrefs.GetInt("Health", health);
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

                if (canThrow)
                {
                    Throw();
                }

                if (canShield)
                {
                    Shield();
                }
            }

            if (isComplete && Input.GetKeyDown(KeyCode.E))
            {
                PlayerPrefs.SetInt("CoinTotal", totalCoins);

                sfxManager.audioSource.PlayOneShot(sfxManager.portalCip);

                TimerController.instance.EndTimer();
                PlayerPrefs.SetFloat("GameTime", TimerController.instance.elapsedTime);
                PlayerPrefs.Save();

                LevelComplete();
            }
        }

        Pause();

        // chech if game is over
        if (health <= 0)
        {
            Die();

            //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            //sfxManager.audioSource.PlayOneShot(sfxManager.deathClip);

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

    private void Shield()
    {
        if (Time.time - shieldLastUp >= shieldCooldown )
        {
            if (Input.GetKeyDown(KeyCode.S))
            {
                shieldUpTime = Time.time;
                shield.SetActive(true);
                canRelease = true;
            }

            if (Input.GetKeyUp(KeyCode.S) && canRelease || Time.time - shieldUpTime > shieldMaxTime && canRelease)
            {
                shieldLastUp = Time.time;
                canRelease = false;
                StartCoroutine(ShieldOffAnimate());
            }
        }

        if (Time.time - shieldLastUp <= shieldCooldown)
        {
            shieldCooldownSlider.value = Time.time - shieldLastUp;
        }
        else if (Time.time - shieldUpTime <= shieldMaxTime)
        {
            shieldCooldownSlider.value = 3 - (Time.time - shieldUpTime);
        }
        else
        {
            shieldCooldownSlider.value = 3;
        }
    }

    IEnumerator ShieldOffAnimate()
    {
        shieldAnimator.SetTrigger("Off");

        yield return new WaitForSeconds(0.2f);

        shield.SetActive(false);
    }

    private void Throw()
    {
        if (Input.GetKeyDown(KeyCode.Q) && Time.time >= lastThrow + throwCoolDown)
        {
            lastThrow = Time.time;

            if (transform.localScale.x == 1)
            {
                Debug.Log("0");
                canister.transform.localScale = new Vector3(1, 1, 1);
                Instantiate(canister, throwSpawn.transform.position, Quaternion.Euler(0, 0, 30));
            }
            else
            {
                Debug.Log("1");
                canister.transform.localScale = new Vector3(1, -1, 1);
                Instantiate(canister, throwSpawn.transform.position, Quaternion.Euler(0, 0, 150));
            }
        }
    }

    private void HealthSystem()
    {
        // make sure health doesn't go above the number of hearts
        if (health > numOfHearts)
        {
            health = numOfHearts;
            PlayerPrefs.SetInt("Health", health);
        }

        // set which hearts appear
        for (int i = 0; i < hearts.Count; i++)
        {
            if (i < health)
            {
                if (i < 5)
                {
                    hearts[i].sprite = fullHeart;
                }
                else
                {
                    hearts[i].sprite = fullExtraHeart;
                }
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

    private void Die()
    {
        PlayerPrefs.SetInt("Health", maxHealth);
        PlayerPrefs.SetFloat("GameTime", TimerController.instance.elapsedTime);
        PlayerPrefs.Save();

        if (checkpointActviated)
        {
            transform.position = checkpointLocation;
            health = maxHealth;
        }
        else
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        //sfxManager.audioSource.PlayOneShot(sfxManager.deathClip);
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
                // PAUSE GAME
                // turn on pause screen and set paused bool = true
                pauseScreen.enabled = true;
                isPaused = true;
                animator.SetBool("isMoving", false);

                TimerController.instance.EndTimer();

                Debug.Log("Paused");
            }
            else
            {
                // UNPAUSE GAME
                // turn off pause screen and set paused bool = false
                pauseScreen.enabled = false;
                isPaused = false;

                TimerController.instance.BeginTimer();

                Debug.Log("Unpaused");
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
        if (SceneManager.GetActiveScene().buildIndex + 1 > SceneManager.GetActiveScene().buildIndex)
        {
            StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex + 1));
        }
        else
        {
            StartCoroutine(LoadLevel(SceneManager.GetSceneByName("GameCompletedScreen").buildIndex));
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
            PlayerPrefs.SetInt("Health", health);
            PlayerPrefs.Save();
            sfxManager.audioSource.PlayOneShot(sfxManager.damageClip);
        }
        
        // heart pickup
        if (collision.tag == "Heart" && health < numOfHearts)
        {
            health++;
            PlayerPrefs.SetInt("Health", health);
            PlayerPrefs.Save();
            sfxManager.audioSource.PlayOneShot(sfxManager.heartPickupClip);
            Destroy(collision.gameObject);
        }
        
        // coin pickup
        if (collision.tag == "Coin")
        {
            totalCoins += 5;
            PlayerPrefs.SetInt("CoinTotal", totalCoins);
            PlayerPrefs.Save();
            coinCounter.SetText(totalCoins.ToString());
            sfxManager.audioSource.PlayOneShot(sfxManager.coinPickupClip);
            Destroy(collision.gameObject);
        }

        // restart
        if (collision.tag == "DeathBarrier")
        {
            Die();

            //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            //sfxManager.audioSource.PlayOneShot(sfxManager.deathClip);
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

        // checkpoint
        if (collision.tag == "Checkpoint")
        {
            if (checkpointLocation == collision.transform.position)
            {
                return;
            }

            checkpointActviated = true;
            checkpointLocation = collision.transform.position;
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
}
