using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//by Sohyun Yi & Philipe Gouveia
public class ColourButton : MonoBehaviour
{
    public int index;
    private AudioSource sfx;
    [SerializeField] private AudioClip click;
    private bool doOnce = true;

    private void Start()
    {
        sfx = GetComponent<AudioSource>();
    }

    //void OnCollisionEnter(Collision collision)
    //{
    //    if (collision.gameObject.tag == "BlueButton")
    //    {
    //        if (collision.gameObject.tag == "PurpleButton")
    //        {
    //            if (collision.gameObject.tag == "YellowButton")
    //            {
    //                if (collision.gameObject.tag == "RedButton")
    //                {
    //                    Destroy(Door3);
    //                }
    //            }
    //        }
    //    }
    //}

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == GameManager.sphereTag || other.gameObject.tag == GameManager.engineerTag)
        {
            if (Input.GetButtonDown("Action4"))
            {
                if (doOnce)
                {
                    doOnce = false;
                    sfx.PlayOneShot(click);
                    FindObjectOfType<ColourResult>().checker[FindObjectOfType<ColourResult>().index] = index;
                    FindObjectOfType<ColourResult>().index++;
                }
                else
                {
                    FindObjectOfType<AIUI>().ShowText("You can only select once each color");
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == GameManager.sphereTag || other.gameObject.tag == GameManager.engineerTag)
        {
            doOnce = true;
        }
    }
}
