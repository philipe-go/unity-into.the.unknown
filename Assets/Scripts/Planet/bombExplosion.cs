using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Sohyun Yi
public class bombExplosion : MonoBehaviour
{
    [SerializeField] private float force = 10f;
    [SerializeField] private GameObject bombProjectile;

    [SerializeField] private AudioSource ExplosionSound;
    [SerializeField] private AudioSource DeadSound;
    [SerializeField] private GameObject[] SpidersSecond;
    [SerializeField] private GameObject[] SpidersFinal;
    [SerializeField] private GameObject[] SpidersFirst;
    [SerializeField] private GameObject[] SpidersThirdStage;
    [SerializeField] private Animator[] SpiderAnim;
    public bool SpiderIsDead = false;
    public static bool firstSpidersDead = false;
    public static bool secondSpidersDead = false;
    public static bool lastSpidersDead = false;
    public int stageNumber;

    private void Awake()
    {
        stageNumber = 1;
    }

    void Start()
    {
        //for (int i = 0; i < Spiders.Length; i++)
        //{
        //    SpiderAnim[i] = Spiders[i].GetComponent<Animator>();
        //}
    }
    void Update()
    {
        if (Input.GetButtonDown("Action3"))
        {
            ExplodeBomb();

            Invoke("Boom", 3f);
            Invoke("SpiderDead", 10f);
        }

    }
    void ExplodeBomb()
    {
        GameObject bomb = Instantiate(bombProjectile, transform.position, transform.rotation);
        Rigidbody rigid = bombProjectile.GetComponent<Rigidbody>();
        rigid.AddForce(transform.forward * force, ForceMode.Impulse);
    }
    void Boom()
    {
        ExplosionSound.Play();
        for (int i = 0; i < SpiderAnim.Length; i++)
        {
            SpiderAnim[i].SetTrigger("Dead");
        }
        //SpiderIsDead = true;
    }

    void SpiderDead()
    {
        DeadSound.Play();
        FindObjectOfType<AIUI>().ShowText("You have slained the spiders. Press ACT ON (E / Y) button to enter the spaceship.                             ");
        if (stageNumber == 1)
        {
            firstSpidersDead = true;
            for (int i = 0; i < SpidersFirst.Length; i++)
            {
                Destroy(SpidersFirst[i]);
            }
        }
        else if (stageNumber == 2)
        {
            secondSpidersDead = true;
            for (int i = 0; i < SpidersFirst.Length; i++)
            {
                Destroy(SpidersSecond[i]);
            }
        }
        else if (stageNumber == 3)
        {
            lastSpidersDead = true;
            for (int i = 0; i < SpidersFirst.Length; i++)
            {
                Destroy(SpidersFinal[i]);
            }
        }
        else if (stageNumber == 0)
        {
            for (int i = 0; i < SpidersFirst.Length - 1; i++)
            {
                Destroy(SpidersThirdStage[i]);
            }
        }
    }

    /*
    [SerializeField] private float force = 10f;
    [SerializeField] private float timer = 30f;
    [SerializeField] private bool TimerIsActive = true;

    [SerializeField] private GameObject bombPrefab;
    [SerializeField] private GameObject CamFPS;
    [SerializeField] private GameObject CamTPS;
    [SerializeField] private float strength = 15f;
    [SerializeField] private AudioSource ExplosionSound;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
            if (Input.GetKeyDown("B"))
            {
                GameObject bomb = Instantiate(bombPrefab, transform.position, transform.rotation);
                bomb.GetComponent<Rigidbody>().AddForce(transform.forward * strength, ForceMode.Impulse);
                ExplosionSound.Play();
            }
    }



    private void OnCollisionEnter(Collision collision)
    {

        //Add force
        Rigidbody rigid = bombPrefab.GetComponent<Rigidbody>();
        rigid.AddForce(transform.forward * -force, ForceMode.Impulse); //Bottom

        if (TimerIsActive)
        {
            Invoke("BombExplosion", timer);
        }

    }

    void BombExplosion()
    {
        Destroy(gameObject);
    }

    private void OnDestroy()
    {
        Destroy(bombPrefab);
    }*/
}
