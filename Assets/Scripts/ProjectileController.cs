using UnityEngine;
using System.Collections;

public class ProjectileController : MonoBehaviour
{
    public float moveSpeed = 5f; // Adjust the speed as needed
    private Rigidbody2D rb;
    private Transform transform;
    public float destroyDelay = 10f;
    EnemyTargetPlayer enemyTargetPlayer;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        transform = GetComponent<Transform>();
        StartCoroutine(DestroyAfterDelay());
    }

    private void Update()
    {
        // Calculate the movement vector based on the object's forward direction
        transform.position += transform.up * moveSpeed * Time.deltaTime;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            enemyTargetPlayer = other.gameObject.GetComponent<EnemyTargetPlayer>();
            enemyTargetPlayer.EnemyDies();
            
        }
    }

    IEnumerator DestroyAfterDelay()
    {
        // Wait for the specified delay
        yield return new WaitForSeconds(destroyDelay);

        // Destroy the GameObject
        Destroy(gameObject);
    }


}
