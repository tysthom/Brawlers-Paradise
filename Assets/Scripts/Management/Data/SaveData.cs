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
    }

    public void LoadBrawler()
    {
        StateNameController.b1MainFightStyleSelection = PlayerPrefs.GetInt("B1 FightStyle");
        StateNameController.b1MainFightStyleSelection = PlayerPrefs.GetInt("B1 OutfitType");
        StateNameController.b1MainFightStyleSelection = PlayerPrefs.GetInt("B1 OutfitVariation");
        StateNameController.b1MainFightStyleSelection = PlayerPrefs.GetInt("B1 HairType");
        StateNameController.b1MainFightStyleSelection = PlayerPrefs.GetInt("B1 HairColor");
        StateNameController.b1MainFightStyleSelection = PlayerPrefs.GetInt("B1 SkinColor");

        GetComponent<MenuManager>().b1FightStyle = StateNameController.b1MainFightStyleSelection;
        GetComponent<MenuManager>().b1OutfitSelection = StateNameController.b1MainFightStyleSelection;
        GetComponent<MenuManager>().b1OutfitVariation = StateNameController.b1MainFightStyleSelection;
        GetComponent<MenuManager>().b1HairType = StateNameController.b1MainFightStyleSelection;
        GetComponent<MenuManager>().b1HairColor = StateNameController.b1MainFightStyleSelection;
        GetComponent<MenuManager>().b1SkinColor = StateNameController.b1MainFightStyleSelection;

        Debug.Log(GetComponent<MenuManager>().b1FightStyle);
    }
}
