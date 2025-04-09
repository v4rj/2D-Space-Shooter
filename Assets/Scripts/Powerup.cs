using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour
{
    [SerializeField]
    private float _powerupSpeed = 4f;
    private GameObject _playerCheck;
    private Player _player;

    void Start()
    {
        _playerCheck = GameObject.FindGameObjectWithTag("Player");
        _player = _playerCheck.GetComponent<Player>();
    }

    void Update()
    {
        PowerupMovement();

        if (transform.position.y <= -5.5f)
        {
            Destroy(gameObject);
        }
    }

    private void PowerupMovement()
    {
        transform.Translate(Vector3.down * _powerupSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            if (_playerCheck != null)
            {
                Debug.Log("Hit!");
                _player.TripleShotSwitch();
            }
            Destroy(gameObject);
        }
    }
}

