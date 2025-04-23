using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    private AudioManager _audioManager;
    void Start()
    {
        _audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();

        _audioManager.PlayExplosion();
        Destroy(gameObject, 2.35f);
    }

}
