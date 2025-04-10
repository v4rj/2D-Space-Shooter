using System.Collections;
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
    private GameObject _tripleShotPrefab;
    [SerializeField]
    private float _fireRate = 0.5f;
    [SerializeField]
    private int _score = 0;
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
            StartCoroutine("CountDownTimer");
            yield return new WaitForSeconds(5);
            _isTripleShotEnabled = false;
            StopCoroutine("CountDownTimer");
        }
    }

    public void SpeedActivated()
    {
        _isSpeedEnabled = true;
        _speed *= 2;
        Debug.Log("Speed Enabled!");
        StartCoroutine(SpeedPowerDownTimer());
    }

    IEnumerator SpeedPowerDownTimer()
    {
        while (_isSpeedEnabled == true)
        {
            StartCoroutine("CountDownTimer");
            yield return new WaitForSeconds(5);
            _isSpeedEnabled = false;
            _speed /= 2;
            Debug.Log("Speed Disabled!");
            StopCoroutine("CountDownTimer");
        }
    }

    public void ShieldActivated()
    {
        _isShieldEnabled = true;
        Debug.Log("Shield Enabled!");
        StartCoroutine(ShieldPowerDownTimer());
    }

    IEnumerator ShieldPowerDownTimer()
    {
        while (_isShieldEnabled == true)
        {
            StartCoroutine("CountDownTimer");
            yield return new WaitForSeconds(5f);
            _isShieldEnabled = false;
            Debug.Log("Shield Disabled!");
            StopCoroutine("CountDownTimer");
        }
    }

    IEnumerator CountDownTimer()
    {
        int count = 1;

        while (true)
        {
            Debug.Log(count);
            yield return new WaitForSeconds(1);
            count++;
        }
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
