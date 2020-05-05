using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//by Philipe Gouveia

public class DoorTrigger : MonoBehaviour
{
    private Animator anim;
    public bool test;
    private bool hasKey1 = false;
    //private bool hasKey2 = false;
    public string aiText;
    private bool doOnce;

    #region SFX 
    [Space]
    [Header("SFX")]
    private AudioSource sfx;
    [SerializeField] private AudioClip openSFX;
    [SerializeField] private AudioClip closeSFX;
    #endregion

    private void Start()
    {
        if (test) hasKey1 = true;
        //if (test) hasKey2 = true;
        else
        {
            hasKey1 = GameManager.hasKey1;
            //hasKey2 = GameManager.hasKey2;
        }
        doOnce = false;
        anim = GetComponent<Animator>();
        sfx = GetComponent<AudioSource>();
    }

    private void OnTriggerStay(Collider other)
    {
        if (hasKey1)
        {
            if (other.gameObject.tag == GameManager.engineerTag)
            { anim.SetBool("isOpen", true); }
            sfx.PlayOneShot(openSFX);
        }
        else
        {
            if (!doOnce)
            {
                FindObjectOfType<AIUI>().ShowText(aiText);
            }
            doOnce = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == GameManager.engineerTag)
        { 
            anim.SetBool("isOpen", false);
            sfx.PlayOneShot(closeSFX);
        }

        doOnce = false;
    }
}
