using System.Collections;
using System.Diagnostics;
using UnityEngine;

public class ShootingProjectile : MonoBehaviour
{
    private Rigidbody2D rb;
    [Header("General")]
    public Transform firePoint;

    [Space]

    [Header("Bullet Character")]
    public GameObject projectilePrefab;
    public float shootInterval = 5f;
    private float timeSinceLastShot = 0f;
    private float speed = 5f;

    [Space]

    [Header("Bomb Character")]
    public GameObject bombPrefab;
    public float bombInterval = 5f;
    private float timeSinceLastBomb = 0f;

    [Space]

    [Header("AK-47 powerup")]
    public GameObject akBulletPrefab;
    public float ak47ShootInterval;
    public float ak47Duration;
    private float timeSinceLastAk47Bullet = 0f;
    private bool isAKPowerup = false;
    


    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (PlayerPrefs.GetInt("Skin", 0) == 1)
        {
            timeSinceLastShot += Time.deltaTime;

            // Check if it's time to shoot
            if (timeSinceLastShot >= shootInterval)
            {
                Shoot(projectilePrefab);
                timeSinceLastShot = 0f; // Reset the timer
            }
        }

        if(PlayerPrefs.GetInt("Skin", 0) == 4)
        {
            timeSinceLastBomb += Time.deltaTime;
            if (timeSinceLastBomb >= bombInterval)
            {
                DropBomb();
                timeSinceLastBomb = 0f;
            }
        }

        if (isAKPowerup)
        {
            timeSinceLastAk47Bullet += Time.deltaTime;
            if (timeSinceLastAk47Bullet >= ak47ShootInterval)
            {
                Shoot(akBulletPrefab);
                timeSinceLastAk47Bullet = 0f;
            }
        }
        
    }

    private void Shoot(GameObject prefab)
    {
        if (prefab == null || firePoint == null)
        {
            UnityEngine.Debug.LogWarning("Missing components in the Shooting script.");
            return;
        }

        // Calculate the direction based on the player's velocity
        Vector2 shootDirection = rb.velocity.normalized;

        // Instantiate a new projectile at the fire point's position

        Quaternion characterRotation = transform.rotation;
        characterRotation *= Quaternion.Euler(0f, 0f, -90f);
        
        GameObject newProjectile = Instantiate(prefab, firePoint.position, characterRotation);
        if (prefab == akBulletPrefab)
        {
            GameObject newProjectile2 = Instantiate(prefab, firePoint.position, characterRotation * Quaternion.Euler(0f,0f,-20f));
            GameObject newProjectile3 = Instantiate(prefab, firePoint.position, characterRotation * Quaternion.Euler(0f, 0f, 20f));
        }

       
    }

    private void DropBomb()
    {
        if (bombPrefab == null)
        {
            UnityEngine.Debug.Log("Bombprefab is null");
        }
        GameObject bomb = Instantiate(bombPrefab, transform.position, Quaternion.identity);
    }

    public void AKPowerupActivate()
    {
        isAKPowerup = true;
        StartCoroutine(DeactivateAfterTime(ak47Duration));
    }

    public void AkPowerUpDeactivate()
    {
        isAKPowerup = false;
    }

    private IEnumerator DeactivateAfterTime(float duration)
    {
        yield return new WaitForSeconds(duration);
        isAKPowerup = false;
    }
}
