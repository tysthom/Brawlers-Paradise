using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class GrowingText : MonoBehaviour
{
    public float largeScaleSize = 3;
    float normalScaleSize;
    GameObject currentSelected;
    GameObject eventSystem;

    private void Start()
    {
        eventSystem = GameObject.Find("EventSystem");
        normalScaleSize = GetComponent<RectTransform>().localScale.x;
    }

    void Update()
    {
        currentSelected = eventSystem.GetComponent<UnityEngine.EventSystems.EventSystem>().currentSelectedGameObject;

        if (currentSelected.name == name)
        {
            GetComponent<RectTransform>().localScale = new Vector3(largeScaleSize, largeScaleSize, largeScaleSize);
        }
        else
        {
            if(GetComponent<RectTransform>().localScale.x > 2)
            {
                GetComponent<RectTransform>().localScale = new Vector3(normalScaleSize, normalScaleSize, normalScaleSize);
            }
        }
    }
}
