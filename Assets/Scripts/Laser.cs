using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    [SerializeField]
    private float _laserSpeed = 12f;
    [SerializeField]
    private int _laserPower = 1;
    [SerializeField]
    private GameObject _tripleShotContainer;

    void Update()
    {
        LaserMovement();
        DestroyLaser();
    }

    void LaserMovement()
    {
        if (gameObject.tag == "Enemy Laser")
        {
            transform.Translate(Vector3.down * _laserSpeed * Time.deltaTime);
        }
        else
        {
            transform.Translate(Vector3.up * _laserSpeed * Time.deltaTime);
        }  
    }

    void DestroyLaser()
    {
        if (transform.position.y >= 7.7f ^ transform.position.y <= -5.5f)
        {
            if (transform.parent != null)
            {
                Destroy(transform.parent.gameObject);
            }
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player" && gameObject.tag == "Enemy Laser")
        {
            Player _player = GameObject.Find("Player").GetComponent<Player>();
            if (_player == null)
            {
                Debug.LogError("Player is null");
            }

            _player.Damage(_laserPower);
            Destroy(gameObject);
        }
    }
}
