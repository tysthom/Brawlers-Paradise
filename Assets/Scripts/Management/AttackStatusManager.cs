using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AttackStatusManager : MonoBehaviour
{
    GameObject gameManager;
    IdManagear idManagerInstance;

    public TextMeshProUGUI b1AttackStatusDisplay, b2AttackStatusDisplay;

    public Color counterHit, punishHit, knockdown, stun, dive, parry, poke, bearHug;

    Coroutine b1Fade, b2Fade;

    private void Awake()
    {
        gameManager = GameObject.Find("Game Manager");
        idManagerInstance = gameManager.GetComponent<IdManagear>();

        b1AttackStatusDisplay.text = "";
        b2AttackStatusDisplay.text = "";
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CounterHit(GameObject brawler)
    {
        if(brawler == idManagerInstance.brawler1)
        {
            b1AttackStatusDisplay.text = "COUNTER HIT";
            b1AttackStatusDisplay.color = counterHit;

            if(b1Fade != null)
            {
                StopCoroutine(b1Fade);
            }
            b1Fade = StartCoroutine(B1WaitToFade());
        }
        else if (brawler == idManagerInstance.brawler2)
        {
            b2AttackStatusDisplay.text = "COUNTER HIT";
            b2AttackStatusDisplay.color = counterHit;

            if (b2Fade != null)
            {
                StopCoroutine(b2Fade);
            }
            b2Fade = StartCoroutine(B2WaitToFade());
        }
        else
        {
            Debug.Log("Error");
        }
    }

    public void PunishHit(GameObject brawler)
    {
        if (brawler == idManagerInstance.brawler1)
        {
            b1AttackStatusDisplay.text = "PUNISH HIT";
            b1AttackStatusDisplay.color = punishHit;

            if (b1Fade != null)
            {
                StopCoroutine(b1Fade);
            }
            b1Fade = StartCoroutine(B1WaitToFade());
        }
        else if (brawler == idManagerInstance.brawler2)
        {
            b2AttackStatusDisplay.text = "PUNISH HIT";
            b2AttackStatusDisplay.color = punishHit;

            if (b2Fade != null)
            {
                StopCoroutine(b2Fade);
            }
            b2Fade = StartCoroutine(B2WaitToFade());
        }
        else
        {
            Debug.Log("Error");
        }
    }

    public void Knockdown(GameObject brawler)
    {
        if (brawler == idManagerInstance.brawler1)
        {
            b1AttackStatusDisplay.text = "KNOCKDOWN";
            b1AttackStatusDisplay.color = knockdown;

            if (b1Fade != null)
            {
                StopCoroutine(b1Fade);
            }
            b1Fade = StartCoroutine(B1WaitToFade());
        }
        else if (brawler == idManagerInstance.brawler2)
        {
            b2AttackStatusDisplay.text = "KNOCKDOWN";
            b2AttackStatusDisplay.color = knockdown;

            if (b2Fade != null)
            {
                StopCoroutine(b2Fade);
            }
            b2Fade = StartCoroutine(B2WaitToFade());
        }
        else
        {
            Debug.Log("Error");
        }
    }

    public void Stun(GameObject brawler)
    {
        if (brawler == idManagerInstance.brawler1)
        {
            b1AttackStatusDisplay.text = "STUN";
            b1AttackStatusDisplay.color = stun;

            if (b1Fade != null)
            {
                StopCoroutine(b1Fade);
            }
            b1Fade = StartCoroutine(B1WaitToFade());
        }
        else if (brawler == idManagerInstance.brawler2)
        {
            b2AttackStatusDisplay.text = "STUN";
            b2AttackStatusDisplay.color = stun;

            if (b2Fade != null)
            {
                StopCoroutine(b2Fade);
            }
            b2Fade = StartCoroutine(B2WaitToFade());
        }
        else
        {
            Debug.Log("Error");
        }
    }

    public void Dive(GameObject brawler)
    {
        if (brawler == idManagerInstance.brawler1)
        {
            b1AttackStatusDisplay.text = "DIVE";
            b1AttackStatusDisplay.color = dive;

            if (b1Fade != null)
            {
                StopCoroutine(b1Fade);
            }
            b1Fade = StartCoroutine(B1WaitToFade());
        }
        else if (brawler == idManagerInstance.brawler2)
        {
            b2AttackStatusDisplay.text = "DIVE";
            b2AttackStatusDisplay.color = dive;

            if (b2Fade != null)
            {
                StopCoroutine(b2Fade);
            }
            b2Fade = StartCoroutine(B2WaitToFade());
        }
        else
        {
            Debug.Log("Error");
        }
    }

    public void Parry(GameObject brawler)
    {
        if (brawler == idManagerInstance.brawler1)
        {
            b1AttackStatusDisplay.text = "PARRY";
            b1AttackStatusDisplay.color = parry;

            if (b1Fade != null)
            {
                StopCoroutine(b1Fade);
            }
            b1Fade = StartCoroutine(B1WaitToFade());
        }
        else if (brawler == idManagerInstance.brawler2)
        {
            b2AttackStatusDisplay.text = "PARRY";
            b2AttackStatusDisplay.color = parry;

            if (b2Fade != null)
            {
                StopCoroutine(b2Fade);
            }
            b2Fade = StartCoroutine(B2WaitToFade());
        }
        else
        {
            Debug.Log("Error");
        }
    }

    public void Poke(GameObject brawler)
    {
        if (brawler == idManagerInstance.brawler1)
        {
            b1AttackStatusDisplay.text = "POKE";
            b1AttackStatusDisplay.color = poke;

            if (b1Fade != null)
            {
                StopCoroutine(b1Fade);
            }
            b1Fade = StartCoroutine(B1WaitToFade());
        }
        else if (brawler == idManagerInstance.brawler2)
        {
            b2AttackStatusDisplay.text = "POKE";
            b2AttackStatusDisplay.color = poke;

            if (b2Fade != null)
            {
                StopCoroutine(b2Fade);
            }
            b2Fade = StartCoroutine(B2WaitToFade());
        }
        else
        {
            Debug.Log("Error");
        }
    }

    public void BearHug(GameObject brawler)
    {
        if (brawler == idManagerInstance.brawler1)
        {
            b1AttackStatusDisplay.text = "BEAR HUG";
            b1AttackStatusDisplay.color = bearHug;

            if (b1Fade != null)
            {
                StopCoroutine(b1Fade);
            }
            b1Fade = StartCoroutine(B1WaitToFade());
        }
        else if (brawler == idManagerInstance.brawler2)
        {
            b2AttackStatusDisplay.text = "BEAR HUG";
            b2AttackStatusDisplay.color = bearHug;

            if (b2Fade != null)
            {
                StopCoroutine(b2Fade);
            }
            b2Fade = StartCoroutine(B2WaitToFade());
        }
        else
        {
            Debug.Log("Error");
        }
    }

    IEnumerator B1WaitToFade()
    {
        b1AttackStatusDisplay.alpha = 1;
        b1AttackStatusDisplay.gameObject.GetComponent<Fade>().fadeOut = false;
        yield return new WaitForSeconds(.1f);
        b1AttackStatusDisplay.gameObject.GetComponent<Fade>().fadeOut = true;
    }

    IEnumerator B2WaitToFade()
    {
        b2AttackStatusDisplay.alpha = 1;
        b2AttackStatusDisplay.gameObject.GetComponent<Fade>().fadeOut = false;
        yield return new WaitForSeconds(.1f);
        b2AttackStatusDisplay.gameObject.GetComponent<Fade>().fadeOut = true;
    }
}
