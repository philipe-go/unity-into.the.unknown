using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//by Sohyun Yi
public class Password : MonoBehaviour
{
    [SerializeField] private GameObject Door1;
    //[SerializeField] private Animator animFloor;
    //[SerializeField] private GameObject Door2;
    [SerializeField] private GameObject[] codes;
    
    private bool canType;
    private bool y, a, x, b;
    private bool accessGranted;

    #region SFX
    [SerializeField] private AudioClip pass;
    private AudioSource sfx;
    #endregion

    [SerializeField] private GameObject[] testers;

    // Start is called before the first frame update
    void Start()
    {
        accessGranted = false;
        canType = false;
        sfx = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (canType)
        {
            CheckPassword();
        }

        if (accessGranted)
        {
            Destroy(Door1);
        }

        foreach(GameObject obj in codes)
        {
            if (obj.activeSelf) canType = true;
            else
            {
                canType = false;
                break;
            }
        }

        if(Input.GetKeyDown(KeyCode.Backspace))
        {
            foreach(GameObject obj in testers)
            {
                obj.SetActive(false);
            }
        }

    }

    //private void OnTriggerStay(Collider other)
    //{
    //    if (other.gameObject.tag == GameManager.engineerTag)
    //    {
    //        if (Input.GetButtonDown("Action3"))
    //        {
    //            //animFloor.SetBool("Unlock", false);
    //            //Door2.SetActive(false);
    //        }
    //    }
    //}

    //void OnCollisionExit(Collision collision)
    //{
    //    if(collision.gameObject.tag == "switchForBox")
    //    {
    //        animFloor.SetBool("Unlock", false);
    //        Destroy(Door2);
    //    }
    //}

    #region Password Checker
    void CheckPassword() //phil
    {
        if (Input.GetButtonDown("Action4") || Input.GetKeyDown(KeyCode.Y))
        {
            y = true;
        }
        if ((Input.GetButtonDown("Action1") || Input.GetKeyDown(KeyCode.A)) && y)
        {
            a = true;
        }
        if ((Input.GetButtonDown("Action3") || Input.GetKeyDown(KeyCode.X)) && y && a)
        {
            x = true;
        }
        if ((Input.GetButtonDown("Action2") || Input.GetKeyDown(KeyCode.B)) && y && a && x)
        {
            b = true;
        }

        if (y && a && x && b)
        {
            accessGranted = true;
            sfx.PlayOneShot(pass);
        }

        else accessGranted = false;
    }
    #endregion
}
