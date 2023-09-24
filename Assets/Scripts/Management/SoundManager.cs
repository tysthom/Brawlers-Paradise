using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [Header("Main Menu Sounds")]
    public AudioClip mainMenuMusic;
    public AudioClip buttonPress;

    [Header("Fighting Hits")]
    public AudioClip[] footSteps;
    public AudioClip[] attackConnections;
    public AudioClip[] block;
    public AudioClip finisher;
    public AudioClip uppercut;
    public AudioClip dive;
    public AudioClip eyePoke;
    public AudioClip bearHug;
    public AudioClip bodyThud;
}
