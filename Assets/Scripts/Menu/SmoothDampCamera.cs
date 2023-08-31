using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmoothDampCamera : MonoBehaviour
{

    public bool smoothDamp;
    public Transform fightMenuTarget, fightOptionsMenuTarget;
    public float speed = 1;
    Vector3 velocity = Vector3.zero;
    GameObject menuManager;

    // Start is called before the first frame update
    void Start()
    {
        menuManager = GameObject.Find("Menu Manager");
    }

    // Update is called once per frame
    void Update()
    {
        if (smoothDamp)
        {
            if(menuManager.GetComponent<MenuManager>().currentMenu == "Fight Menu")
            {
                transform.position = Vector3.SmoothDamp(transform.position, fightMenuTarget.position, ref velocity, speed * Time.deltaTime);
                if (Vector3.Distance(transform.position, fightMenuTarget.position) < .01)
                    smoothDamp = false;
            } 
            else if(menuManager.GetComponent<MenuManager>().currentMenu == "Something Else")
            {
                transform.position = Vector3.SmoothDamp(transform.position, fightOptionsMenuTarget.position, ref velocity, speed * Time.deltaTime);
                if (Vector3.Distance(transform.position, fightOptionsMenuTarget.position) < .01)
                    smoothDamp = false;
            }
        }
    }
}
