using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Security.Cryptography;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;


public class CollisionHandler : MonoBehaviour
{
    private GameManager gameManager;
    public EnemyTargetPlayer enemyTargetPlayer;
    public GameObject laserAnimationPrefab;
    public GameObject shieldPrefab;
    private float powerUpTime = 5f;
    private GameObject player;
    public AudioSource warningAudio;
    private bool isShielded;

    EnemySpawner enemySpawner;
    PlayerController playerController;
    ProgressionBar progressionBar;
    CoinsController coinsController;
    DestroyObject destroyObject;
    TimerScript timerscript;
    ShootingProjectile shootingProjectile;
    private bool isChainsaw = false;
    private bool powerUpActive = false;
    public Vector3 spawnPosition = new Vector3(-6f, 0f, 0f);
    private Vector3 playerSpawnPosition = new Vector3(0f,0f, 0f);
    private int highScore;
    public GameObject warningPrefab;
    public Image freezePanel;
    private float freezeOpacity;

    public GameObject explosionPrefab;
    public GameObject coinAnimationPrefab;



    private void Start()
    {
        
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        
        coinsController = GameObject.FindGameObjectWithTag("EnemySpawner").GetComponent<CoinsController>();
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        timerscript = GameObject.Find("GameManager").GetComponent<TimerScript>();
        enemySpawner = GameObject.Find("EnemySpawner").GetComponent<EnemySpawner>();
        shootingProjectile = gameObject.GetComponent<ShootingProjectile>();
        freezePanel.gameObject.SetActive(false);

    }

    private IEnumerator EnableChainsawPowerUp()
    {
        powerUpActive = true;
        isChainsaw = true;
        yield return new WaitForSeconds(powerUpTime);
        isChainsaw = false;
        powerUpActive=false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            if (isChainsaw || isShielded)
            {
                EnemyTargetPlayer dyingEnemy = collision.gameObject.GetComponent<EnemyTargetPlayer>();
                dyingEnemy.EnemyDies();
            }
            else
            {
                gameManager.EndGame();
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
                enemySpawner.ResetTimer();
            }

        }

        else if (collision.gameObject.CompareTag("Chainsaw"))
            
        {
            if (!powerUpActive)
            {
                StartCoroutine(EnableChainsawPowerUp());

                EnemyTargetPlayer[] enemyplayers = GameObject.FindObjectsOfType<EnemyTargetPlayer>();
                playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
                progressionBar = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<ProgressionBar>();
                progressionBar.ProgressionBarAnimation(powerUpTime);

                if (playerController != null)
                {
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
            Instantiate(coinAnimationPrefab, collision.gameObject.transform.position, Quaternion.identity);
            Destroy(collision.gameObject);
            
        }
        else if (collision.gameObject.CompareTag("ShieldIcon"))
        {
            Instantiate(shieldPrefab, transform.position, Quaternion.identity);
            Destroy(collision.gameObject);
            isShielded = true;
        }
        else if (collision.gameObject.CompareTag("FreezeIcon"))
        {
            /* insert panel visibilty*/
            EnemyTargetPlayer[] enemyplayers = GameObject.FindObjectsOfType<EnemyTargetPlayer>();
            freezePanel.gameObject.SetActive(true);
            StartCoroutine(ShowFreezePanel());
            foreach (EnemyTargetPlayer enemyScript in enemyplayers)
            {
                if (enemyScript != null)
                {
                    enemyScript.FreezeEnemy();
                    Destroy(collision.gameObject);
                }
            }
        }
        else if (collision.gameObject.CompareTag("BombIcon"))
        {
            Instantiate(explosionPrefab, collision.gameObject.transform.position, Quaternion.identity);
            Destroy(collision.gameObject);
        }
        else if (collision.gameObject.CompareTag("ShootIcon"))
        {
            shootingProjectile.AKPowerupActivate();
            Destroy(collision.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D trigger2D)
    {
        if (!isChainsaw && !isShielded)
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

    public void isNotShielded()
    {
        StartCoroutine(SetShieldedToFalse());
    }

    private IEnumerator SetShieldedToFalse()
    {
        yield return new WaitForSeconds(1f);
        isShielded = false;
    }

    private IEnumerator ShowFreezePanel()
    {
        yield return new WaitForSeconds(3f);
        freezePanel.gameObject.SetActive(false);
    }

}
