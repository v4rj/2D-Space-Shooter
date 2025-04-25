using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private Text _scoreText;
    [SerializeField]
    private TextMeshProUGUI _ammoText;
    [SerializeField]
    private Text _gameOver;
    [SerializeField]
    private Text _restartText;
    [SerializeField]
    private Image _thruster;
    [SerializeField]
    private float _thrusterBarCooldownRate = 0.5f;
    [SerializeField]
    private Image _livesContainer;
    [SerializeField]
    private Sprite[] _lives;

    private GameManager _gameManager;
    private Player _player;
    private AudioManager _audioManager;
    private float _thrusterAlphaCooldown = 0f;
    private bool _rechargeSoundCooldown;

    void Start()
    {
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        if (_gameManager == null)
        {
            Debug.LogError("Game Manager Script is Null.");
        }

        _player = GameObject.Find("Player").GetComponent<Player>();
        if (_player == null)
        {
            Debug.LogError("Player Script is Null.");
        }

        _audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
        if (_audioManager == null)
        {
            Debug.LogError("Audio Manager Script is null");
        }

        _gameOver.gameObject.SetActive(false);
        _scoreText.text = "Score: " + 0;
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.LeftShift) && _player.GetThrusterCooldown() == false)
        {
            ThrusterActivated();
        }
        else
        {
            ThrusterRecharge();
        }
    }

    private void ThrusterActivated()
    {
        _thruster.fillAmount -= .5f * Time.deltaTime;

        if (_thruster.fillAmount == 0)
        {
            _player.ThrusterCooldownSwitch();
            _thruster.color = new Color(92, 88, 87);
        }
    }

    private void ThrusterRecharge()
    {
        _thruster.fillAmount += .35f * Time.deltaTime;

        if (_thruster.fillAmount == 1)
        {
            _player.ThrusterCooldownSwitch();
            _thruster.color = new Color(255, 0, 0);
            _rechargeSoundCooldown = false;
        }
        else if (_player.GetThrusterCooldown() == true)
        {
            if (_rechargeSoundCooldown == false)
            {
                _audioManager.RechargeSound();
                _rechargeSoundCooldown = true;
            }
            StartCoroutine(ThrusterUIFlicker());
        }
    }

    IEnumerator ThrusterUIFlicker()
    {
        yield return new WaitForSeconds(.45f);

        if (_thruster.color.a == 1 && Time.time > _thrusterAlphaCooldown)
        {
            _thrusterAlphaCooldown = Time.time + _thrusterBarCooldownRate;

            _thruster.color = new Color(_thruster.color.r, _thruster.color.g, _thruster.color.b, 0f);
        }
        else if (Time.time > _thrusterAlphaCooldown)
        {
            _thrusterAlphaCooldown = Time.time + _thrusterBarCooldownRate;

            _thruster.color = new Color(_thruster.color.r, _thruster.color.g, _thruster.color.b, 1f);
        }
    }

    public void UpdateScore(int playerScore)
    {
        _scoreText.text = "Score: " + playerScore;
    }

    public void UpdateLives(int playerLife)
    {
        if (playerLife < 0)
        {
            return;
        }

        _livesContainer.sprite = _lives[playerLife];

        if (playerLife == 0)
        {
            GameOverSequence();
        }
    }

    public void UpdateAmmoCount(int ammoCount)
    {
        _ammoText.text = "" + ammoCount;
    }

    private void GameOverSequence()
    {
        _gameManager.GameOverSwitch();
        _restartText.gameObject.SetActive(true);
        StartCoroutine(GameOverFlicker());
    }

    IEnumerator GameOverFlicker()
    {
        while (true)
        {
            _gameOver.gameObject.SetActive(true);
            yield return new WaitForSeconds(.4f);
            _gameOver.gameObject.SetActive(false);
            yield return new WaitForSeconds(.5f);
        }
    }
}
