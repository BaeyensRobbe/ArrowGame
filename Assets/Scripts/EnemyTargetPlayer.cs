using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTargetPlayer : MonoBehaviour
{
    private Transform player;
    public float moveSpeed = 5f;
    private bool isReversingDirection = false;
    public int coinValue = 1;
    private CoinsController coinsController;
    public GameObject splashPrefab;
    private bool canMove = false;
    public float delayBeforeMoving = 1f;

    // Camera boundaries
    private float cameraMinX;
    private float cameraMaxX;
    private float cameraMinY;
    private float cameraMaxY;

    private ChainSpawner chainspawner;

    void Start()
    { 
        player = GameObject.FindGameObjectWithTag("Player").transform;

        // Calculate camera boundaries
        Camera mainCamera = Camera.main;
        float cameraOrthoSize = mainCamera.orthographicSize;
        float cameraAspect = mainCamera.aspect;
        float cameraWidth = cameraOrthoSize * cameraAspect;
        float cameraHeight = cameraOrthoSize;
        cameraMinX = mainCamera.transform.position.x - cameraWidth;
        cameraMaxX = mainCamera.transform.position.x + cameraWidth;
        cameraMinY = mainCamera.transform.position.y - cameraHeight;
        cameraMaxY = mainCamera.transform.position.y + cameraHeight;

        chainspawner = GameObject.Find("PowerUpSpawner").GetComponent<ChainSpawner>();

        StartCoroutine(DelayedStart(delayBeforeMoving));


    }

    void Update()
    {
        if (canMove)
        {
            Vector3 direction;
            if (isReversingDirection)
            {
                // Calculate the direction to move away from the player
                direction = (transform.position - player.position).normalized;
            }
            else
            {
                // Default behavior: move towards the player
                direction = (player.position - transform.position).normalized;
            }

            Vector3 newPosition = transform.position + direction * moveSpeed * Time.deltaTime;

            if (chainspawner.IsNotBlockedByUI(newPosition))
            {
                // Clamp the new position to stay within camera boundaries
                newPosition.x = Mathf.Clamp(newPosition.x, cameraMinX, cameraMaxX);
                newPosition.y = Mathf.Clamp(newPosition.y, cameraMinY, cameraMaxY);

                // Update the enemy's position
                transform.position = newPosition;
            }
            else
            {
                // The new position is blocked by UI in the vertical direction, so only move horizontally
                newPosition = transform.position + new Vector3(direction.x, 0, 0) * moveSpeed * Time.deltaTime;

                // Check if the new horizontal position is not blocked by UI elements
                if (chainspawner.IsNotBlockedByUI(newPosition))
                {
                    // Update the enemy's position in the horizontal direction
                    transform.position = newPosition;
                }
            }
        }
        
    }

    // Call this method to reverse the enemy's direction for 5 seconds
    public void ReverseDirection(float powerUpTime)
    {
        UnityEngine.Debug.Log("ReverseDirection called");
        if (!isReversingDirection)
        {
            StartCoroutine(ReverseDirectionForSeconds(powerUpTime));
        }
    }

    private IEnumerator ReverseDirectionForSeconds(float seconds)
    {
        isReversingDirection = true;
        yield return new WaitForSeconds(seconds);
        isReversingDirection = false;
    }

    public void EnemyDies()
    {
        UnityEngine.Debug.Log("Enemy dies");
        coinsController = GameObject.FindGameObjectWithTag("EnemySpawner").GetComponent<CoinsController>();
        coinsController.IncrementCoinCount(coinValue);

        // Instantiate the splash prefab
        GameObject splash = Instantiate(splashPrefab, transform.position, Quaternion.identity);

        // Access the enemy's SpriteRenderer component
        SpriteRenderer enemyRenderer = GetComponent<SpriteRenderer>();

        // Access the splash's SpriteRenderer component
        SpriteRenderer splashRenderer = splash.GetComponent<SpriteRenderer>();

        // Ensure both renderers are valid
        if (enemyRenderer != null && splashRenderer != null)
        {
            // Set the splash's color to match the enemy's color
            splashRenderer.color = enemyRenderer.color;
            UnityEngine.Debug.Log(enemyRenderer.color);
        }

        // Destroy the enemy
        Destroy(gameObject);
    }


    public void DestroyEnemy()
    {
        Destroy(gameObject);
    }

    private IEnumerator DelayedStart(float delayTime)
    {
        yield return new WaitForSeconds(delayTime); // Wait for 1.5 seconds.
        canMove = true; // Set the flag to allow movement after the delay.
    }
}
