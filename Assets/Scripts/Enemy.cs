using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private float timeBtwShots;
    public float startTimeBtwShots;
    public float enemyHealth;
    public float maxEnemyHealth = 1;

    public GameObject projectile;
    public GameObject firePoint;
    public GameObject gun;
    public GameObject player;

    private void Start()
    {
        timeBtwShots = startTimeBtwShots;
        enemyHealth = maxEnemyHealth;
    }

    private void Update()
    {
        Aim();

        if (timeBtwShots <= 0)
        {
            Shoot();
        }
        else if (timeBtwShots > 0)
        {
            timeBtwShots -= Time.deltaTime;
        }

        if (enemyHealth <= 0f)
        {
            GameObject.Destroy(gameObject);
        }
    }

    private void Aim()
    {
        Vector3 targ = player.transform.position;
        targ.z = 0f;
        Vector3 objectPos = gun.transform.position;
        targ.x = targ.x - objectPos.x;
        targ.y = targ.y - objectPos.y;
        float angle = Mathf.Atan2(targ.y, targ.x) * Mathf.Rad2Deg;
        gun.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
    }

    private void Shoot()
    {
        projectile.transform.position = new Vector3(firePoint.transform.position.x,
            firePoint.transform.position.y, firePoint.transform.position.z);
        projectile.transform.rotation = gun.transform.rotation;

        Instantiate(projectile);

        timeBtwShots = startTimeBtwShots;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "PlayerProjectile")
        {
            enemyHealth--;
        }
    }
}
