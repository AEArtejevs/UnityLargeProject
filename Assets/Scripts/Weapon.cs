using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public Camera playerCamera;

    [Header("Shooting")]

    public bool isShooting, readyToShoot;
    bool allowReset = true;
    public float shootingDelay = 2f;

    // Burst
    public int bulletsPerBurst = 3;
    public int burstBulletsLeft = 0;

    // Spread
    public float spreadIntensity;

    // Bullet
    public GameObject bulletPrefab;
    public Transform bulletSpawn;
    public float bulletSpeed = 30;
    public float bulletLifeTime = 3f;

    public enum ShootingMode
    {
        Single,
        Burst,
        Auto
    }
    public ShootingMode currentShootingMode;

    private void Awake()
    {
        readyToShoot = true;
        burstBulletsLeft = bulletsPerBurst;
    }

    // Update is called once per frame
    void Update()
    {
        if (currentShootingMode == ShootingMode.Auto)
        {
            // Hold donw left mouse button to shoot
            isShooting = Input.GetKey(KeyCode.Mouse0); // GetKey - is used for holding button
        }
        else if (currentShootingMode == ShootingMode.Single || 
            currentShootingMode == ShootingMode.Burst){
            // Click left mouse button to shoot
            isShooting = Input.GetKeyDown(KeyCode.Mouse0); // GetKeyDown - is used for single clicks
        }

        if (readyToShoot && isShooting)
        {
            burstBulletsLeft = bulletsPerBurst;
            FireWeapon();
        }
    }
    private void FireWeapon()
    {
        readyToShoot = false; // can't shoot until the previous shot is finished

        Vector3 shootingDirection = CalculateDirectionAndSpread().normalized;

        // Instantiate the bullet at the spawn point
        GameObject bullet = Instantiate(bulletPrefab, bulletSpawn.position, Quaternion.identity);

        // Point the bullet in the shooting direction
        bullet.transform.forward = shootingDirection; 

        // Shoot the bullet, applying force to it
        bullet.GetComponent<Rigidbody>().AddForce(shootingDirection * bulletSpeed, ForceMode.Impulse);

         // Destroy the bullet after a certain time
        StartCoroutine(DestroyBulletAfterTime(bullet, bulletLifeTime));

        // Checking if we are done shooting
        if(allowReset)
        {
            Invoke("ResetShoot", shootingDelay);
            allowReset = false;
        }

        // Burst shooting logic
        if (currentShootingMode == ShootingMode.Burst && burstBulletsLeft > 1) // We aready shot one bullet, so we check if there are more bullets left in the burst
        {
            burstBulletsLeft--;
            Invoke("FireWeapon", shootingDelay / bulletsPerBurst); // Fire the next bullet in the burst after a short delay
        }

    }

    private void ResetShoot()
    {
        readyToShoot = true;
        allowReset = true;
    }

    public Vector3 CalculateDirectionAndSpread()
    {
        // Get the direction from the camera to the mouse position
        Ray ray = playerCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0)); // Ray from the center of the screen
        RaycastHit hit;
        Vector3 targetPoint;

        if (Physics.Raycast(ray, out hit))
        {
            // Hiting something, so we shoot towards the hit point
            targetPoint = hit.point;
        }
        else
        {
            // Shooting at air
            targetPoint = ray.GetPoint(100);
        }

        Vector3 direction = targetPoint - bulletSpawn.position;

        float spreadX = Random.Range(-spreadIntensity, spreadIntensity);
        float spreadY = Random.Range(-spreadIntensity, spreadIntensity);

        // Return the direction with added spread
        return direction + new Vector3(spreadX, spreadY, 0);

    }
    private IEnumerator DestroyBulletAfterTime(GameObject bullet, float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(bullet);
    }


}
