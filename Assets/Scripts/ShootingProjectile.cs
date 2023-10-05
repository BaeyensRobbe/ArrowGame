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

    [Header("AK-47 powerup")]
    public GameObject akBulletPrefab;
    public float ak47ShootInterval;
    public float ak47Duration;
    private float timeSinceLastAk47Bullet = 0f;
    private bool isAKPowerup = false;

    [Space]

    [Header("NinjaStar Character")]
    public GameObject ninjaStarPrefab;
    public float ninjaStarInterval;
    public int numberOfStars;
    private float timeSinceLastNinjaStar = 0f;

    [Space]

    [Header("BombDropper Character")]
    public GameObject droppedBombPrefab;
    public float droppedBombInterval;
    private float timeSinceLastBombDrop = 0f;


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
                UnityEngine.Debug.Log("function is called wrong");
                Shoot(projectilePrefab);
                timeSinceLastShot = 0f; // Reset the timer
            }
        }

        if (PlayerPrefs.GetInt("Skin", 0) == 2)
        {
            timeSinceLastNinjaStar += Time.deltaTime;
            if (timeSinceLastNinjaStar >= ninjaStarInterval)
            {
                UnityEngine.Debug.Log("function is called right");
                Shoot(ninjaStarPrefab);
                timeSinceLastNinjaStar = 0f;
            }
        }

        if(PlayerPrefs.GetInt("Skin", 0) == 3)
        {
            timeSinceLastBombDrop += Time.deltaTime;
            if (timeSinceLastBombDrop >= droppedBombInterval)
            {
                DropBomb();
                timeSinceLastBombDrop = 0f;
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
        if (prefab == projectilePrefab)
        {
            GameObject newProjectile = Instantiate(prefab, firePoint.position, characterRotation);
            UnityEngine.Debug.Log("why is this even called?");
        }
        
        if (prefab == akBulletPrefab)
        {
            GameObject newProjectile = Instantiate(prefab, firePoint.position, characterRotation);
            GameObject newProjectile2 = Instantiate(prefab, firePoint.position, characterRotation * Quaternion.Euler(0f,0f,-20f));
            GameObject newProjectile3 = Instantiate(prefab, firePoint.position, characterRotation * Quaternion.Euler(0f, 0f, 20f));
        }
        if (prefab == ninjaStarPrefab)
        {
            for (int i = 0; i < numberOfStars; i++)
            {
                UnityEngine.Debug.Log("wwrong prefab is spawned smh?");
                float angle = i * (360f /  numberOfStars);
                float angleInRadians = angle * Mathf.Deg2Rad;

                Instantiate(prefab, transform.position, Quaternion.Euler(0f, 0f, angle));
            }
        }

       
    }

    private void DropBomb()
    {
        if (droppedBombPrefab == null)
        {
            UnityEngine.Debug.Log("Bombprefab is null");
        }
        GameObject bomb = Instantiate(droppedBombPrefab, transform.position, Quaternion.identity);
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
