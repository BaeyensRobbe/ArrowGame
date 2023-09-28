using System.Collections;
using System.Diagnostics;
using UnityEngine;

public class ShootingProjectile : MonoBehaviour
{
    private Rigidbody2D rb;
    public GameObject projectilePrefab;
    public Transform firePoint;
    public float shootInterval = 5f; // Time between shots in seconds
    private float timeSinceLastShot = 0f;
    private float speed = 5f;


    public GameObject bombPrefab;
    public float bombInterval = 5f;
    private float timeSinceLastBomb = 0f;

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
                Shoot();
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
        
    }

    private void Shoot()
    {
        if (projectilePrefab == null || firePoint == null)
        {
            UnityEngine.Debug.LogWarning("Missing components in the Shooting script.");
            return;
        }

        // Calculate the direction based on the player's velocity
        Vector2 shootDirection = rb.velocity.normalized;

        // Instantiate a new projectile at the fire point's position

        Quaternion characterRotation = transform.rotation;
        characterRotation *= Quaternion.Euler(0f, 0f, -90f);
        GameObject newProjectile = Instantiate(projectilePrefab, firePoint.position, characterRotation);

       
    }

    private void DropBomb()
    {
        if (bombPrefab == null)
        {
            UnityEngine.Debug.Log("Bombprefab is null");
        }
        GameObject bomb = Instantiate(bombPrefab, transform.position, Quaternion.identity);
    }
}
