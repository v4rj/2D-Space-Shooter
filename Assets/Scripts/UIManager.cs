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
    private Text _gameOver;
    [SerializeField]
    private Text _restartText;
    [SerializeField]
    private Image _thruster;
    [SerializeField]
    private Image _livesContainer;
    [SerializeField]
    private Sprite[] _lives;

    private GameManager _gameManager;
    private bool _thrusterCooldown;

    void Start()
    {
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        _gameOver.gameObject.SetActive(false);
        _scoreText.text = "Score: " + 0;

        if (_gameManager == null)
        {
            Debug.LogError("Game Manager is Null.");
        }

        _thrusterCooldown = false;
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.LeftShift) && _thrusterCooldown == false)
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
            _thrusterCooldown = true;
        }
    }

    private void ThrusterRecharge()
    {
        _thruster.fillAmount += .35f * Time.deltaTime;

        if (_thruster.fillAmount == 1)
        {
            _thrusterCooldown = false;
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
