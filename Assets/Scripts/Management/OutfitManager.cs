using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum outfits {Beach, Training };

public class OutfitManager : MonoBehaviour
{
    public outfits playerOutfit;
    public List<GameObject> outfitsparts = new List<GameObject>();
}
