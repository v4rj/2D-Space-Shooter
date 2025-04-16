using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    [SerializeField]
    private float _rotationSpeed = 3f;
    [SerializeField]
    private GameObject _anim;

    private Vector3 posToSpawn;

    void Start()
    {

        if (_anim == null)
        {
            Debug.LogError("Explosion Animation animator is null");
        }
    }

    void Update()
    {
        posToSpawn = transform.position;

        transform.Rotate(Vector3.forward * _rotationSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Laser")
        {
            Destroy(other.gameObject);

            Instantiate(_anim, posToSpawn, Quaternion.identity);

            Destroy(gameObject);
        }
    }
}
