using System.Diagnostics;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject objectToSpawn; // The object to spawn
    public float initialSpawnRate = 3f; // The initial rate at which to spawn the objects
    private float timer; // A timer to control the spawn rate
    public float minDistanceToPlayer;

    [SerializeField]
    private Color[] randomColors;

    void Update()
    {
        // Increment the timer
        timer += Time.deltaTime;

        // Calculate a dynamic spawn rate based on game time
        float dynamicSpawnRate = Mathf.Lerp(initialSpawnRate, 1f, Time.time / 60f); // Adjust 600f for desired time scaling

        // Check if it's time to spawn an object
        if (timer >= dynamicSpawnRate)
        {
            timer = 0f;
            SpawnEnemyWithMinDistance();
        }
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
                validSpawnPointFound = true;
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
