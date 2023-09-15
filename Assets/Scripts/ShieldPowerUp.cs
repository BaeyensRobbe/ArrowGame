using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldPowerUp : MonoBehaviour
{
    public float destroyRadius = 5f;
    public GameObject destructionParticlePrefab;
    private CollisionHandler collisionHandler;

    private GameObject player;
    private Transform playerTransform;

    public GameObject shieldCircle;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerTransform = player.GetComponent<Transform>();
        collisionHandler = GameObject.Find("Player").GetComponent<CollisionHandler>();
        /*shieldCircle = GameObject.FindGameObjectWithTag("ShieldCircle");
        if (shieldCircle != null)
        {
            UnityEngine.Debug.Log("ShieldCirce found");
        }*/
    }

    void Update()
    {
        if (playerTransform != null)
        {
            transform.position = playerTransform.position;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            /*ShieldDestroyed();*/
            Destroy(gameObject);
            Instantiate(shieldCircle, transform.position, Quaternion.identity);
            
            collisionHandler.isNotShielded();
            
        }
    }

    void ShieldDestroyed()
    {
        // Find all enemy objects within the specified radius.
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, destroyRadius);
        foreach (Collider2D collider in colliders)
        {
            // Check if the collider has an EnemyTargetPlayer component.
            EnemyTargetPlayer enemyTarget = collider.GetComponent<EnemyTargetPlayer>();

            if (enemyTarget != null)
            {
                // Call the EnemyDies function on the enemy.
                enemyTarget.EnemyDies();
            }
        }

        // Destroy this object (the shield).
        Destroy(gameObject);
        Instantiate(shieldCircle, transform.position, Quaternion.identity);
        if (destructionParticlePrefab != null)
        {
            destructionParticlePrefab.SetActive(true);
            destructionParticlePrefab.transform.position = transform.position;
            ParticleSystem particleSystem = destructionParticlePrefab.GetComponent<ParticleSystem>();

            if (particleSystem != null)
            {
                particleSystem.Play();
            }
        }
    }
}
