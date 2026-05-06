using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform bulletSpawn;
    public float bulletSpeed = 30;
    public float bulletLifeTime = 3f;


    // Update is called once per frame
    void Update()
    {
        // Left mouse button to shoot
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            FireWeapon();
        }
        // Right mouse button to zoom
        // if (Input.GetKeyDown(KeyCode.Mouse1))
        // {
        //     Zoom();
        // }
    }
    private void FireWeapon()
    {
        // Instantiate the bullet at the spawn point
        GameObject bullet = Instantiate(bulletPrefab, bulletSpawn.position, Quaternion.identity);
        // Add velocity to the bullet
        bullet.GetComponent<Rigidbody>().AddForce(bulletSpawn.forward.normalized * bulletSpeed, ForceMode.Impulse);
         // Destroy the bullet after a certain time
        StartCoroutine(DestroyBulletAfterTime(bullet, bulletLifeTime));
    }

    private IEnumerator DestroyBulletAfterTime(GameObject bullet, float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(bullet);
    }


}
