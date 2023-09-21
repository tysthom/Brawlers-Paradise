using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class IdManagear : MonoBehaviour
{
    public enum mode {playerVsAi, AiVsAi, training }

    public mode gameMode;

    public GameObject playerPrefab;
    public GameObject aiPrefab;
    public GameObject[] player;
    GameObject[] ai;
    public GameObject brawler1, brawler2;
    public Camera playerCamera;
    public GameObject liveCamera;
    public Camera spectatorCamera;
    public Camera winnerCamera;
    GameObject[] spawnPositions;
    public GameObject combatManager, characterManager, particleManager;

    private void Awake()
    {
        spawnPositions = GameObject.FindGameObjectsWithTag("Spawners");

        if (UniversalFight.usingMenuData)
        {
            if(StateNameController.gameModeSelection == 0)
            {
                gameMode = mode.training;
            } 
            else if (StateNameController.gameModeSelection == 1)
            {
                gameMode = mode.playerVsAi;
            }
            else
            {
                gameMode = mode.AiVsAi;
            }
        }

        if (gameMode == mode.playerVsAi || gameMode == mode.training)
        {
            GameObject p = Instantiate(playerPrefab, spawnPositions[1].transform);
            GameObject ai1 = Instantiate(aiPrefab, spawnPositions[0].transform);
            p.name = "Player";
            ai1.name = "Enemy";
            if(gameMode == mode.training)
            {
                ai1.GetComponent<AiBehavior>().isPunchingBag = true;
            }
        } else if(gameMode == mode.AiVsAi)
        {
            GameObject ai1 = Instantiate(aiPrefab, spawnPositions[0].transform.position, spawnPositions[0].transform.rotation);
            GameObject ai2 = Instantiate(aiPrefab, spawnPositions[1].transform.position, spawnPositions[1].transform.rotation);
            ai1.name = "Enemy";
            ai2.name = "Enemy";
        }

        player = GameObject.FindGameObjectsWithTag("Player");
        ai = GameObject.FindGameObjectsWithTag("Enemy");
        //spectatorCamera = Camer GameObject.Find("Spectator Camera");
        winnerCamera.enabled = false;

        if (gameMode == mode.playerVsAi || gameMode == mode.training)
        {
            playerCamera.enabled = true;
            spectatorCamera.enabled = false;
            brawler1 = player[0];
            brawler2 = ai[0];

            PlayerInitializationVariables();

            brawler1.GetComponent<BrawlerId>().brawlerId = BrawlerId.Id.brawler1;
            brawler2.GetComponent<BrawlerId>().brawlerId = BrawlerId.Id.brawler2;
        } else if(gameMode == mode.AiVsAi)
        {
            spectatorCamera.enabled = true;
            playerCamera.enabled = false;
            brawler1 = ai[0];
            brawler2 = ai[1];

            brawler1.GetComponent<BrawlerId>().brawlerId = BrawlerId.Id.brawler1;
            brawler2.GetComponent<BrawlerId>().brawlerId = BrawlerId.Id.brawler2;
        }

        //Assigns brawler 1/2 to all other scripts that need it
        combatManager.GetComponent<CombatStats>().brawler1 = brawler1;
        combatManager.GetComponent<CombatStats>().brawler2 = brawler2;
        characterManager.GetComponent<CharacterManager>().brawler1 = brawler1;
        characterManager.GetComponent<CharacterManager>().brawler2 = brawler2;
        particleManager.GetComponent<ParticleManager>().brawler1 = brawler1;
        particleManager.GetComponent<ParticleManager>().brawler2 = brawler2;
    }

    public void PlayerInitializationVariables()
    {
        liveCamera.GetComponent<CinemachineFreeLook>().Follow = player[0].transform;
        liveCamera.GetComponent<CinemachineFreeLook>().LookAt = GameObject.Find("Look Point").transform;
    }
}
