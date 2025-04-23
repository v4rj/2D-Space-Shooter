using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour
{
    [SerializeField]
    private float _powerupSpeed = 4f;

    //0 = Triple Shot
    //1 = Speed 
    //2 = Shield
    [SerializeField]
    private int _powerupID;

    private Player _player;
    private AudioManager _audioManager;

    void Start()
    {
<<<<<<< HEAD
        _player = GameObject.FindGameObjectWithTag("Player")?.GetComponent<Player>();
=======
        _playerCheck = GameObject.FindGameObjectWithTag("Player");
        _player = _playerCheck.GetComponent<Player>();
>>>>>>> e74a98862b071f94358bc505f0ce8949ac799272
        _audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
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
            if (_player != null)
            {
                switch(_powerupID)
                {
                    case 0:
                        _player.TripleShotActivated();
                        break;
                    case 1:
                        _player.SpeedActivated();
                        break;
                    case 2:
                        _player.ShieldActivated();
                        break;
                }
                _audioManager.PlayPowerup();
            }
            Destroy(gameObject);
        }
    }
}

