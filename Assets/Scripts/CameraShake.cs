using System.Collections;
using System.Collections.Generic;
using FirstGearGames.SmoothCameraShaker;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public ShakeData _myShake;
    // Start is called before the first frame update
    void Start()
    {
        CameraShakerHandler.Shake(_myShake);
    }
}
