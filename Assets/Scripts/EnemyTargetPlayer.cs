using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTargetPlayer : MonoBehaviour
{
    private Transform player;
    public float moveSpeed = 5f;
    private bool isReversingDirection = false;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
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
        MoveTowardsPlayer(direction);
    }

    void MoveTowardsPlayer(Vector3 direction)
    {
        Vector3 newPosition = transform.position + direction * moveSpeed * Time.deltaTime;
        transform.position = newPosition;
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
}
