using System;
using System.Diagnostics;
using UnityEngine;

public class ChainSpawner : MonoBehaviour
{
    public GameObject[] objectsToSpawn; // The object to spawn
    public float initialSpawnRate = 3f; // The initial rate at which to spawn the objects
    private float timer; // A timer to control the spawn rate
    public float minDistanceToPlayer;


    public RectTransform[] blockingUIElements;





    void Update()
    {
        // Increment the timer
        timer += Time.deltaTime;

        // Calculate a dynamic spawn rate based on game time
        // Adjust 600f for desired time scaling

        // Check if it's time to spawn an object
        if (timer >= initialSpawnRate)
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
                if (IsNotBlockedByUI(randomPosition))
                {
                    validSpawnPointFound = true;
                }
                

            }
        }

        // Instantiate the enemy at the valid random position
        int randomIndex = UnityEngine.Random.Range(0, objectsToSpawn.Length);
        GameObject objectToSpawn = objectsToSpawn[randomIndex];
        GameObject spawnedObject = Instantiate(objectToSpawn, randomPosition, Quaternion.identity);
        /*spawnedObject.transform.SetParent(transform, false);*/

    }

    public bool IsNotBlockedByUI(Vector3 randomPosition)
    {
        foreach (RectTransform rectTransform in blockingUIElements)
        {
            Vector2 localPoint;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(rectTransform, randomPosition, null, out localPoint);

            Rect expandedRect = rectTransform.rect;
            expandedRect.xMin -= 100f;
            expandedRect.xMax += 100f;

            if (expandedRect.Contains(localPoint))
            {
                return false; // The random position is inside this UI element's bounds
            }
        }
        return true; // The random position is not inside any blocking UI element bounds
    }






}