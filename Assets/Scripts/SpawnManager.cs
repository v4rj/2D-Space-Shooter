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
    private GameObject[] _commonPowerups;
    [SerializeField]
    private GameObject _rarePowerUp;
    [SerializeField]
    private GameObject[] _pickups;
    [SerializeField]
    private GameObject _powerupContainer;
    [SerializeField]
    private GameObject _pickupContainer;
    [SerializeField]
    private float _spawnTimer = 1.8f;
    [SerializeField]
    private float _spawnTimeIncrease = .2f;
    [SerializeField]
    private float _commonPowerupTimer = 2.5f;
    [SerializeField]
    private float _rarePowerupTimer = 5;
    [SerializeField]
    private float _pickupTimer = 3f;

    private bool _stopSpawning = false;
    private float _randomPos;

    void Update()
    {
        _randomPos = Random.Range(-9.32f, 9.32f);
    }

    public void StartSpawning()
    {
        StartCoroutine(EnemySpawnTimer());
        StartCoroutine(CommonPowerupSpawnTimer());
        StartCoroutine(RarePowerupSpawnTimer());
        StartCoroutine(PickupSpawnTimer());
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

    IEnumerator CommonPowerupSpawnTimer()
    {
        yield return new WaitForSeconds(3f);
        GameObject newPowerUp;

        while (_stopSpawning == false)
        {
            int randomPowerup = Random.Range(0, _commonPowerups.Length);
            Vector3 posToSpawn = new Vector3(_randomPos, 7.7f, 0);
            newPowerUp = Instantiate(_commonPowerups[randomPowerup], posToSpawn, Quaternion.identity);
            newPowerUp.transform.parent = _powerupContainer.transform;
            yield return new WaitForSeconds(_commonPowerupTimer);
        }
    }

    IEnumerator RarePowerupSpawnTimer()
    {
        yield return new WaitForSeconds(6f);
        GameObject newRarePowerUp;

        while (_stopSpawning == false)
        {
            Vector3 posToSpawn = new Vector3(_randomPos, 7.7f, 0);
            newRarePowerUp = Instantiate(_rarePowerUp, posToSpawn, Quaternion.identity);
            newRarePowerUp.transform.parent = _powerupContainer.transform;
            yield return new WaitForSeconds(_rarePowerupTimer);
        }
    }

    IEnumerator PickupSpawnTimer()
    {
        yield return new WaitForSeconds(5f);
        GameObject newPickup;

        while (_stopSpawning == false)
        {
            int randomPickup = Random.Range(0, _pickups.Length);
            Vector3 posToSpawn = new Vector3(_randomPos, 7.7f, 0);
            newPickup = Instantiate(_pickups[randomPickup], posToSpawn, Quaternion.identity);
            newPickup.transform.parent = _powerupContainer.transform;
            yield return new WaitForSeconds(_pickupTimer);
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
