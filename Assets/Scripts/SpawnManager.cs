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
    private GameObject[] _powerups;
    [SerializeField]
    private GameObject _powerupContainer;
    [SerializeField]
    private float _spawnTimer = 1.8f;
    [SerializeField]
    private float _spawnTimeIncrease = .2f;
    [SerializeField]
    private float _powerupTimer = 2.5f;

    private bool _stopSpawning = false;
    private float _randomPos;

    void Update()
    {
        _randomPos = Random.Range(-9.32f, 9.32f);
    }
    public void StartSpawning()
    {
        StartCoroutine(EnemySpawnTimer());
        StartCoroutine(PowerupSpawnTimer());
    }

    IEnumerator EnemySpawnTimer()
    {
        yield return new WaitForSeconds(2f);
        GameObject newEnemy;

        while (_stopSpawning == false)
            {
                Vector3 posToSpawn = new Vector3(_randomPos, 7.7f, 0);
                newEnemy = Instantiate(_enemyPrefab, posToSpawn, Quaternion.identity);
                newEnemy.transform.parent = _enemyContainer.transform;
                yield return new WaitForSeconds(_spawnTimer);
            }         
    }

    IEnumerator PowerupSpawnTimer()
    {
        yield return new WaitForSeconds(3f);
        GameObject newPowerUp;

        while (_stopSpawning == false)
        {
            int randomPowerup = Random.Range(0, _powerups.Length);
            Vector3 posToSpawn = new Vector3(_randomPos, 7.7f, 0);
            newPowerUp = Instantiate(_powerups[randomPowerup], posToSpawn, Quaternion.identity);
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
        else if (_spawnTimer <= 1f && _spawnTimer >= .35f)
        {
            _spawnTimer -= (_spawnTimeIncrease / 2);
        }
        
    }

    public void OnPlayerDeath()
    {
        _stopSpawning = true;
    }
}
