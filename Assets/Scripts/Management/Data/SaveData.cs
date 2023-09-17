using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveData : MonoBehaviour
{
    public void SaveBrawler()
    {
        PlayerPrefs.SetInt("B1 FightStyle", StateNameController.b1MainFightStyleSelection);
        PlayerPrefs.SetInt("B1 OutfitType", StateNameController.b1MainOutfit);
        PlayerPrefs.SetInt("B1 OutfitVariation", StateNameController.b1MainOutfitVariation);
        PlayerPrefs.SetInt("B1 HairType", StateNameController.b1MainHairType);
        PlayerPrefs.SetInt("B1 HairColor", StateNameController.b1MainHairColor);
        PlayerPrefs.SetInt("B1 SkinColor", StateNameController.b1MainSkinColor);
        PlayerPrefs.SetInt("B1 NameSelection", StateNameController.b1MainNameSelection);
        PlayerPrefs.SetInt("B1 SouvenirSelection", StateNameController.b1MainSouvenirSelection);

        PlayerPrefs.SetInt("B2 FightStyle", StateNameController.b2MainFightStyleSelection);
        PlayerPrefs.SetInt("B2 OutfitType", StateNameController.b2MainOutfit);
        PlayerPrefs.SetInt("B2 OutfitVariation", StateNameController.b2MainOutfitVariation);
        PlayerPrefs.SetInt("B2 HairType", StateNameController.b2MainHairType);
        PlayerPrefs.SetInt("B2 HairColor", StateNameController.b2MainHairColor);
        PlayerPrefs.SetInt("B2 SkinColor", StateNameController.b2MainSkinColor);
        PlayerPrefs.SetInt("B2 NameSelection", StateNameController.b2MainNameSelection);
        PlayerPrefs.SetInt("B2 SouvenirSelection", StateNameController.b2MainSouvenirSelection);
    }

    public void LoadBrawler()
    {
        StateNameController.b1MainFightStyleSelection = PlayerPrefs.GetInt("B1 FightStyle");
        StateNameController.b1MainOutfit = PlayerPrefs.GetInt("B1 OutfitType");
        StateNameController.b1MainOutfitVariation = PlayerPrefs.GetInt("B1 OutfitVariation");
        StateNameController.b1MainHairType = PlayerPrefs.GetInt("B1 HairType");
        StateNameController.b1MainHairColor = PlayerPrefs.GetInt("B1 HairColor");
        StateNameController.b1MainSkinColor = PlayerPrefs.GetInt("B1 SkinColor");
        StateNameController.b1MainNameSelection = PlayerPrefs.GetInt("B1 NameSelection");
        StateNameController.b1MainSouvenirSelection = PlayerPrefs.GetInt("B1 SouvenirSelection");

        GetComponent<MenuManager>().b1FightStyle = StateNameController.b1MainFightStyleSelection;
        GetComponent<MenuManager>().b1OutfitSelection = StateNameController.b1MainOutfit;
        GetComponent<MenuManager>().b1OutfitVariation = StateNameController.b1MainOutfitVariation;
        GetComponent<MenuManager>().b1HairType = StateNameController.b1MainHairType;
        GetComponent<MenuManager>().b1HairColor = StateNameController.b1MainHairColor;
        GetComponent<MenuManager>().b1SkinColor = StateNameController.b1MainSkinColor;
        GetComponent<MenuManager>().b1NameSelection = StateNameController.b1MainNameSelection;
        GetComponent<MenuManager>().b1SouvenirSelection = StateNameController.b1MainSouvenirSelection;

        StateNameController.b2MainFightStyleSelection = PlayerPrefs.GetInt("B2 FightStyle");
        StateNameController.b2MainOutfit = PlayerPrefs.GetInt("B2 OutfitType");
        StateNameController.b2MainOutfitVariation = PlayerPrefs.GetInt("B2 OutfitVariation");
        StateNameController.b2MainHairType = PlayerPrefs.GetInt("B2 HairType");
        StateNameController.b2MainHairColor = PlayerPrefs.GetInt("B2 HairColor");
        StateNameController.b2MainSkinColor = PlayerPrefs.GetInt("B2 SkinColor");
        StateNameController.b2MainNameSelection = PlayerPrefs.GetInt("B2 NameSelection");
        StateNameController.b2MainSouvenirSelection = PlayerPrefs.GetInt("B2 SouvenirSelection");

        GetComponent<MenuManager>().b2FightStyle = StateNameController.b2MainFightStyleSelection;
        GetComponent<MenuManager>().b2OutfitSelection = StateNameController.b2MainOutfit;
        GetComponent<MenuManager>().b2OutfitVariation = StateNameController.b2MainOutfitVariation;
        GetComponent<MenuManager>().b2HairType = StateNameController.b2MainHairType;
        GetComponent<MenuManager>().b2HairColor = StateNameController.b2MainHairColor;
        GetComponent<MenuManager>().b2SkinColor = StateNameController.b2MainSkinColor;
        GetComponent<MenuManager>().b2NameSelection = StateNameController.b2MainNameSelection;
        GetComponent<MenuManager>().b2SouvenirSelection = StateNameController.b2MainSouvenirSelection;
    }

    public void SaveOptions()
    {
        PlayerPrefs.SetInt("HUD Selection", StateNameController.hudSelection);
        PlayerPrefs.SetInt("Controller Vibration", StateNameController.controllerVibration);
        PlayerPrefs.SetFloat("Music Value", StateNameController.musicVolume);
        PlayerPrefs.SetFloat("Sound Effects Value", StateNameController.soundEffectsVolume);
    }

    public void LoadOptions()
    {
        StateNameController.hudSelection = PlayerPrefs.GetInt("HUD Selection");
        StateNameController.controllerVibration = PlayerPrefs.GetInt("Controller Vibration");
        StateNameController.musicVolume = PlayerPrefs.GetFloat("Music Value");
        StateNameController.soundEffectsVolume = PlayerPrefs.GetFloat("Sound Effects Value");

        GetComponent<MenuManager>().hudSelectionDropdown.value = StateNameController.hudSelection;
        if(StateNameController.controllerVibration == 0)
        {
            GetComponent<MenuManager>().vibrationToggle.isOn = true;
        }
        else
        {
            GetComponent<MenuManager>().vibrationToggle.isOn = false;
        }
        GetComponent<MenuManager>().musicSlider.value = StateNameController.musicVolume;
        GetComponent<MenuManager>().soundEffectsSlider.value = StateNameController.soundEffectsVolume;
    }
}
