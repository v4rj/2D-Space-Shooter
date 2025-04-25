using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    [SerializeField]
    private float _bombSpeed = 12f;
    [SerializeField]
    private GameObject _AOEPrefab;

    // Update is called once per frame
    void Update()
    {
        BombMovement();
        DestroyBomb();
    }

    private void BombMovement()
    {
        transform.Translate(Vector3.up * _bombSpeed * Time.deltaTime);
    }

    private void DestroyBomb()
    {
        if (transform.position.y > 7.7f)
        {
            Destroy(gameObject, 2.633f);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            Instantiate(_AOEPrefab, transform.position, Quaternion.identity);
            Destroy(other.gameObject);
            Destroy(gameObject);
        }
    }


}
