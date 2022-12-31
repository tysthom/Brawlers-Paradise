using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XInputDotNetPure;

public class Vibrations : MonoBehaviour
{
    PlayerIndex playerIndex;
    GamePadState state;
    GamePadState prevState;
    public bool vibrateOn;
    public bool vibrating;
    public float vIntensity;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //vibrate = true;
        //vIntensity = .5f;
            GamePad.SetVibration(playerIndex, vIntensity, vIntensity);
    }

    public IEnumerator Vibrate(float duration, float intensity)
    {
        if (vibrateOn)
        {
            vIntensity = intensity;
            vibrating = true;
            yield return new WaitForSeconds(duration);
            vIntensity = 0;
            //GamePad.SetVibration(playerIndex, vIntensity, vIntensity);
            vibrating = false;
        }
    }
}
