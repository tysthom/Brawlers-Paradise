using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouristManager : MonoBehaviour
{
    GameObject gameManager;

    public bool canUseTourist;
    public float touristSpawnTime;
    public GameObject touristPrefab;
    public float touristAcquireRadius = 3;
    public bool canAcquire;
    public Collider[] companions;
    GameObject closestCompanion;
    GameObject tour;

    public Vector3 spawnCenter, spawnSize;

    private void Awake()
    {
        gameManager = GameObject.Find("Game Manager");
    }

    void Start()
    {
        touristSpawnTime = Random.Range(5, 6);
        if (canUseTourist)
        {
            StartCoroutine(WaitForToursit(touristSpawnTime));
        }
    }

    IEnumerator WaitForToursit(float time)
    {
        yield return new WaitForSeconds(time);

        Vector3 pos = spawnCenter + new Vector3(Random.Range(-spawnSize.x / 2, spawnSize.x / 2), Random.Range(-spawnSize.y / 2, spawnSize.y / 2), Random.Range(-spawnSize.z / 2, spawnSize.z / 2));

        tour = Instantiate(touristPrefab, pos, Quaternion.identity);
        gameManager.GetComponent<CharacterManager>().tourist = tour;
        gameManager.GetComponent<CharacterManager>().AssignTourist();
        canAcquire = true;
    }

    private void Update()
    {
        if (canAcquire)
        {
            companions = Physics.OverlapSphere(tour.transform.position, touristAcquireRadius);

            for (int i = 0; i < companions.Length; i++)
            {
                if (companions[i].tag == "Player" || companions[i].tag == "Enemy")
                {
                    if (closestCompanion == null)
                    {
                        closestCompanion = companions[i].gameObject;
                    }
                    else if (Vector3.Distance(transform.position, companions[i].transform.position) < Vector3.Distance(transform.position, closestCompanion.transform.position))
                    {
                        closestCompanion = companions[i].gameObject;
                    }
                }
            }
            if (closestCompanion != null)
            {
                canAcquire = false;
               StartCoroutine(tour.GetComponent<Tourist>().CompanionAssigned(closestCompanion));
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(0.7833475f, 0, 1, .5f);
        Gizmos.DrawCube(spawnCenter, spawnSize);
    }
}
