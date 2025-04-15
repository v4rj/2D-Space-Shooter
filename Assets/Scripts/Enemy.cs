using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private float _enemySpeed = 4f;
    [SerializeField]
    private int _enemyPower = 20;

    private int randomScore;
    private Player player;
    private SpawnManager spawn;

    private void Start()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
        spawn = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
    }

    void Update()
    {
        EnemyMovement();

        if (transform.position.y <= -5.5f)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Laser")
        {
            if (player != null)
            {
                randomScore = Random.Range(10, 20);
                player.ScoreCalculator(randomScore);
            }

            if (spawn != null)
            {
                spawn.IncreaseSpawnTimer();
            }
            Destroy(other.gameObject);
            Destroy(gameObject);
        }
        else if (other.tag == "Player")
        {
            if (player != null)
            {
                player.Damage(_enemyPower);
            }
            Destroy(gameObject);            
        }
    }

    void EnemyMovement()
    {
        transform.Translate(Vector3.down * _enemySpeed * Time.deltaTime);
    }
}
