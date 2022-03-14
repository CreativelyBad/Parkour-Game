using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyV2 : MonoBehaviour
{
    // floats
    [HideInInspector] public float enemyHealth;
    public float maxEnemyHealth = 1;

    public float fireRate;
    public float shots;
    public float loopDelay;

    // object refs
    public GameObject gun;
    [HideInInspector] public GameObject player;

    private bool isInFrame;

    private void Start()
    {
        enemyHealth = maxEnemyHealth;
        isInFrame = false;

        player = GameObject.FindGameObjectWithTag("Player");

        gun.GetComponent<EnemyWeapon>().SetTBS(fireRate);
    }

    private void Update()
    {
        if (!player.GetComponent<PlayerController>().isPaused)
        {
            Aim();

            if (isInFrame)
            {
                gun.GetComponent<EnemyWeapon>().OnShoot(shots, fireRate, loopDelay);
            }
        }

        // check if enemy is dead
        if (enemyHealth <= 0f)
        {
            GameObject.Destroy(gameObject);
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

        if (collision.tag == "CanisterColl")
        {
            Destroy(gameObject);
        }
    }
}
