using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldCollisionHandler : MonoBehaviour
{
    EnemyTargetPlayer enemyTargetPlayer;
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the colliding object has the "Enemy" tag.
        if (other.CompareTag("Enemy"))
        {
            enemyTargetPlayer = other.gameObject.GetComponent<EnemyTargetPlayer>();
            enemyTargetPlayer.EnemyDies();
            /*Destroy(other.gameObject);*/
        }
    }
}
