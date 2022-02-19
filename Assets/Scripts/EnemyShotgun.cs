using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShotgun : EnemyWeapon
{
    public override IEnumerator Fire(float shots, float fireRate, float loopDelay)
    {
        for (int i = 0; i < shots; i++)
        {
            float angle = Random.Range(-45, 45);
            Debug.Log(angle);

            projectile.transform.position = new Vector3(firePoint.transform.position.x,
            firePoint.transform.position.y, firePoint.transform.position.z);
            projectile.transform.rotation = Quaternion.Euler(0, 0, angle + transform.rotation.z);

            // spawn new projectile
            Instantiate(projectile);

            audioSource.PlayOneShot(shootClip);

            yield return new WaitForSeconds(loopDelay);
        }
    }
}
