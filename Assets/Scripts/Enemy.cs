using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private float _enemySpeed = 4f;
    [SerializeField]
    private int _enemyPower = 1;
    [SerializeField]
    private float _fireRate = 3f;
    [SerializeField]
    private GameObject _laserPrefab;
    [SerializeField]
    private GameObject _enemyLaserContainer;

    private Animator _animator;
    private int _randomScore;
    private Player _player;
    private SpawnManager _spawn;
    private AudioManager _audioManager;
    private bool _isDead;

    private void Start()
    {
        _player = GameObject.Find("Player").GetComponent<Player>();
        if (_player == null)
        {
            Debug.LogError("Player is null");
        }
        
        _spawn = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
        if (_spawn == null)
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

        _isDead = false;
    }

    void Update()
    {
        EnemyMovement();

        if (Time.time > _fireRate && _isDead == false)
        {
            EnemyFire();
        }
        
        if (transform.position.y <= -5.5f)
        {
            Destroy(gameObject);
        }
    }

    private void EnemyMovement()
    {
        transform.Translate(Vector3.down * _enemySpeed * Time.deltaTime);
    }

    private void EnemyFire()
    {
            _fireRate = Random.Range(3, 5);
            _fireRate += Time.time;
            GameObject _laserContainer = Instantiate(_laserPrefab, transform.position, Quaternion.identity);
            Laser[] _lasers = _laserContainer.GetComponentsInChildren<Laser>();

            for (int i = 0; i < _lasers.Length; i++)
            {
                _lasers[i].tag = "Enemy Laser";
            }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Laser")
        {
            Destroy(other.gameObject);

            if (_player != null)
            {
                _randomScore = Random.Range(10, 20);
                _player.ScoreCalculator(_randomScore);
            }

            if (_spawn != null)
            {
                _spawn.IncreaseSpawnTimer();
            }

            IsEnemyDead();
            _animator.SetTrigger("OnEnemyDeath");
            _enemySpeed = 0;
            _audioManager.PlayExplosion();

            Destroy(GetComponent<Collider2D>());

            Destroy(gameObject, 2.633f);
        }
        else if (other.tag == "Player")
        {
            if (_player != null)
            {
                _player.Damage(_enemyPower);
            }
            IsEnemyDead();
            _animator.SetTrigger("OnEnemyDeath");

            _audioManager.PlayExplosion();

            Destroy(gameObject, 2.633f);
        }
    }

    

    private void IsEnemyDead()
    {
        _isDead = true;
    }
}
