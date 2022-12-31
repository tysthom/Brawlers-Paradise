using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawn : MonoBehaviour
{
    public GameObject respawnPoint;
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Throwable")
        {
            collision.transform.position = respawnPoint.transform.position;
        }
    }
}
