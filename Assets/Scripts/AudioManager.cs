using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField]
    private AudioSource _laserShot;
    [SerializeField]
    private AudioSource _explosion;
    [SerializeField]
    private AudioSource _powerup;
    [SerializeField]
    private AudioSource _misFire;
    [SerializeField]
    private AudioSource _recharge;

    public void PlayLaserShot ()
    {
        _laserShot.Play();
    }

    public void PlayExplosion()
    {
        _explosion.Play();
    }

    public void PlayPowerup()
    {
        _powerup.Play();
    }

    public void MisfireSound()
    {
        _misFire.Play();
    }

    public void RechargeSound()
    {
        _recharge.Play();
    }
}
