using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private float _enemySpeed = 4f;
    [SerializeField]
    private int _enemyPower = 1;

    private Animator _animator;
    private int randomScore;
    private Player player;
    private SpawnManager spawn;
    private AudioManager _audioManager;

    private void Start()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
        if (player == null)
        {
            Debug.LogError("Player is null");
        }
        
        spawn = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
        if (spawn == null)
        {
            Debug.LogError("Spawn Manager is null");
        }

        _animator = GetComponent<Animator>();
        if (_animator == null)
        {
            Debug.LogError("Enemy Animator is null");
        }

        _audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
        if (_audioManager == null)
        {
            Debug.LogError("Audio Manager is null");
        }
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
            Destroy(other.gameObject);

            if (player != null)
            {
                randomScore = Random.Range(10, 20);
                player.ScoreCalculator(randomScore);
            }

            if (spawn != null)
            {
                spawn.IncreaseSpawnTimer();
            }

            _animator.SetTrigger("OnEnemyDeath");

            _audioManager.PlayExplosion();

            _enemySpeed = 0;

            Destroy(gameObject, 2.633f);
        }
        else if (other.tag == "Player")
        {
            if (player != null)
            {
                player.Damage(_enemyPower);
            }

            _animator.SetTrigger("OnEnemyDeath");

            _audioManager.PlayExplosion();

            Destroy(gameObject, 2.633f);
        }
    }

    void EnemyMovement()
    {
        transform.Translate(Vector3.down * _enemySpeed * Time.deltaTime);
    }
}
