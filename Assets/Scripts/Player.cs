﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private int _playerHealth = 100;
    [SerializeField]
    private float _speed = 3.5f;
    [SerializeField]
    private GameObject _laserPrefab;
    [SerializeField]
    private float _fireRate = 0.5f;
    [SerializeField]
    private int _score = 0;
    private float _coolDown = -1f;
    private float horizontalInput;
    private float verticalInput;
    Vector3 direction;

    void Start()
    {
        StartPosition();
    }

    void Update()
    {
        CalculateMovement();

        if (Input.GetKeyDown(KeyCode.Space) && Time.time > _coolDown)
        {
            SpawnLaser();
        }
    }

    void StartPosition()
    {
        transform.position = new Vector3(0, -3, 0);
    }

    void CalculateMovement()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");
        direction = new Vector3(horizontalInput, verticalInput);

        transform.Translate(direction * _speed * Time.deltaTime);

        transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, -3.8f, 0), 0);

        if (transform.position.x >= 11.2893f)
        {
            transform.position = new Vector3(-11.2893f, transform.position.y, 0);
        }
        else if (transform.position.x <= -11.2893f)
        {
            transform.position = new Vector3(11.2893f, transform.position.y, 0);
        }
    }

    void SpawnLaser()
    {
            _coolDown = Time.time + _fireRate;
            Instantiate(_laserPrefab, new Vector3(transform.position.x, transform.position.y + 0), Quaternion.identity);
    }

    public void Damage(int damageValue)
    {
        SpawnManager spawn_manager;
        GameObject spawn_manager_check = GameObject.FindGameObjectWithTag("SpawnManager");

        _playerHealth -= damageValue;

        if (_playerHealth <= 0)
        {
            if (spawn_manager_check != null)
            {
                spawn_manager = spawn_manager_check.GetComponent<SpawnManager>();
                spawn_manager.OnPlayerDeath();
            }
            Destroy(gameObject);
        }
    }

    public void ScoreCalculator(int scoreValue)
    {
        _score += scoreValue;
    }
}
