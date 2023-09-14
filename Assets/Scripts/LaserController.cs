using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserController : MonoBehaviour
{
    public float moveSpeed;
    private AudioSource audioSource;
    public EnemyTargetPlayer enemyTargetPlayer;


    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 newPosition = transform.position + Vector3.right * moveSpeed * Time.deltaTime;

        // Update the GameObject's position.
        transform.position = newPosition;
        if (transform.position.x > 6)
        {
            Destroy(gameObject);
        }

        audioSource.Play();
    }

  

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            EnemyTargetPlayer dyingEnemy = collision.gameObject.GetComponent<EnemyTargetPlayer>();
            dyingEnemy.EnemyDies();

        }
    }
}
