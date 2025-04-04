﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private float _enemySpeed = 4f;
    [SerializeField]
    private int _enemyPower = 20;
    private GameObject playerCheck;
    private GameObject spawnCheck;
    private Player player;
    private SpawnManager spawn;

    private void Start()
    {
        playerCheck = GameObject.FindGameObjectWithTag("Player");
        spawnCheck = GameObject.FindGameObjectWithTag("SpawnManager");

        player = playerCheck.GetComponent<Player>();
        spawn = spawnCheck.GetComponent<SpawnManager>();
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
            if (playerCheck != null)
            {
                player.ScoreCalculator(20);
            }

            if (spawnCheck != null)
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
