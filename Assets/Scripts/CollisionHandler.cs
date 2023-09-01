using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class CollisionHandler : MonoBehaviour
{
    private GameManager gameManager;
    public EnemyTargetPlayer enemyTargetPlayer;
    private float powerUpTime = 5f;
    EnemyTargetPlayer enemytargetplayer;
    PlayerController playerController;
    private bool isChainsaw = false;
    

    private void Start()
    {
        UnityEngine.Debug.Log("script started");
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        UnityEngine.Debug.Log("PlayerMovement: GameManager found: " + (gameManager != null));
        
    }

    private IEnumerator EnableChainsawPowerUp()
    {
        isChainsaw = true;
        UnityEngine.Debug.Log("Chainsaw power up enabled");
        yield return new WaitForSeconds(powerUpTime);
        isChainsaw = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        UnityEngine.Debug.Log("Collision detected with: " + collision.gameObject.name);
        if (collision.gameObject.CompareTag("Enemy"))
        {
            if (isChainsaw)
            {
                Destroy(collision.gameObject);
            } else
            {
                UnityEngine.Debug.Log("Game Over triggered!");
                gameManager.GameOver();
            }
            
        }
        else if (collision.gameObject.CompareTag("Chainsaw"))
            
        {
            StartCoroutine(EnableChainsawPowerUp());
            /*EnemyTargetPlayer enemyScript = collision.gameObject.GetComponent<EnemyTargetPlayer>();*/
            UnityEngine.Debug.Log("collision detected with chainsaw");
            /*enemytargetplayer = GameObject.FindGameObjectWithTag("Enemy").GetComponent<EnemyTargetPlayer>();*/
            EnemyTargetPlayer[] enemyplayers = GameObject.FindObjectsOfType<EnemyTargetPlayer>();
            playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();

            if (playerController != null)
            {
                UnityEngine.Debug.Log("PlayerController found");
                playerController.spriteToChainsaw(powerUpTime);
            }

            // Reverse the enemy's movement direction
            foreach (EnemyTargetPlayer enemyScript in enemyplayers)
            {
                if (enemyScript != null)
                {
                    enemyScript.ReverseDirection(powerUpTime);

                }

                Destroy(collision.gameObject);

            }





        }
    }

}
