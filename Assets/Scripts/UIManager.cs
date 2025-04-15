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

        _livesContainer.sprite = _lives[playerLife];

        if (playerLife == 0)
        {
            StartCoroutine(GameOverFlicker());
        }
    }
    IEnumerator GameOverFlicker()
    {
        while (true)
        {
            yield return new WaitForSeconds(.4f);
            _gameOver.gameObject.SetActive(true);
            yield return new WaitForSeconds(.5f);
            _gameOver.gameObject.SetActive(false);
        }
    }
}
