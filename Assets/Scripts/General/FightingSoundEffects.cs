using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightingSoundEffects : MonoBehaviour
{
    GameObject soundManager;
    SoundManager soundManagerInstance;
    AudioSource soundEffectsAudioSource;
    AudioSource crowdAudioSource;

    int previousAttackGrunt = -1;
    int previousFlinchGrunt = -1;

    void Start()
    {
        soundManager = GameObject.Find("Sound Manager");
        soundManagerInstance = soundManager.GetComponent<SoundManager>();
        soundEffectsAudioSource = GameObject.Find("Sound Effects").GetComponent<AudioSource>();
        soundEffectsAudioSource.volume = StateNameController.soundEffectsVolume;
        crowdAudioSource = GameObject.Find("Crowd").GetComponent<AudioSource>();
        crowdAudioSource.volume = StateNameController.soundEffectsVolume;

        GetComponent<AudioSource>().volume = StateNameController.soundEffectsVolume;
    }

    private void Update()
    {
        if(UniversalFight.usingMenuData)
            soundEffectsAudioSource.volume = StateNameController.soundEffectsVolume;
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


    public void Block()
    {
        soundEffectsAudioSource.clip = soundManagerInstance.block[Random.Range(0, soundManagerInstance.block.Length)];
        soundEffectsAudioSource.PlayOneShot(soundEffectsAudioSource.clip);
    }

    public void Finisher()
    {
        GetComponent<AudioSource>().clip = soundManagerInstance.finisher;
        GetComponent<AudioSource>().PlayOneShot(GetComponent<AudioSource>().clip);
    }

    public void Uppercut()
    {
        GetComponent<AudioSource>().clip = soundManagerInstance.uppercut;
        GetComponent<AudioSource>().PlayOneShot(GetComponent<AudioSource>().clip);
    }

    public void DiveSound()
    {
        GetComponent<AudioSource>().clip = soundManagerInstance.dive;
        GetComponent<AudioSource>().PlayOneShot(GetComponent<AudioSource>().clip);
    }

    public void EyePoke()
    {
        GetComponent<AudioSource>().clip = soundManagerInstance.eyePoke;
        GetComponent<AudioSource>().PlayOneShot(GetComponent<AudioSource>().clip);
    }

    public void BearHug()
    {
        GetComponent<AudioSource>().clip = soundManagerInstance.bearHug;
        GetComponent<AudioSource>().PlayOneShot(GetComponent<AudioSource>().clip);
    }

    public void BodyThud()
    {
        GetComponent<AudioSource>().clip = soundManagerInstance.bodyThud;
        GetComponent<AudioSource>().PlayOneShot(GetComponent<AudioSource>().clip);
    }

    public void AttackGrunt()
    {
        int i = Random.Range(1, 10);
        if (i <= 5)
        {
            int j = Random.Range(0, soundManagerInstance.attackGrunt.Length);
            while (j == previousAttackGrunt)
            {
                j = Random.Range(0, soundManagerInstance.attackGrunt.Length);
            }

            soundEffectsAudioSource.clip = soundManagerInstance.attackGrunt[j];
            soundEffectsAudioSource.PlayOneShot(soundEffectsAudioSource.clip);
            previousAttackGrunt = j;
        }
    }

    public void StrainGrunt()
    {
        if (GetComponent<FightStyle>().fightStyle == FightStyle.fightStyles.boxing)
        {
            soundEffectsAudioSource.clip = soundManagerInstance.strainGrunt[0];
        }
        else if (GetComponent<FightStyle>().fightStyle == FightStyle.fightStyles.kungFu)
        {
            soundEffectsAudioSource.clip = soundManagerInstance.strainGrunt[1];
        }
        
        soundEffectsAudioSource.PlayOneShot(soundEffectsAudioSource.clip);
    }

    public void FlinchGrunt()
    {
        int i = Random.Range(1, 10);
        if (i <= 5)
        {
            int j = Random.Range(0, soundManagerInstance.flinchGrunt.Length);
            while (j == previousFlinchGrunt)
            {
                j = Random.Range(0, soundManagerInstance.flinchGrunt.Length);
            }

            soundEffectsAudioSource.clip = soundManagerInstance.flinchGrunt[Random.Range(0, soundManagerInstance.flinchGrunt.Length)];
            soundEffectsAudioSource.PlayOneShot(soundEffectsAudioSource.clip);
            previousFlinchGrunt = j;
        }
    }

    public void DyingGrunt()
    {
        GetComponent<AudioSource>().clip = soundManagerInstance.dyingGrunt;
        GetComponent<AudioSource>().PlayOneShot(GetComponent<AudioSource>().clip);
    }

    public IEnumerator BearHuggedGrunt(GameObject brawler)
    {
        while (brawler.GetComponent<Flinch>().isBearhugged)
        {
            soundEffectsAudioSource.clip = soundManagerInstance.flinchGrunt[Random.Range(0, soundManagerInstance.flinchGrunt.Length)];
            soundEffectsAudioSource.PlayOneShot(soundEffectsAudioSource.clip);

            yield return new WaitForSeconds(1.5f);
        }
    }

    public void ThrowableHit()
    {
        GetComponent<AudioSource>().clip = soundManagerInstance.throwableHit;
        GetComponent<AudioSource>().PlayOneShot(GetComponent<AudioSource>().clip);
    }

    public void Souvenir()
    {
        GetComponent<AudioSource>().clip = soundManagerInstance.souvenir;
        GetComponent<AudioSource>().PlayOneShot(GetComponent<AudioSource>().clip);
    }

    public void Victory()
    {
        GameObject.Find("Sound Effects").GetComponent<AudioSource>().PlayOneShot(soundManagerInstance.victory);
    }
}
