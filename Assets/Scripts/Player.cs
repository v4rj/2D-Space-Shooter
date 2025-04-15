using System.Collections;
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

    private float _coolDown = -1f;
    private float horizontalInput;
    private float verticalInput;
    private Vector3 direction;
    private int _score = 0;
    private SpawnManager _spawnManager;
    private UIManager _uiManager;

    void Start()
    {
        StartPosition();

        _spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
        _uiManager = GameObject.Find("UI_Manager").GetComponent<UIManager>();

        if (_spawnManager == null)
        {
            Debug.LogError("The spawn manager is null.");
        }

        if (_uiManager == null)
        {
            Debug.LogError("The UI manager is null.");
        }
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

    private void SpawnLaser()
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

        _uiManager.UpdateLives();

        if (_playerLives <= 0)
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
        _uiManager.UpdateScore(_score);
    }
}
