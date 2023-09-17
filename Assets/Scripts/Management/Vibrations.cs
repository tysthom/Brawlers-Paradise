using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XInputDotNetPure;

public class Vibrations : MonoBehaviour
{
    PlayerIndex playerIndex;
    public bool vibrateOn;
    public bool vibrating;
    public float vIntensity;
    public Coroutine vibrateCoroutine;

    // Start is called before the first frame update
    void Start()
    {
        if (UniversalFight.usingMenuData)
        {
            if (StateNameController.controllerVibration == 0)
            {
                vibrateOn = true;
            }
            else
            {
                vibrateOn = false;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
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
            vibrating = false;
        }
    }
}
