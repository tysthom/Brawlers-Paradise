using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateNameController : MonoBehaviour
{
    public static bool useMainMenu = true;

    public static int b1MainFightStyleSelection;
    public static int b1MainOutfit;
    public static int b1MainOutfitVariation;
    public static int b1MainHairType;
    public static int b1MainHairColor;
    public static int b1MainSkinColor;
    public static int b1MainNameSelection;
    public static string b1Name;
    public static int b1MainSouvenirSelection;

    public static int b2MainFightStyleSelection;
    public static int b2MainOutfit;
    public static int b2MainOutfitVariation;
    public static int b2MainHairType;
    public static int b2MainHairColor;
    public static int b2MainSkinColor;
    public static int b2MainNameSelection;
    public static string b2Name;
    public static int b2MainSouvenirSelection;

    public static int gameModeSelection;
    public static int difficultySelection;
    public static int throwableSelection; //0 YES 1 NO

    public static int totalFights;
    public static int totalWins;
    public static float winRate;
    public static int[] fightStyleUsage = new int[6];
    //Karate, Boxing, MMA, TKD, Kung Fu, Wrestling
    public static float avgBasicAttackConnectionRate;
    public static float avgDamageInflicted;
    public static float avghealthRecovered;
    public static float avgStaminaUsed;
    public static float avgDodgesPerformed;
    public static int[] souvenirUsage = new int[9];
    // Medicine, Sunscreen, Coffee, Briefcase, LifeJacket, RatPoison, Tequila, VIPCard, Floaty, None

    public static int hudSelection;
    public static int controllerVibration; //0 YES 1 NO
    public static float musicVolume;
    public static float soundEffectsVolume;
}
