using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

//Sohyun Yi
public class SpiderAI : MonoBehaviour
{
    //NavMeshAgent SpiderAgent = null;
    [SerializeField] Transform[] Point = null;
    int AgentNumber = 10;
    Transform OriginalPosition = null;
    [SerializeField] Transform PlayerPosition = null;
    private bombExplosion b = null;
    public float speed = 3f;
    public bool isDead = false;

    // Start is called before the first frame update
    void Start()
    {
        b = FindObjectOfType<bombExplosion>();
        //SpiderAgent = GetComponent<NavMeshAgent>();
        //InvokeRepeating("MoveSpider", 0f, 0.5f);
        OriginalPosition = GetComponent<Transform>();
        PlayerPosition = FindObjectOfType<CarHandler>().transform;
    }

    private void Update()
    {
        if (b != null)
        {
            if (Input.GetButtonDown("Action3"))
            {
                isDead = true;
            }
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!isDead)
        {
            MoveSpider();
        }
    }

    void MoveSpider()
    {
        //if (PlayerPosition == null)
        //{
        //    if (SpiderAgent.velocity == Vector3.zero)
        //    {
        //        SpiderAgent.SetDestination(Point[Random.Range(0, AgentNumber)].position);


        //        if (AgentNumber >= Point.Length)
        //        {
        //            {
        //                AgentNumber = 0;
        //            }
        //        }
        //    }
        //}
        if (PlayerPosition != null)
        {
            //SpiderAgent.SetDestination(PlayerPosition.position);
            transform.position = Vector3.MoveTowards(transform.position, PlayerPosition.position, speed);
            transform.LookAt(PlayerPosition.position);
        }
    }


    //private void OnCollisionEnter(Collision collision)
    //{
    //    if(collision.gameObject.tag == "Enemy")
    //    {
    //        SpiderAgent.SetDestination(OriginalPosition.position);
    //    }
    //}

    public void PlayerEnter(Transform playerPosition)
    {
        CancelInvoke();
        PlayerPosition = playerPosition;
    }

    public void PlayerExit(Transform playerPosition)
    {
        PlayerPosition = null;
        playerPosition = null;
        InvokeRepeating("MoveSpider", 0f, 0.5f);
    }




}
