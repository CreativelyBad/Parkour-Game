using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShotgun : EnemyWeapon
{
    Quaternion rot;

    public override IEnumerator Fire(float shots, float fireRate, float loopDelay)
    {
        for (int i = 0; i < shots; i++)
        {
            float angle = Random.Range(-20, 20);

            Vector3 pos = firePoint.transform.position;

            if (transform.localScale.x == -1)
                rot = Quaternion.Euler(0, 0, angle + transform.localEulerAngles.z * -1);
            else
                rot = Quaternion.Euler(0, 0, angle + transform.localEulerAngles.z);


            // =====> OLD CODE <=====
            //projectile.transform.position = new Vector3(firePoint.transform.position.x,
            //firePoint.transform.position.y, firePoint.transform.position.z);
            //projectile.transform.rotation = Quaternion.Euler(0, 0, angle + transform.rotation.z);

            // spawn new projectile
            Instantiate(projectile, pos, rot);

            audioSource.PlayOneShot(shootClip);

            yield return new WaitForSeconds(loopDelay);
        }
    }
}
