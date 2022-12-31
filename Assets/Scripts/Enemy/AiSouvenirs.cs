using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiSouvenirs : MonoBehaviour
{
    public GameObject ai;

    private void Awake()
    {
        ai = GameObject.Find("Enemy");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
