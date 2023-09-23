using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightingSoundEffects : MonoBehaviour
{
    GameObject soundManager;
    SoundManager soundManagerInstance;
    AudioSource soundEffectsAudioSource;

    void Start()
    {
        soundManager = GameObject.Find("Sound Manager");
        soundManagerInstance = soundManager.GetComponent<SoundManager>();
        soundEffectsAudioSource = GameObject.Find("Sound Effects").GetComponent<AudioSource>();
    }

    private void Update()
    {
        //if(UniversalFight.usingMenuData)
            //soundEffectsAudioSource.volume = StateNameController.soundEffectsVolume;
    }

    public void FootSteps()
    {
        GetComponent<AudioSource>().clip = soundManagerInstance.footSteps[Random.Range(0, soundManagerInstance.footSteps.Length)];
        GetComponent<AudioSource>().PlayOneShot(GetComponent<AudioSource>().clip);
    }

    public void BasicAttackConnectionSound()
    {
        soundEffectsAudioSource.clip = soundManagerInstance.attackConnections[Random.Range(0, soundManagerInstance.attackConnections.Length)];
        soundEffectsAudioSource.PlayOneShot(soundEffectsAudioSource.clip);
    }

    public void Uppercut()
    {
        GetComponent<AudioSource>().clip = soundManagerInstance.uppercut;
        GetComponent<AudioSource>().PlayOneShot(GetComponent<AudioSource>().clip);
    }

    public void BodyThud()
    {
        GetComponent<AudioSource>().clip = soundManagerInstance.bodyThud;
        GetComponent<AudioSource>().PlayOneShot(GetComponent<AudioSource>().clip);
    }
}
