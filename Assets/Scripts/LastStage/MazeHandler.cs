using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//by Philipe Gouveia

public class MazeHandler : MonoBehaviour
{
    #region Singleton
    public static MazeHandler instance;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }
    #endregion

    //[SerializeField] Vector3[] rotations;
    //Vector3 initialRot = new Vector3(0,0,0);
    //private int index;
    //public float smoothness = 0.5f;
    ////public int countdown = 100;
    ////private int temp;
    //public float timer;0

    [SerializeField] private GameObject[] deactivateObjects;
    [SerializeField] private GameObject[] actiaveteObjects;

    private void Start()
    {
        //timer = 5f;
        //doRotation = true;
    }

    private void Update()
    {
        //if (doRotation)
        //{
        //    StartCoroutine(NewRotation(5f));
        //}
    }

    private void OnTriggerEnter(Collider other) //End portal of Maze
    {
        TeleportToEngineer();
    }


    //CODE commented to check if we have time implementing a random rotation. As it is below the rotation does suddenly not as animation
    //IEnumerator NewRotation(float time) 
    //{
    //    doRotation = false;
    //    index = Random.Range(0, rotations.Length - 1);
    //    transform.rotation = Quaternion.Slerp(Quaternion.Euler(initialRot), Quaternion.Euler(rotations[index]), smoothness);
    //    yield return new WaitForSeconds(time);
    //    doRotation = true;
    //    StopCoroutine("NewRotation");
    //}


    internal void TeleportToEngineer()
    {
        foreach (GameObject obj in deactivateObjects)
        {
            obj.SetActive(false);
        }
        foreach (GameObject obj in actiaveteObjects)
        {
            obj.SetActive(true);
        }

        FindObjectOfType<GameManager>().carOn = false;
        FindObjectOfType<GameManager>().engineerOn = true;
        FindObjectOfType<GameManager>().sphereOn = false;
        FindObjectOfType<EngineerHandler>().canMove = true;
        FindObjectOfType<EngineerHandler>().anim.SetFloat("WalkSpeed", EngineerHandler.speed);

        CameraRigHandler.stageIndex = 0;
        FindObjectOfType<CameraRigHandler>().stage_PlaceHolders[0] = Transform.FindObjectOfType<EngineerHandler>().transform;
        FindObjectOfType<CameraRigHandler>().stage_PlaceHolders[1] = Transform.FindObjectOfType<EngineerHandler>().transform;
        FindObjectOfType<CameraRigHandler>().camScheme = 1;
        FindObjectOfType<GameManager>().CharacterHandler();

        FindObjectOfType<CameraRigHandler>().hasEngineer = true;
        FindObjectOfType<CameraRigHandler>().hasSphere = false;

        FindObjectOfType<LastStageManager>().isMaze = false;
        FindObjectOfType<LastStageManager>().isStage = true;
        FindObjectOfType<LastStageManager>().isTetris = false;
    }
}

