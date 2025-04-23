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
    private bool _isTripleShotEnabled;
    [SerializeField]
    private bool _isShieldEnabled;
    [SerializeField]
<<<<<<< HEAD
    private bool _isSpeedEnabled;
=======
    private bool _isSpeedEnabled = false;
>>>>>>> e74a98862b071f94358bc505f0ce8949ac799272
    [SerializeField]
    private GameObject[] _hurtAnims;

    private float _coolDown = -1f;
    private float _horizontalInput;
    private float _verticalInput;
    private Vector3 _direction;
    private int _score = 0;
    private SpawnManager _spawnManager;
    private UIManager _uiManager;
    private AudioManager _audioManager;
<<<<<<< HEAD
    private int _randAnimation;
=======
    private int randAnimation;
>>>>>>> e74a98862b071f94358bc505f0ce8949ac799272

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

<<<<<<< HEAD
        _randAnimation = Random.Range(0, _hurtAnims.Length);
=======
        randAnimation = Random.Range(0, _hurtAnims.Length);
>>>>>>> e74a98862b071f94358bc505f0ce8949ac799272
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

        transform.Translate(_direction * _speed * Time.deltaTime);

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
<<<<<<< HEAD
        ActivateHurtAnimation();
=======

        ActivateHurtAnimation();

>>>>>>> e74a98862b071f94358bc505f0ce8949ac799272
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
<<<<<<< HEAD
            _hurtAnims[_randAnimation].SetActive(true);
        }

        if (_playerLives < 2 && _randAnimation == 0)
        {
            _hurtAnims[1].SetActive(true);
        }
        else if (_playerLives < 2 && _randAnimation == 1)
=======
            _hurtAnims[randAnimation].SetActive(true);
        }

        if (_playerLives < 2 && randAnimation == 0)
        {
            _hurtAnims[1].SetActive(true);
        }
        else if (_playerLives < 2 && randAnimation == 1)
>>>>>>> e74a98862b071f94358bc505f0ce8949ac799272
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
