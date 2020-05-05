using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//by Philipe Gouveia

public class GameBegin : MonoBehaviour
{
    //script done to handle the animations at the beginning of the game

    [Header("Tag on Players Characters and Zion")]
    //Animator for the Players Characters and Zion to handle at the beginning of the game
    [SerializeField] private Animator engineerAnim;
    [SerializeField] private Animator sphereAnim;
    [SerializeField] private Animator zionAnim;
    [Space]
    [Header("Zion Game Object")]
    [SerializeField] GameObject zion;
    [SerializeField] GameObject zionLight;
    [SerializeField] KillSpider spider;

    private CameraRigHandler camRig;

    private void Start()
    {
        engineerAnim = GameObject.FindGameObjectWithTag(GameManager.engineerTag).GetComponent<Animator>();
        sphereAnim = GameObject.FindGameObjectWithTag(GameManager.sphereTag).GetComponent<Animator>();
        zionAnim = GameObject.FindGameObjectWithTag(GameManager.zionTag).GetComponent<Animator>();
        spider = GameObject.FindObjectOfType<KillSpider>();
        camRig = GameObject.FindObjectOfType<CameraRigHandler>();

        if (zion) zionAnim.SetBool("walkAway", false);
        FindObjectOfType<GameManager>().sphereOn = false;
        FindObjectOfType<GameManager>().engineerOn = true;

        StartCoroutine(Begin(6f));
    }

    //private void Update()
    //{
    //    if (Input.GetKeyDown(KeyCode.Backspace)) //TO BE removed - Just for testing purpose
    //    {
    //        FindObjectOfType<GameManager>().engineerOn = true;
    //        Destroy(zion);
    //        Destroy(zionLight);
    //        Destroy(this);
    //        GameObject.FindObjectOfType<SwitchBridge>().OnClick();
    //        GameObject.FindObjectOfType<SwitchSphere>().OnClick();

    //        SwitchWall[] tempWall = GameObject.FindObjectsOfType<SwitchWall>();
    //        foreach (SwitchWall obj in tempWall)
    //        {
    //            obj.OnClick();
    //        }

    //        SwitchFloor[] temp = GameObject.FindObjectsOfType<SwitchFloor>();
    //        foreach (SwitchFloor obj in temp)
    //        {
    //            obj.OnOpen();
    //        }

    //        spider.Die();

    //        FindObjectOfType<AIInstructions>().beginTutorial = true;
    //    }
    //}

    IEnumerator Begin(float time)
    {
        yield return new WaitForSeconds(time * 0.7f);
        if (zion) zionAnim.SetBool("walkAway", true);
        yield return new WaitForSeconds(time * 0.1f);
        if (zion) zionAnim.SetBool("endScene", true);
        FindObjectOfType<AIInstructions>().beginTutorial = true;
        yield return new WaitForSeconds(time * 0.4f);
        Destroy(zionLight);
        Destroy(zion);
        yield return new WaitForSeconds(time * 0.3f);
        if (camRig.index == 0)
        {
            CameraRigHandler.isTopView = true;
            camRig.IndexChanger(+1);
        }
        StopCoroutine("Begin");
        Destroy(this);
    }
}
