using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private Text scoreText;
    [SerializeField]
    private int _score;
    private GameObject playerCheck;
    private Player player;

    void Start()
    {
        playerCheck = GameObject.FindGameObjectWithTag("Player");
        player = playerCheck.GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        if (playerCheck != null)
        {
            _score = player.GetScore();
            scoreText.text = $"Score: {_score}";
        }
    }
}
