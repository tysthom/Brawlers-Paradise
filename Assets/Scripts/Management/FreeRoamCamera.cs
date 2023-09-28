using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreeRoamCamera : MonoBehaviour
{
    [Header("Refrences")]
    float primary;
    float rotX, rotY;
    Rigidbody rb;

    [Header("Status")]
    public bool isSprinting;

    [Header("Stats")]
    public float speed = 5, sprintSpeed = 15;
    [Range(0, 100)] public float sensitivityX = 60, sensitivityY = 45;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        sensitivityX /= 50;
        sensitivityY /= 50;
        speed *= 500;
        sprintSpeed *= 500;
    }
    void LateUpdate()
    {
        primary = Input.GetAxis("Primary");
        if (primary != 0)
        {
            isSprinting = true;
        }
        else
        {
            isSprinting = false;
        }

        //Get controller inputs
        rotX += Input.GetAxis("Controller X") * sensitivityX;
        rotY += Input.GetAxis("Controller Y") * sensitivityY * -1;

        //Clamp vertical rotation
        rotY = Mathf.Clamp(rotY, -90, 90);

        //Rotate camera vertically  
        transform.rotation = Quaternion.Euler(-rotY, rotX, 0);

        //Movement refrences
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        //Movement
        rb.velocity = Vector3.zero;
        if (!isSprinting)
        {
            rb.velocity += transform.forward * z * Time.deltaTime * speed;
            rb.velocity += transform.right * x * Time.deltaTime * speed;
        }
        else
        {
            rb.velocity += transform.forward * z * Time.deltaTime * sprintSpeed;
            rb.velocity += transform.right * x * Time.deltaTime * sprintSpeed;
        }
    }
}
