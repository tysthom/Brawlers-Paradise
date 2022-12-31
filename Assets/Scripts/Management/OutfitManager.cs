using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum outfits {Beach, Training };

public class OutfitManager : MonoBehaviour
{
    
    public outfits playerOutfit;
    //public ArrayList outfitsParts = new ArrayList(); 
    public List<GameObject> outfitsparts = new List<GameObject>();

    void Start()
    {
        //outfitsParts.
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
