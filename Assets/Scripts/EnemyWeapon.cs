using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWeapon : MonoBehaviour
{
    // floats
    private float timeBtwShots;

    // object refs
    public GameObject projectile;
    public GameObject firePoint;
    public AudioSource audioSource;
    public AudioClip shootClip;

    public void SetTBS(float fireRate)
    {
        timeBtwShots = fireRate;
    }

    public void OnShoot(float shots, float fireRate, float loopDelay)
    {
        // check whether to shoot
        if (timeBtwShots <= 0)
        {
            StartCoroutine(Fire(shots, fireRate, loopDelay));

            // reset time between shots
            timeBtwShots = fireRate;
        }
        else if (timeBtwShots > 0)
        {
            timeBtwShots -= Time.deltaTime;
        }
    }

    public virtual IEnumerator Fire(float shots, float fireRate, float loopDelay)
    {
        for (int i = 0; i < shots; i++)
        {
            projectile.transform.position = new Vector3(firePoint.transform.position.x,
            firePoint.transform.position.y, firePoint.transform.position.z);
            projectile.transform.rotation = transform.rotation;

            // spawn new projectile
            Instantiate(projectile);

            audioSource.PlayOneShot(shootClip);

            yield return new WaitForSeconds(loopDelay);
        }
    }
}
