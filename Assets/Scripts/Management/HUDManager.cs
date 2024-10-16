using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HUDManager : MonoBehaviour
{
    public enum hud { classic, minimalist, none}

    public hud hudType;

    GameObject gameManager;
    public GameObject brawler1, brawler2;

    public Image brawler1Health, brawler1HealthFill, brawler1HealthRegen, brawler1ArmourFill, brawler1Stamina, brawler1StaminaFill,
        brawler2Health, brawler2HealthFill, brawler2HealthRegen, brawler2ArmourFill, brawler2Stamina, brawler2StaminaFill;

    public TMP_Text brawler1Name, brawler2Name;

    public TMP_Text finisherText;
    public GameObject finisherPrompt;

    private void Awake()
    {
        gameManager = GameObject.Find("Game Manager");

        brawler1 = gameManager.GetComponent<IdManagear>().brawler1;
        brawler2 = gameManager.GetComponent<IdManagear>().brawler2;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        if (UniversalFight.usingMenuData)
        {
            if(StateNameController.hudSelection == 0)
            {
                hudType = hud.classic;
                brawler1Name.text = "" + StateNameController.b1Name;
                brawler2Name.text = "" + StateNameController.b2Name;
            } 
            else if(StateNameController.hudSelection == 1)
            {
                hudType = hud.minimalist;
            }
            else
            {
                hudType = hud.none;
            }
        }

        if(hudType == hud.classic)
        {
            #region Brawler1 Bars
            GameObject mainCanvas = GameObject.Find("Main Canvas");

            Image[] mainCanvasChildren = mainCanvas.GetComponentsInChildren<Image>();
            foreach (Image child in mainCanvasChildren)
            {
                if (child.gameObject.name == "Brawler1 Health Bar") //Finds b1 health bar gameobject on main canvas
                {
                    brawler1Health = child;
                }

                if (child.gameObject.name == "Brawler1 Stamina Bar") //Finds b1 health bar gameobject on main canvas
                {
                    brawler1Stamina = child;
                }
            }

            Image[] b1Health = brawler1Health.GetComponentsInChildren<Image>();
            foreach (Image child in b1Health)
            {
                if (child.gameObject.name == "Health Fill") //Finds b1 health bar fill on main canvas
                {
                    brawler1HealthFill = child;
                }
                if (child.gameObject.name == "Health Regen Fill") //Finds b1 health regen bar on main canvas
                {
                    brawler1HealthRegen = child;
                }
                if (child.gameObject.name == "Armour Fill") //Finds b1 armour fill on main canvas
                {
                    brawler1ArmourFill = child;
                }
            }

            Image[] b1Stamina = brawler1Stamina.GetComponentsInChildren<Image>();
            foreach (Image child in b1Stamina)
            {
                if (child.gameObject.name == "Stamina Fill") //Finds b1 stamina fill gameobject on main canvas
                {
                    brawler1StaminaFill = child;
                }
            }
            #endregion

            #region Brawler2 Bars 
            foreach (Image child in mainCanvasChildren)
            {
                if (child.gameObject.name == "Brawler2 Health Bar")
                {
                    brawler2Health = child;
                }

                if (child.gameObject.name == "Brawler2 Stamina Bar") //Finds b1 health bar gameobject on main canvas
                {
                    brawler2Stamina = child;
                }
            }

            Image[] b2Health = brawler2Health.GetComponentsInChildren<Image>();
            foreach (Image child in b2Health)
            {
                if (child.gameObject.name == "Health Fill")
                {
                    brawler2HealthFill = child;
                }
                if (child.gameObject.name == "Health Regen Fill")
                {
                    brawler2HealthRegen = child;
                }
                if (child.gameObject.name == "Armour Fill") //Finds b2 armour fill on main canvas
                {
                    brawler2ArmourFill = child;
                }
            }

            Image[] b2Stamina = brawler2Stamina.GetComponentsInChildren<Image>();
            foreach (Image child in b2Stamina)
            {
                if (child.gameObject.name == "Stamina Fill") //Finds b1 stamina fill gameobject on main canvas
                {
                    brawler2StaminaFill = child;
                }
            }
            #endregion
        }
        else if(hudType == hud.minimalist)
        {
            #region Brawler1 Bars
            Image[] brawler1Children = brawler1.GetComponentsInChildren<Image>();
            foreach (Image child in brawler1Children)
            {
                if (child.gameObject.name == "Health Bar") //Finds b1 health bar gameobject on main canvas
                {
                    brawler1Health = child;
                }

                if (child.gameObject.name == "Stamina Bar") //Finds b1 health bar gameobject on main canvas
                {
                    brawler1Stamina = child;
                }
            }

            Image[] b1Health = brawler1Health.GetComponentsInChildren<Image>();
            foreach (Image child in b1Health)
            {
                if (child.gameObject.name == "Health Fill") //Finds b1 health bar fill
                {
                    brawler1HealthFill = child;
                }
                if (child.gameObject.name == "Health Regen Fill") //Finds b1 health regen bar
                {
                    brawler1HealthRegen = child;
                }
                if (child.gameObject.name == "Armour Fill") //Finds b1 armour fill
                {
                    brawler1ArmourFill = child;
                }
            }

            Image[] b1Stamina = brawler1Stamina.GetComponentsInChildren<Image>();
            foreach (Image child in b1Stamina)
            {
                if (child.gameObject.name == "Stamina Fill") //Finds b1 stamina fill gameobject on main canvas
                {
                    brawler1StaminaFill = child;
                }
            }
            #endregion

            #region Brawler2 Bars
            Image[] brawler2Children = brawler2.GetComponentsInChildren<Image>();
            foreach (Image child in brawler2Children)
            {
                if (child.gameObject.name == "Health Bar") //Finds b1 health bar gameobject on main canvas
                {
                    brawler2Health = child;
                }

                if (child.gameObject.name == "Stamina Bar") //Finds b1 health bar gameobject on main canvas
                {
                    brawler2Stamina = child;
                }
            }

            Image[] b2Health = brawler2Health.GetComponentsInChildren<Image>();
            foreach (Image child in b2Health)
            {
                if (child.gameObject.name == "Health Fill") //Finds b1 health bar fill on main canvas
                {
                    brawler2HealthFill = child;
                }
                if (child.gameObject.name == "Health Regen Fill") //Finds b1 health regen bar on main canvas
                {
                    brawler2HealthRegen = child;
                }
                if (child.gameObject.name == "Armour Fill") //Finds b2 armour fill
                {
                    brawler2ArmourFill = child;
                }
            }

            Image[] b2Stamina = brawler2Stamina.GetComponentsInChildren<Image>();
            foreach (Image child in b2Stamina)
            {
                if (child.gameObject.name == "Stamina Fill") //Finds b1 stamina fill gameobject on main canvas
                {
                    brawler2StaminaFill = child;
                }
            }
            #endregion

            brawler1Health.gameObject.SetActive(true);
        }
    }
}
