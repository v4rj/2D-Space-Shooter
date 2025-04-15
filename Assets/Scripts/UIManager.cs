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
    private Image _firstLife;
    [SerializeField]
    private Image _secondLife;
    [SerializeField]
    private Image _thirdLife;

    void Start()
    {
        _scoreText.text = "Score: " + 0;
    }

    // Update is called once per frame
    public void UpdateScore(int playerScore)
    {
        _scoreText.text = "Score: " + playerScore;
    }

    public void UpdateLives()
    {
        if (_firstLife != null)
        {
            Destroy(_firstLife);
        }
        else if ( _firstLife == null & _secondLife != null)
        {
            Destroy(_secondLife);
        }
        else
        {
            Destroy(_thirdLife);
        }
    }
}
