using System.Diagnostics;
using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour
{
    public GameObject objectToSpawn; // The object to spawn
    public float initialSpawnRate = 3f; // The initial rate at which to spawn the objects
    public float maxSpawnRate = 1f; // The maximum spawn rate
    public float timeToReachMaxRate = 60f; // The time it takes to reach the maximum spawn rate
    private float timer; // A timer to control the spawn rate
    private float elapsedTime;
    public float minDistanceToPlayer;
    private ChainSpawner chainSpawner;

    [SerializeField]
    private Color[] randomColors;

    void Start()
    {
        chainSpawner = GameObject.Find("PowerUpSpawner").GetComponent<ChainSpawner>();
        ResetSpawnRate();
        StartCoroutine(SpawnObjects());

    }

    /*void Update()
    {
        // Increment the timer
        timer += Time.deltaTime;

        // Calculate a dynamic spawn rate based on game time
        float dynamicSpawnRate = Mathf.Lerp(initialSpawnRate, maxSpawnRate, Time.deltaTime / timeToReachMaxRate); // Adjust 600f for desired time scaling

        // Check if it's time to spawn an object
        if (timer >= dynamicSpawnRate)
        {
            timer = 0f;
            SpawnEnemyWithMinDistance();
        }
    }*/

    private IEnumerator SpawnObjects()
    {
        while (true) // Infinite loop to keep spawning objects
        {
            // Calculate a dynamic spawn rate based on elapsed time
            float dynamicSpawnRate = Mathf.Lerp(initialSpawnRate, maxSpawnRate, elapsedTime / timeToReachMaxRate);

            // Spawn objects based on the dynamic spawn rate
            SpawnEnemyWithMinDistance();

            // Wait for the inverse of the dynamic spawn rate
            float waitTime = 1f / dynamicSpawnRate;
            yield return new WaitForSeconds(waitTime);

            // Update the elapsed time
            elapsedTime += waitTime;
        }
    }

    public void ResetTimer()
    {
        timer = 0f;
    }

    public void ResetSpawnRate()
    {
        elapsedTime = 0f; // Reset the elapsed time to reset the spawn rate
    }

    private void SpawnEnemyWithMinDistance()
    {
        // Get the main camera
        Camera mainCamera = Camera.main;
        if (mainCamera == null)
        {
            UnityEngine.Debug.LogError("Main camera not found. Make sure you have a camera tagged as 'MainCamera' in your scene.");
            return;
        }

        Vector3 playerPosition = GameObject.FindGameObjectWithTag("Player").transform.position;

        // Calculate the camera's bounds
        float cameraHeight = 2f * mainCamera.orthographicSize;
        float cameraWidth = cameraHeight * mainCamera.aspect;
        float leftBound = mainCamera.transform.position.x - cameraWidth / 2f;
        float rightBound = mainCamera.transform.position.x + cameraWidth / 2f;
        float topBound = mainCamera.transform.position.y + cameraHeight / 2f;
        float bottomBound = mainCamera.transform.position.y - cameraHeight / 2f;

        Vector3 randomPosition = Vector3.zero;
        bool validSpawnPointFound = false;

        // Keep generating random positions until a suitable one is found
        while (!validSpawnPointFound)
        {
            randomPosition = new Vector3(UnityEngine.Random.Range(leftBound, rightBound), UnityEngine.Random.Range(bottomBound, topBound), 0f);

            // Check if the distance between the player and the spawn point is greater than the minimum distance
            if (Vector3.Distance(playerPosition, randomPosition) > minDistanceToPlayer)
            {
                if (chainSpawner.IsNotBlockedByUI(randomPosition))
                {
                    validSpawnPointFound = true;
                }

            }
        }

        // Instantiate the enemy at the valid random position
        GameObject spawnedObject = Instantiate(objectToSpawn, randomPosition, Quaternion.identity);

        SpriteRenderer enemyRenderer = spawnedObject.GetComponent<SpriteRenderer>();

        // Set a random color to the enemy's material color
        Color randomColor = randomColors[UnityEngine.Random.Range(0, randomColors.Length)];
        randomColor.a = 1.0f;
        enemyRenderer.color = randomColor;
    }
}