using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AOE : MonoBehaviour
{
    [SerializeField]
    private int _AOEDamage = 1;

    private Player _player;
    private CircleCollider2D _circleCollider;
    private Vector3 _scaleChange;

    void Start()
    {
        _player = GameObject.Find("Player").GetComponent<Player>();
        if (_player == null)
        {
            Debug.LogError("Player Script is null");
        }
        
        _circleCollider = GetComponent<CircleCollider2D>();
        if (_circleCollider == null)
        {
            Debug.LogError("AOE Explosion Circle Collider is null");
        }

        _scaleChange = new Vector3(.05f, .05f, 0);

    }

    void Update()
    {
        if (transform.localScale.x < 31)
        {
            AOEExplosion();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void AOEExplosion()
    {
        transform.localScale += _scaleChange;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            Debug.Log("Player Hit");
            _player.Damage(_AOEDamage);
        }
        else if (other.tag == "Enemy")
        {
            Destroy(other.gameObject);
        }
    }
}
