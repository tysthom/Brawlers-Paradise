using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmoothDampCamera : MonoBehaviour
{

    public bool smoothDamp;
    public bool mainMenuPos, fightMenuPos;
    public Transform mainMenuTarget, fightMenuTarget;
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
            if (mainMenuPos)
            {
                transform.position = Vector3.SmoothDamp(transform.position, fightMenuTarget.position, ref velocity, speed * Time.deltaTime);
                if (Vector3.Distance(transform.position, fightMenuTarget.position) <= .03f)
                {
                    smoothDamp = false; fightMenuPos = true; mainMenuPos = false;
                }
            }
            else if (fightMenuPos)
            {

                transform.position = Vector3.SmoothDamp(transform.position, mainMenuTarget.position, ref velocity, speed * Time.deltaTime);
                if (Vector3.Distance(transform.position, mainMenuTarget.position) <= .03f)
                {
                    smoothDamp = false; fightMenuPos = false; mainMenuPos = true;
                }
            }
        }
    }
}
