using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [Header("Main Menu Sounds")]
    public AudioClip mainMenuMusic;
    public AudioClip buttonPress;
    public AudioClip[] clothesChange;

    [Header("Fighting Ambiance")]
    public AudioClip crowd;

    [Header("Post Game")]
    public AudioClip victory;

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

    [Header("Fighting Grunts")]
    public AudioClip[] attackGrunt;
    public AudioClip[] strainGrunt;
    public AudioClip[] flinchGrunt;
    public AudioClip dyingGrunt;

    [Header("Throwable")]
    public AudioClip throwableHit;

    [Header("Souvenirs")]
    public AudioClip souvenir;
}
