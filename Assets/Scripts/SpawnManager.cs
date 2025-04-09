using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _enemyPrefab;
    [SerializeField]
    private GameObject _enemyContainer;
    [SerializeField]
    private GameObject _powerupPrefab;
    [SerializeField]
    private GameObject _powerupContainer;
    [SerializeField]
    private float _spawnTimer = 1.8f;
    [SerializeField]
    private float _spawnTimeIncrease = .2f;
    [SerializeField]

    private float _powerupTimer = 2.3f;
    private bool _stopSpawning = false;
    private float randomPos;

    void Start()
    {
        StartCoroutine(EnemySpawnTimer());
        StartCoroutine(PowerupSpawnTimer());
    }

    private void Update()
    {
        randomPos = Random.Range(-9.32f, 9.32f);
    }

    IEnumerator EnemySpawnTimer()
    {
        GameObject newEnemy;

        while (_stopSpawning == false)
            {
                newEnemy = Instantiate(_enemyPrefab, new Vector3(randomPos, 7.7f, 0), Quaternion.identity);
                newEnemy.transform.parent = _enemyContainer.transform;
                yield return new WaitForSeconds(_spawnTimer);
            }         
    }

    IEnumerator PowerupSpawnTimer()
    {
        GameObject newPowerUp;

        while (_stopSpawning == false)
        {
            newPowerUp = Instantiate(_powerupPrefab, new Vector3(randomPos, 7.7f, 0), Quaternion.identity);
            newPowerUp.transform.parent = _powerupContainer.transform;
            yield return new WaitForSeconds(_powerupTimer);
        }
    }

    public void IncreaseSpawnTimer()
    {
        if (_spawnTimer >= 1f)
        {
            _spawnTimer -= _spawnTimeIncrease;
        }
        else if (_spawnTimer <= 1f && _spawnTimer >= .2)
        {
            _spawnTimer -= (_spawnTimeIncrease / 2);
        }
        
    }

    public void OnPlayerDeath()
    {
        _stopSpawning = true;
    }
}
