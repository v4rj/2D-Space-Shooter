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
    private Image _livesContainer;
    [SerializeField]
    private Sprite[] _lives;
    
    void Start()
    {
        _gameOver.gameObject.SetActive(false);
        _scoreText.text = "Score: " + 0;
    }

    // Update is called once per frame
    public void UpdateScore(int playerScore)
    {
        _scoreText.text = "Score: " + playerScore;
    }

    public void UpdateLives(int playerLife)
    {
        switch(playerLife)
        {
            case 0:
                _livesContainer.sprite = _lives[0];
                break;
            case 1:
                _livesContainer.sprite = _lives[1];
                break;
            case 2:
                _livesContainer.sprite = _lives[2];
                break;
            case 3:
                _livesContainer.sprite = _lives[3];
                break;
        }
    }

    public void GameOver()
    {
        _gameOver.gameObject.SetActive(true);
    }
}
