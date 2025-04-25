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
    private float _thrusterMultiplier = 1.5f;
    [SerializeField]
    private float _thrusterSpeed;
    [SerializeField]
    private int _ammoCount = 15;
    [SerializeField]
    private int _ammoRefill = 15;
    [SerializeField]
    private GameObject _laserPrefab;
    [SerializeField]
    private GameObject _tripleShotPrefab;
    [SerializeField]
    private GameObject _shield;
    [SerializeField]
    private float _fireRate = 0.5f;
    [SerializeField]
    private bool _isTripleShotEnabled;
    [SerializeField]
    private bool _isShieldEnabled;
    [SerializeField]
    private int _shieldStrength;
    [SerializeField]
    private bool _isSpeedEnabled;
    [SerializeField]
    private GameObject[] _hurtAnims;

    private float _coolDown = -1f;
    private bool _thrusterCooldown;
    private float _horizontalInput;
    private float _verticalInput;
    private Vector3 _direction;
    private int _score = 0;
    private SpriteRenderer _shieldSpriteColor;
    private SpawnManager _spawnManager;
    private UIManager _uiManager;
    private AudioManager _audioManager;
    private int _randAnimation;


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

        _shieldSpriteColor = _shield.GetComponent<SpriteRenderer>();
        if (_shieldSpriteColor == null)
        {
            Debug.LogError("Shield Sprite Renderer is null");
        }

        _randAnimation = Random.Range(0, _hurtAnims.Length);

        _thrusterSpeed = _speed * _thrusterMultiplier;

        _thrusterCooldown = false;
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
        _horizontalInput = Input.GetAxis("Horizontal");
        _verticalInput = Input.GetAxis("Vertical");
        _direction = new Vector3(_horizontalInput, _verticalInput);

        if (Input.GetKey(KeyCode.LeftShift) && _thrusterCooldown == false)
        {
            transform.Translate(_direction * _thrusterSpeed * Time.deltaTime);
        }
        else
        {
            transform.Translate(_direction * _speed * Time.deltaTime);
        }

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

    public bool GetThrusterCooldown()
    {
        return _thrusterCooldown;
    }

    public void ThrusterCooldownSwitch()
    {
        if (_thrusterCooldown == true)
        {
            _thrusterCooldown = false;
        }
        else
        {
            _thrusterCooldown = true;
        }
    }

    private void FireLaser()
    {
        if (_ammoCount <= 0)
        {
            Debug.Log("Out of Ammo!");    
            return;
        }
        
        _coolDown = Time.time + _fireRate;

        if (_isTripleShotEnabled == true && _ammoCount >= 3)
        {
            Instantiate(_tripleShotPrefab, transform.position, Quaternion.identity);
            _ammoCount -= 3;
        }
        else
        {
            Instantiate(_laserPrefab, new Vector3(transform.position.x, transform.position.y + .85f), Quaternion.identity);
            _ammoCount -= 1;
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
        yield return new WaitForSeconds(5);
        _isTripleShotEnabled = false;
    }

    public void SpeedActivated()
    {
        _isSpeedEnabled = true;
        _speed *= 2;
        StartCoroutine(SpeedPowerDownTimer());
    }

    IEnumerator SpeedPowerDownTimer()
    {
        yield return new WaitForSeconds(5);
        _isSpeedEnabled = false;
        _speed /= 2;
    }

    public void ShieldActivated()
    {
        _isShieldEnabled = true;
        _shieldStrength = 3;
        _shield.SetActive(true);
    }

    public void AmmoRefill()
    {
        _ammoCount += _ammoRefill;
    }

    public void Damage(int damageValue)
    {
        SpawnManager spawn_manager;
        GameObject spawn_manager_check = GameObject.FindGameObjectWithTag("SpawnManager");

        if (_isShieldEnabled == true)
        {
            _shieldStrength -= 1;

            if (_shieldStrength <= 0)
            {
                _isShieldEnabled = false;
                _shield.SetActive(false);
                return;
            }
            else if (_shieldStrength == 2)
            {
                _shieldSpriteColor.color = new Color(0.1492524f, 0.8113208f, 0.3983684f);
            }
            else
            {
                _shieldSpriteColor.color = Color.red;
            }
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
            _hurtAnims[_randAnimation].SetActive(true);
        }

        if (_playerLives < 2 && _randAnimation == 0)
        {
            _hurtAnims[1].SetActive(true);
        }
        else if (_playerLives < 2 && _randAnimation == 1)
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
