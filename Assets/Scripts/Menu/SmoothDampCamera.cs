using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmoothDampCamera : MonoBehaviour
{

    public bool smoothDamp;
    public Transform target;
    public float speed = 1;
    public Vector3 velocity = Vector3.zero;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(smoothDamp)
        transform.position = Vector3.SmoothDamp(transform.position, target.position, ref velocity, speed * Time.deltaTime);
    }
}
