using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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

    public void SaveFightOptions()
    {
        PlayerPrefs.SetInt("Game Mode Selection", StateNameController.gameModeSelection);
        PlayerPrefs.SetInt("Difficulty Selection", StateNameController.difficultySelection);
        PlayerPrefs.SetInt("Throwable Selection", StateNameController.throwableSelection);
    }

    public void LoadFightOptions()
    {
        StateNameController.gameModeSelection = PlayerPrefs.GetInt("Game Mode Selection");
        StateNameController.difficultySelection = PlayerPrefs.GetInt("Difficulty Selection");
        StateNameController.throwableSelection = PlayerPrefs.GetInt("Throwable Selection");

        GetComponent<MenuManager>().gameModeDropDown.value = StateNameController.gameModeSelection;
        GetComponent<MenuManager>().difficultyDropdown.value = StateNameController.difficultySelection;
        if(StateNameController.throwableSelection == 0)
        {
            GetComponent<MenuManager>().throwableToggle.isOn = true;
        }
        else
        {
            GetComponent<MenuManager>().throwableToggle.isOn = false;
        }
    }

    public void SaveStats()
    {
        PlayerPrefs.SetInt("Total Fights", StateNameController.totalFights);
        PlayerPrefs.SetInt("Total Wins", StateNameController.totalWins);
        PlayerPrefs.SetInt("Win Rate", StateNameController.winRate);
        #region Fight Style Usage
        PlayerPrefs.SetInt("Karate Count", StateNameController.fightStyleUsage[0]);
        PlayerPrefs.SetInt("Boxing Count", StateNameController.fightStyleUsage[1]);
        PlayerPrefs.SetInt("MMA Count", StateNameController.fightStyleUsage[2]);
        PlayerPrefs.SetInt("TKD Count", StateNameController.fightStyleUsage[3]);
        PlayerPrefs.SetInt("Kung Fu Count", StateNameController.fightStyleUsage[4]);
        PlayerPrefs.SetInt("Wrestling Count", StateNameController.fightStyleUsage[5]);
        #endregion
        PlayerPrefs.SetFloat("Attack Connection", StateNameController.avgBasicAttackConnectionRate);
        PlayerPrefs.SetFloat("Damage Inflicted", StateNameController.avgDamageInflicted);
        PlayerPrefs.SetFloat("Health Recovered", StateNameController.avghealthRecovered);
        PlayerPrefs.SetFloat("Stamina Used", StateNameController.avgStaminaUsed);
        PlayerPrefs.SetFloat("Dodges Performed", StateNameController.avgDodgesPerformed);
        #region Souvenir
        PlayerPrefs.SetInt("Medicine Usage", StateNameController.souvenirUsage[0]);
        PlayerPrefs.SetInt("Sunscreen Usage", StateNameController.souvenirUsage[1]);
        PlayerPrefs.SetInt("Coffee Usage", StateNameController.souvenirUsage[2]);
        PlayerPrefs.SetInt("Briefcase Usage", StateNameController.souvenirUsage[3]);
        PlayerPrefs.SetInt("Life Jacket Usage", StateNameController.souvenirUsage[4]);
        PlayerPrefs.SetInt("Rat Poison Usage", StateNameController.souvenirUsage[5]);
        PlayerPrefs.SetInt("Tequila Usage", StateNameController.souvenirUsage[6]);
        PlayerPrefs.SetInt("Floaty Usage", StateNameController.souvenirUsage[7]);
        PlayerPrefs.SetInt("VIP Card Usage", StateNameController.souvenirUsage[8]);
        #endregion
    }

    public void LoadStats()
    {
        StateNameController.totalFights = PlayerPrefs.GetInt("Total Fights");
        StateNameController.totalWins = PlayerPrefs.GetInt("Total Wins");
        StateNameController.winRate  = PlayerPrefs.GetInt("Win Rate");
        #region Fight Style Usage
        StateNameController.fightStyleUsage[0] = PlayerPrefs.GetInt("Karate Count");
        StateNameController.fightStyleUsage[1] = PlayerPrefs.GetInt("Boxing Count");
        StateNameController.fightStyleUsage[2] = PlayerPrefs.GetInt("MMA Count");
        StateNameController.fightStyleUsage[3] = PlayerPrefs.GetInt("TKD Count");
        StateNameController.fightStyleUsage[4] = PlayerPrefs.GetInt("Kung Fu Count");
        StateNameController.fightStyleUsage[5] = PlayerPrefs.GetInt("Wrestling Count");
        #endregion
        StateNameController.avgBasicAttackConnectionRate = PlayerPrefs.GetFloat("Attack Connection");
        StateNameController.avgDamageInflicted = PlayerPrefs.GetFloat("Damage Inflicted");
        StateNameController.avghealthRecovered = PlayerPrefs.GetFloat("Health Recovered");
        StateNameController.avgStaminaUsed = PlayerPrefs.GetFloat("Stamina Used");
        StateNameController.avgDodgesPerformed = PlayerPrefs.GetFloat("Dodges Performed");
        #region Souvenir
        StateNameController.souvenirUsage[0] = PlayerPrefs.GetInt("Medicine Usage");
        StateNameController.souvenirUsage[1] = PlayerPrefs.GetInt("Sunscreen Usage");
        StateNameController.souvenirUsage[2] = PlayerPrefs.GetInt("Coffee Usage");
        StateNameController.souvenirUsage[3] = PlayerPrefs.GetInt("Briefcase Usage");
        StateNameController.souvenirUsage[4] = PlayerPrefs.GetInt("Life Jacket Usage");
        StateNameController.souvenirUsage[5] = PlayerPrefs.GetInt("Rat Poison Usage");
        StateNameController.souvenirUsage[6] = PlayerPrefs.GetInt("Tequila Usage");
        StateNameController.souvenirUsage[7] = PlayerPrefs.GetInt("Floaty Usage");
        StateNameController.souvenirUsage[8] = PlayerPrefs.GetInt("VIP Card Usage");
        #endregion

        if (SceneManager.GetActiveScene().name == "MainMenu")
        {
            GetComponent<MenuManager>().totalFightsText.text = "" + StateNameController.totalFights;
            GetComponent<MenuManager>().winRateText.text = StateNameController.winRate + "%";
            int max = 0;
            int index = -1;
            #region Fight Style Display
            for (int i = 0; i < StateNameController.fightStyleUsage.Length; i++)
            {
                if (max < StateNameController.fightStyleUsage[i])
                {
                    max = StateNameController.fightStyleUsage[i];
                    index = i;
                }
            }
            if (index == 0)
            {
                GetComponent<MenuManager>().fightStyleText.text = "Karate";
            }
            else if (index == 1)
            {
                GetComponent<MenuManager>().fightStyleText.text = "Boxing";
            }
            else if (index == 2)
            {
                GetComponent<MenuManager>().fightStyleText.text = "MMA";
            }
            else if (index == 3)
            {
                GetComponent<MenuManager>().fightStyleText.text = "Taekwondo";
            }
            else if (index == 4)
            {
                GetComponent<MenuManager>().fightStyleText.text = "Kung Fu";
            }
            else if (index == 5)
            {
                GetComponent<MenuManager>().fightStyleText.text = "Pro Wrestling";
            }
            else
            {
                GetComponent<MenuManager>().fightStyleText.text = "N/A";
            }
            #endregion
            GetComponent<MenuManager>().basicAttackText.text = StateNameController.avgBasicAttackConnectionRate + "%";
            GetComponent<MenuManager>().damageText.text = "" + StateNameController.avgDamageInflicted;
            GetComponent<MenuManager>().healthText.text = "" + StateNameController.avghealthRecovered;
            GetComponent<MenuManager>().staminaText.text = "" + StateNameController.avgStaminaUsed;
            GetComponent<MenuManager>().dodgeText.text = "" + StateNameController.avgDodgesPerformed;
            #region Souvenir Text
            max = 0;
            index = -1;
            for (int i = 0; i < StateNameController.souvenirUsage.Length; i++)
            {
                if (max < StateNameController.souvenirUsage[i])
                {
                    max = StateNameController.souvenirUsage[i];
                    index = i;
                }
            }
            if (index == 0)
            {
                GetComponent<MenuManager>().souvenirText.text = "Medicine";
            }
            else if (index == 1)
            {
                GetComponent<MenuManager>().souvenirText.text = "Sunscreen";
            }
            else if (index == 2)
            {
                GetComponent<MenuManager>().souvenirText.text = "Coffee";
            }
            else if (index == 3)
            {
                GetComponent<MenuManager>().souvenirText.text = "Briefcase";
            }
            else if (index == 4)
            {
                GetComponent<MenuManager>().souvenirText.text = "Life Jacket";
            }
            else if (index == 5)
            {
                GetComponent<MenuManager>().souvenirText.text = "Rat Poison";
            }
            else if (index == 6)
            {
                GetComponent<MenuManager>().souvenirText.text = "Tequila";
            }
            else if (index == 7)
            {
                GetComponent<MenuManager>().souvenirText.text = "VIP Card";
            }
            else if (index == 8)
            {
                GetComponent<MenuManager>().souvenirText.text = "Floaty";
            }
            else
            {
                GetComponent<MenuManager>().souvenirText.text = "None";
            }
            #endregion
        }
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
