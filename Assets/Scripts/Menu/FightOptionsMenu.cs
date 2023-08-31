using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightOptionsMenu : MonoBehaviour
{
    GameObject menuManager;
    MenuManager menuManagerInstance;
    public GameObject brawler1, brawler2;

    [Header("Souvenir")]
    public int b1SouvenirSelection;
    public int b2SouvenirSelection;
    public string[] souvniers = { "Medicine", "Sunscreen", "Coffee", "Briefcase", "Rat Poison", "Life Jacket", "Tequila",
        "VIP Card", "Floaty", "None"};

    // Start is called before the first frame update
    void Start()
    {
        menuManager = GameObject.Find("Menu Manager");
        menuManagerInstance = menuManager.GetComponent<MenuManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if(b1SouvenirSelection != menuManagerInstance.b1SouvenirSelection || b2SouvenirSelection != menuManagerInstance.b2SouvenirSelection)
        {
            //Do Somethin Cool!
        }

        b1SouvenirSelection = menuManagerInstance.b1SouvenirSelection;
        b2SouvenirSelection = menuManagerInstance.b2SouvenirSelection;
    }
}
