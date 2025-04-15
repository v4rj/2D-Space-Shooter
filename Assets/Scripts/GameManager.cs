using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private bool _isGameOver;

    void Start()
    {
        _isGameOver = false;
    }

    void Update()
    {
        GameOver();
    }
    private void GameOver()
    {
        if (Input.GetKeyDown(KeyCode.R) && _isGameOver == true)
        {
            SceneManager.LoadScene(1); //main game scene
        }
    }

    public void GameOverSwitch()
    {
        _isGameOver = true;
    }
}
