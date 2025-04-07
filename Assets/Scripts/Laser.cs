using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    [SerializeField]
    private float _laserSpeed = 7f;
    [SerializeField]
    private GameObject _tripleShotContainer;

    void Update()
    {
        LaserMovement();
        DestroyLaser();
    }

    void LaserMovement()
    {
        transform.Translate(Vector3.up * _laserSpeed * Time.deltaTime);
    }

    void DestroyLaser()
    {
        if (transform.position.y >= 7.7f)
        {
            if (transform.parent != null)
            {
                Destroy(_tripleShotContainer);
            }
            Destroy(gameObject);
        }
    }
}
