using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class Main_Menu : MonoBehaviour
{
    public void LoadGame()
    {
        SceneManager.LoadScene(1); //main game scene
    }
}
