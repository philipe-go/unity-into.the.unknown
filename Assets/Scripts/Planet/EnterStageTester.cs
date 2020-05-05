using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Sohyun Yi
public class EnterStageTester : MonoBehaviour
{
    private bool doOnce = true;
    public string sceneName;
    public string stageName;
    private Queue<string> phares = new Queue<string>();
    private string enterPhase;
    public bool FirstStage;
    public bool noStage;
    public bool SecondStage;
    public bool LastStage;
    [SerializeField] private bool canEnter;
    public int stageNumber = 0;
    [SerializeField] SpiderAI[] SpiderList;
    [SerializeField] GameObject SpiderAgent;
    public bombExplosion b;

    [SerializeField] private GameObject spawnPlaceHolder; //phil
    #region SFX
    private AudioSource asour;
    [SerializeField] private AudioClip enterStageSound;
    #endregion

    private void Start()
    {
        asour = GetComponent<AudioSource>();
        doOnce = true;
        if (FirstStage)
        {
            b = FindObjectOfType<bombExplosion>();
            phares.Enqueue($"You are at the {stageName} zone. But, you cannot go further.                        ");
            phares.Enqueue("There are spiders protecting them. If you go deeper inside, the spiders will follow and beleaguer you.                      ");
            phares.Enqueue("To use your bomb towards spiders, press the PICK UP button (Q / X).                                     ");
        }

        else
        {
            enterPhase = ($"You entered the {stageName} zone. press ACT ON button (E / Y) to enter the stage                         ");
        }
    }

    private void Update()
    {
        foreach (SpiderAI spd in SpiderList)
        {
            if (spd.isDead == true) canEnter = true;
            else
            {
                canEnter = false;
                break;
            }
        }
        //if (spiderList.Count <= 0) canEnter = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == GameManager.carTag)
        {

            if (!canEnter)
                FindObjectOfType<AIUI>().ShowText(phares);
            else if (canEnter)
                FindObjectOfType<AIUI>().ShowText(enterPhase);

            if (!FirstStage)
            {
                PlanetHandler.spawnerPos = spawnPlaceHolder.transform;
                SpiderAgent.SetActive(true);
                b = FindObjectOfType<bombExplosion>();
            }
            else if (SecondStage)
            {
                if (bombExplosion.secondSpidersDead == true) canEnter = true;
                else canEnter = false;
            }
            else if (LastStage)
            {
                if (bombExplosion.lastSpidersDead == true) canEnter = true;
                else canEnter = false;
            }
            if (noStage)
            {
                canEnter = false;
            }

            b.stageNumber = stageNumber;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == GameManager.carTag)
        {
            if (Input.GetButtonDown("Action4"))
            {
                if (!noStage)
                {
                    if (canEnter)
                    {
                        asour.PlayOneShot(enterStageSound);
                        Invoke("GoInsideShip", 2f);
                        PlanetHandler.spawnerPos = spawnPlaceHolder.transform;
                    }
                    else
                    {
                        FindObjectOfType<AIUI>().ShowText("You have to kill all spider. To use your bomb towards spiders, press the PICK UP button (Q / X).                                     ");
                    }
                }
                else if (noStage)
                {
                    FindObjectOfType<AIUI>().ShowText("This station is locked and empty. There is nothing to do here                                     ");
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == GameManager.carTag)
        {
            SpiderAgent.SetActive(false);
        }
    }

    void GoInsideShip()
    {
        if (doOnce)
        {
            doOnce = false;
            //s.BtnLoadScene(stageName);
            GameManager gm = GameManager.instance;
            doOnce = false;
            gm.LoadNewLevel(sceneName);
            gm.ActivateNewScene();
        }
    }
}
