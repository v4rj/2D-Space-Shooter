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
<<<<<<< HEAD
        QuitGame();
        GameOver();
    }

    private void QuitGame()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

=======
        GameOver();
    }
>>>>>>> e74a98862b071f94358bc505f0ce8949ac799272
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
