using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XInputDotNetPure;

public class Vibrations : MonoBehaviour
{
    PlayerIndex playerIndex;
    public bool canVibrate;
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
                canVibrate = true;
            }
            else
            {
                canVibrate = false;
            }
        }

        if(GetComponent<IdManagear>().gameMode != IdManagear.mode.AiVsAi)
        {
            canVibrate = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (vibrating == false)
            vIntensity = 0;

       GamePad.SetVibration(playerIndex, vIntensity, vIntensity);
    }

    public IEnumerator Vibrate(float duration, float intensity)
    {
        if (canVibrate)
        {
            vIntensity = intensity;
            vibrating = true;
            yield return new WaitForSeconds(.1f);
            vIntensity = 0;
            vibrating = false;
            StopCoroutine(vibrateCoroutine);
        }
    }
}
