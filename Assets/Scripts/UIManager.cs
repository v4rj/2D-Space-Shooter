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
    private Image _livesContainer;
    [SerializeField]
    private Sprite[] _lives;

    private GameManager _gameManager;
    
    void Start()
    {
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        _gameOver.gameObject.SetActive(false);
        _scoreText.text = "Score: " + 0;

        if (_gameManager == null)
        {
            Debug.LogError("Game Manager is Null.");
        }
    }

    public void UpdateScore(int playerScore)
    {
        _scoreText.text = "Score: " + playerScore;
    }

    public void UpdateLives(int playerLife)
    {

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
