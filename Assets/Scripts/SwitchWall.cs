using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//by Philipe Gouveia and Sohyun Yi

public class SwitchWall : MonoBehaviour
{
    [Header("Result of this switch")]
    [Tooltip("Put here the GameObject that will react when this switch is activated")]
    [SerializeField] private GameObject resultObj;
    [Header("Player who will act on this switch:")]
    [Tooltip("Check here the player who will be responsible to act on this switch")]
    public bool sphereAct = false;
    public bool engineerAct = false;

    [Space]
    [Header("Has Switch Off option")]
    public bool hasSwitchOff = false;

    [Space]
    [Header("Output AI instruction?")]
    [Tooltip("Check and write here if there is any instruction the AI will give to the player or the characters")]
    public bool outputInstruction = false;
    public string aiInstruction = "";

    #region Attributes
    private Material mat;
    private bool activated = false;
    #endregion

    #region SFX 
    //by Philipe Gouveia
    [Header("SFX")]
    private AudioSource sfx;
    [SerializeField] private AudioClip clickSFX;
    #endregion

    void Start()
    {
        sfx = GetComponent<AudioSource>();
        mat = GetComponent<Renderer>().material;
    }

    private void OnTriggerStay(Collider other)
    {
        if ((sphereAct && other.gameObject.tag == GameManager.sphereTag) || (engineerAct && other.gameObject.tag == GameManager.engineerTag))
        {
            if (Input.GetButtonDown("Action4"))
            {
                if (!activated)
                {
                    OnClick();
                    activated = true;
                }
                else if (hasSwitchOff)
                {
                    SwitchOff();
                    activated = false;
                }
            }
        }
    }

    public void OnClick()
    {
        sfx.PlayOneShot(clickSFX);
        mat.SetColor("_EmissionColor", Color.green);
        resultObj.GetComponent<Animator>().SetBool("isOpen", true);
        if (outputInstruction) { FindObjectOfType<AIUI>().ShowText(aiInstruction); }
    }

    public void SwitchOff()
    {
        sfx.PlayOneShot(clickSFX);
        mat.SetColor("_EmissionColor", Color.red);
        resultObj.GetComponent<Animator>().SetBool("isOpen", false);
    }
}
