using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CorrectRotation : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Quaternion newQuat = new Quaternion();
        newQuat.Set(0, transform.rotation.y, transform.rotation.z, transform.rotation.w);
        transform.rotation = newQuat;

        if (GetComponent<Combat>().faceEnemy)
        {
            Quaternion rotTarget = Quaternion.LookRotation(GetComponent<Combat>().enemy.transform.position - transform.position);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, rotTarget, 100000 * Time.deltaTime);

            transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);
        }
        if (GetComponent<Combat>().faceHead)
        {
            Quaternion rotTarget = Quaternion.LookRotation(GetComponent<Combat>().enemyHead.transform.position - transform.position);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, rotTarget, 100000 * Time.deltaTime);

            transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);
        }
    }
}
