using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // floats
    private float timeBtwShots;
    public float startTimeBtwShots;
    public float enemyHealth;
    public float maxEnemyHealth = 1;

    // object refs
    public GameObject projectile;
    public GameObject firePoint;
    public GameObject gun;
    public GameObject player;

    private bool isInFrame;

    private void Start()
    {
        timeBtwShots = startTimeBtwShots;
        enemyHealth = maxEnemyHealth;
        isInFrame = false;
    }

    private void Update()
    {
        if (!player.GetComponent<PlayerController>().isPaused && isInFrame)
        {
            Aim();

            // check whether to shoot
            if (timeBtwShots <= 0)
            {
                Shoot();
            }
            else if (timeBtwShots > 0)
            {
                timeBtwShots -= Time.deltaTime;
            }

            // check if enemy is dead
            if (enemyHealth <= 0f)
            {
                GameObject.Destroy(gameObject);
            }
        }
    }

    private void Aim()
    {
        // set target to players position
        Vector3 targ = player.transform.position;
        targ.z = 0f;
        
        // get guns position
        Vector3 objectPos = gun.transform.position;

        targ.x = targ.x - objectPos.x;
        targ.y = targ.y - objectPos.y;

        // set guns rotation
        float angle = Mathf.Atan2(targ.y, targ.x) * Mathf.Rad2Deg;
        gun.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
    }

    private void Shoot()
    {
        // set projectile position on spawn
        projectile.transform.position = new Vector3(firePoint.transform.position.x,
            firePoint.transform.position.y, firePoint.transform.position.z);
        projectile.transform.rotation = gun.transform.rotation;

        // spawn new projectile
        Instantiate(projectile);

        // reset time between shots
        timeBtwShots = startTimeBtwShots;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // take projectile damage
        if (collision.tag == "PlayerProjectile")
        {
            enemyHealth--;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "MainCamera")
        {
            isInFrame = false;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "MainCamera")
        {
            isInFrame = true;
        }
    }
}
