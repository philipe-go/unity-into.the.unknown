using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//by Philipe Gouveia

public class LastStageManager : MonoBehaviour
{
    public bool isMaze;
    public bool isTetris;
    public bool isStage;

    void Start()
    {
        isStage = true;
        isMaze = false;
        isTetris = false;
    }

    void Update()
    {
        //if (isStage)
        //{

        //}
        if (isMaze)
        {
            if (Input.GetButtonDown("CharSwitcher"))
            {
                FindObjectOfType<CameraRigHandler>().camScheme = 2 / FindObjectOfType<CameraRigHandler>().camScheme;
            }
        }
        if (isTetris)
        {
            if (Input.GetButtonDown("CharSwitcher"))
            {
                FindObjectOfType<CameraRigHandler>().camScheme = 3 / FindObjectOfType<CameraRigHandler>().camScheme;
            }
        }

        //if (Input.GetKeyDown(KeyCode.Backspace)) //tester
        //{
        //    FindObjectOfType<MazeHandler>().TeleportToEngineer();
        //}
    }


}
