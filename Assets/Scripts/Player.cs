﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private int _playerLives = 3;
    [SerializeField]
    private float _speed = 3.5f;
    [SerializeField]
    private GameObject _laserPrefab;
    [SerializeField]
    private GameObject _tripleShotPrefab;
    [SerializeField]
    private GameObject _shield;
    [SerializeField]
    private float _fireRate = 0.5f;
    [SerializeField]
    private bool _isTripleShotEnabled = false;
    [SerializeField]
    private bool _isShieldEnabled = false;
    [SerializeField]
    private bool _isSpeedEnabled = false;
    [SerializeField]
    private GameObject[] _hurtAnims;

    private float _coolDown = -1f;
    private float horizontalInput;
    private float verticalInput;
    private Vector3 direction;
    private int _score = 0;
    private SpawnManager _spawnManager;
    private UIManager _uiManager;
    private AudioManager _audioManager;
    private int randAnimation;

    void Start()
    {
        StartPosition();

        _spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
        if (_spawnManager == null)
        {
            Debug.LogError("Spawn Manager is null");
        }

        _uiManager = GameObject.Find("UI_Manager").GetComponent<UIManager>();
        if (_uiManager == null)
        {
            Debug.LogError("UI Manager is null");
        }

        _audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
        if (_audioManager == null)
        {
            Debug.LogError("Audio Manager is null");
        }

        randAnimation = Random.Range(0, _hurtAnims.Length);
    }

    void Update()
    {
        CalculateMovement();

        if (Input.GetKeyDown(KeyCode.Space) && Time.time > _coolDown)
        {
            FireLaser();
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

    private void FireLaser()
    {
        _coolDown = Time.time + _fireRate;

        if (_isTripleShotEnabled == false)
        {
            Instantiate(_laserPrefab, new Vector3(transform.position.x, transform.position.y + .85f), Quaternion.identity);
        }
        else
        {
            Instantiate(_tripleShotPrefab, transform.position, Quaternion.identity);
        }

        _audioManager.PlayLaserShot();
    }
    public void TripleShotActivated()
    {
        _isTripleShotEnabled = true;
        StartCoroutine(TripleShotPowerDownTimer());
    }

    IEnumerator TripleShotPowerDownTimer()
    {
        while (_isTripleShotEnabled == true)
        {
            yield return new WaitForSeconds(5);
            _isTripleShotEnabled = false;
        }
    }

    public void SpeedActivated()
    {
        _isSpeedEnabled = true;
        _speed *= 2;
        StartCoroutine(SpeedPowerDownTimer());
    }

    IEnumerator SpeedPowerDownTimer()
    {
        while (_isSpeedEnabled == true)
        {
            yield return new WaitForSeconds(5);
            _isSpeedEnabled = false;
            _speed /= 2;
        }
    }

    public void ShieldActivated()
    {
        _isShieldEnabled = true;
        _shield.SetActive(true);
    }

    public void Damage(int damageValue)
    {
        SpawnManager spawn_manager;
        GameObject spawn_manager_check = GameObject.FindGameObjectWithTag("SpawnManager");

        if (_isShieldEnabled == true)
        {
            _isShieldEnabled = false;
            _shield.SetActive(false);
            return;
        }

        _playerLives -= damageValue;

        ActivateHurtAnimation();

        _uiManager.UpdateLives(_playerLives);

        if (_playerLives <= 0)
        {
            if (spawn_manager_check != null)
            {
                spawn_manager = spawn_manager_check.GetComponent<SpawnManager>();
                spawn_manager.OnPlayerDeath();
            }
            _audioManager.PlayExplosion();
            Destroy(gameObject);
        }
    }

    private void ActivateHurtAnimation()
    {
        if (_playerLives < 3)
        {
            _hurtAnims[randAnimation].SetActive(true);
        }

        if (_playerLives < 2 && randAnimation == 0)
        {
            _hurtAnims[1].SetActive(true);
        }
        else if (_playerLives < 2 && randAnimation == 1)
        {
            _hurtAnims[0].SetActive(true);
        }
    }

    public void ScoreCalculator(int scoreValue)
    {
        _score += scoreValue;
        _uiManager.UpdateScore(_score);
    }
}
