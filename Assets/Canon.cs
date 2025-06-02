using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon : MonoBehaviour
{
    
    public Transform firePoint;
    public bool isAiming = false;
    public float bulletSpeed = 10f;
    public float shootingInterval = 1f;
    public float initialDelay = 0f;
    private float timeSinceLastShot = 0f;
    public bool isShoot = true;
    public bool isFatal = false;
    public GameObject playerBig;
    public GameObject playerSmall;
    public GameObject bulletPrefab;
    public GameObject bulletFatalPrefab;
    

    void Start()
    {
        timeSinceLastShot = -initialDelay+shootingInterval;
        if (playerBig == null)
            playerBig = GameObject.Find("PlayerBig");
        if (playerSmall == null)
            playerSmall = GameObject.Find("PlayerSmall");
        if (isFatal)
            bulletPrefab = bulletFatalPrefab;
    }

    void Update()
    {
        if (playerBig == null)
            playerBig = GameObject.Find("PlayerBig");
        if (playerSmall == null)
            playerSmall = GameObject.Find("PlayerSmall");
        if (isShoot)
        {
            if (timeSinceLastShot >= shootingInterval)
            {
                ShootBullet();
                timeSinceLastShot = 0f;
            }
            timeSinceLastShot += Time.deltaTime;
            if (playerBig != null)
                if (playerBig.activeSelf && isAiming)
            {
                Vector2 directionToPlayer = playerBig.transform.position - transform.position;
                float angle = Mathf.Atan2(directionToPlayer.y, directionToPlayer.x) * Mathf.Rad2Deg;
                transform.rotation = Quaternion.Euler(0f, 0f, angle - 90);
            }
            if (playerSmall != null)
                if (playerSmall.activeSelf && isAiming)
            {
                Vector2 directionToPlayer = playerSmall.transform.position - transform.position;
                float angle = Mathf.Atan2(directionToPlayer.y, directionToPlayer.x) * Mathf.Rad2Deg;
                transform.rotation = Quaternion.Euler(0f, 0f, angle - 90);
            }
        }
    }

    void ShootBullet()
    {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Rigidbody2D bulletRb = bullet.GetComponent<Rigidbody2D>();
        bulletRb.velocity = firePoint.up * bulletSpeed;
    }
    public void ToggleShooting()
    {
        isShoot = !isShoot;
    }
    public void EnableShooting()
    {
        isShoot = true;
    }
    public void DisableShooting()
    {
        isShoot = false;
    }
}
