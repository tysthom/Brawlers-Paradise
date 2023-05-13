using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiveAttackUI : MonoBehaviour
{
    public GameObject playerDiveAttackPrompt;
    GameObject player;

    private void Start()
    {
        player = GameObject.Find("Player");
    }

    private void Update()
    {
        if (player != null && player.GetComponent<Combat>().isGroundIdle || player.GetComponent<Combat>().isGroundAttacking)
        {
            playerDiveAttackPrompt.SetActive(true);
        }
        else
        {
            playerDiveAttackPrompt.SetActive(false);
        }
    }
}
