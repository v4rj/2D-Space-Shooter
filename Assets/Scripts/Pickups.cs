using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickups : MonoBehaviour
{
    [SerializeField]
    private float _pickupsSpeed = 4f;

    // 0 Create
    // 1 Heart
    [SerializeField]
    private int _pickupsID;
    private Player _player;
    private AudioManager _audioManager;

    void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player")?.GetComponent<Player>();
        _audioManager = GameObject.Find("AudioManager")?.GetComponent<AudioManager>();
    }

    void Update()
    {
        PickupMovement();

        if (transform.position.y <= -5.5f)
        {
            Destroy(gameObject);
        }
    }

    private void PickupMovement()
    {
        transform.Translate(Vector3.down * _pickupsSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            if (_player != null)
            {
                switch (_pickupsID)
                {
                    case 0:
                        _player.AmmoRefill();
                        break;
                    case 1:
                        _player.GainALife();
                        break;
                }
                _audioManager.PlayPowerup();
            }
            Destroy(gameObject);
        }
    }
}
