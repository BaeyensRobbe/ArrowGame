using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Security.Cryptography;
using System.Threading;
using UnityEngine;

public class CollisionHandler : MonoBehaviour
{
    private GameManager gameManager;
    public EnemyTargetPlayer enemyTargetPlayer;
    public GameObject laserAnimationPrefab;
    public GameObject shieldPrefab;
    private float powerUpTime = 5f;
    private GameObject player;
    public AudioSource warningAudio;
    
    PlayerController playerController;
    ProgressionBar progressionBar;
    CoinsController coinsController;
    DestroyObject destroyObject;
    TimerScript timerscript;
    private bool isChainsaw = false;
    private bool powerUpActive = false;
    public Vector3 spawnPosition = new Vector3(-6f, 0f, 0f);
    private Vector3 playerSpawnPosition = new Vector3(0f,0f, 0f);
    private int highScore;
    public GameObject warningPrefab;



    private void Start()
    {
        UnityEngine.Debug.Log("script started");
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        UnityEngine.Debug.Log("PlayerMovement: GameManager found: " + (gameManager != null));
        coinsController = GameObject.FindGameObjectWithTag("EnemySpawner").GetComponent<CoinsController>();
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        timerscript = GameObject.Find("GameManager").GetComponent<TimerScript>();
        

    }

    private IEnumerator EnableChainsawPowerUp()
    {
        powerUpActive = true;
        isChainsaw = true;
        UnityEngine.Debug.Log("Chainsaw power up enabled");
        yield return new WaitForSeconds(powerUpTime);
        isChainsaw = false;
        powerUpActive=false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        UnityEngine.Debug.Log("Collision detected with: " + collision.gameObject.name);
        if (collision.gameObject.CompareTag("Enemy"))
        {
            if (isChainsaw)
            {
                EnemyTargetPlayer dyingEnemy = collision.gameObject.GetComponent<EnemyTargetPlayer>();
                dyingEnemy.EnemyDies();
            }
            else
            {
                UnityEngine.Debug.Log("Game Over triggered!");
                gameManager.GameOver();
                EnemyTargetPlayer[] enemiesgameover = GameObject.FindObjectsOfType<EnemyTargetPlayer>();
                DestroyObject[] objectsToDestroy = GameObject.FindObjectsOfType<DestroyObject>();
                foreach (EnemyTargetPlayer enemyScript in enemiesgameover)
                {
                    if (enemyScript != null)
                    {
                        enemyScript.DestroyEnemy();
                    }
                }
                foreach (DestroyObject objectToDestroy in objectsToDestroy)
                {
                    if (objectToDestroy != null)
                    {
                        objectToDestroy.DestroyThisObject();
                    }
                }

                playerController.ResetPlayerPosition(playerSpawnPosition);
                timerscript.ResetTimer();
            }

        }

        else if (collision.gameObject.CompareTag("Chainsaw"))
            
        {
            if (!powerUpActive)
            {
                StartCoroutine(EnableChainsawPowerUp());

                UnityEngine.Debug.Log("collision detected with chainsaw");

                EnemyTargetPlayer[] enemyplayers = GameObject.FindObjectsOfType<EnemyTargetPlayer>();
                playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
                progressionBar = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<ProgressionBar>();
                progressionBar.ProgressionBarAnimation(powerUpTime);

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
        else if (collision.gameObject.CompareTag("LaserIcon"))
        {
            Instantiate(laserAnimationPrefab, spawnPosition , Quaternion.identity);
            Destroy(collision.gameObject);
        }
        else if (collision.gameObject.CompareTag("Coin"))
        {
            coinsController.IncrementCoinCount(100);
            Destroy(collision.gameObject);
        }
        else if (collision.gameObject.CompareTag("ShieldIcon"))
        {
            Instantiate(shieldPrefab, transform.position, Quaternion.identity);
            Destroy(collision.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D trigger2D)
    {
        if (!isChainsaw)
        {
            if (trigger2D.CompareTag("Enemy"))
            {
                player = GameObject.Find("Player");
                if (warningAudio != null)
                {
                    warningAudio.PlayOneShot(warningAudio.clip);
                }
                Instantiate(warningPrefab, player.transform.position, trigger2D.transform.rotation);
            }
        }
        
    }

}
