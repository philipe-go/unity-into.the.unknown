using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//by Philipe Gouveia

    [RequireComponent(typeof(AudioSource))]
public class PortalToMaze : MonoBehaviour
{
    [SerializeField] private GameObject portal;
    [SerializeField] private Portal control;
    [Space]
    [SerializeField] private GameObject[] deactivateObjects;
    [SerializeField] private GameObject[] actiaveteObjects;
    [Space]
    [SerializeField] private Transform cameraRigTarget;
    [SerializeField] private Transform mazePlayer;
    [Space]
    [SerializeField] private TextMesh hologram;
    [SerializeField] private string newHologramText;

    public bool isSphere;
    public bool isEngineer;

    internal static bool skipPortal;

    public bool toMaze;
    public bool toBossFight;

    #region SFX 
    [Space]
    [Header("SFX")]
    private AudioSource sfx;
    [SerializeField] private AudioClip portalSFX;
    [SerializeField] private AudioClip mechClickSFX;
    #endregion

    private void Awake()
    {
        skipPortal = false; //to be changed after testing
    }

    private void Start()
    {
        if (portal) portal.SetActive(false);
        sfx = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (skipPortal) TeleportTo();
    }

    private void OnTriggerStay(Collider other)
    {
        if (isSphere)
        {
            if (other.gameObject.tag == GameManager.sphereTag)
            {
                if (Input.GetButtonDown("Action4"))
                {
                    if (control) control.isActive = true;
                    if (portal) portal.SetActive(true);

                    hologram.color = Color.green;
                    hologram.text = newHologramText;

                    Invoke("TeleportTo", 4f);
                }
            }
        }

        if (isEngineer)
        {
            if (other.gameObject.tag == GameManager.engineerTag)
            {
                if (Input.GetButtonDown("Action4"))
                {
                    if (control) control.isActive = true;
                    if (portal) portal.SetActive(true);

                    hologram.color = Color.green;
                    hologram.text = newHologramText;

                    Invoke("TeleportTo", 3f);
                }
            }
        }
    }

    internal void TeleportTo()
    {
        foreach(GameObject obj in deactivateObjects)
        {
            obj.SetActive(false);
        }
        foreach(GameObject obj in actiaveteObjects)
        {   
            obj.SetActive(true);
        }

        FindObjectOfType<GameManager>().carOn = false;
        FindObjectOfType<GameManager>().engineerOn = false;
        FindObjectOfType<GameManager>().sphereOn = false;

        //CameraRigHandler.stageIndex = 0;
        //FindObjectOfType<CameraRigHandler>().stage_PlaceHolders[0] = mazePlayer;
        //FindObjectOfType<CameraRigHandler>().stage_PlaceHolders[1] = cameraRigTarget;
        //FindObjectOfType<CameraRigHandler>().camScheme = 1;

        FindObjectOfType<CameraRigHandler>().hasEngineer = false;
        FindObjectOfType<CameraRigHandler>().hasSphere = false;

        if (toMaze)
        {
            CameraRigHandler.stageIndex = 0;
            FindObjectOfType<CameraRigHandler>().stage_PlaceHolders[0] = mazePlayer;
            FindObjectOfType<CameraRigHandler>().stage_PlaceHolders[1] = cameraRigTarget;
            FindObjectOfType<CameraRigHandler>().camScheme = 1;

            FindObjectOfType<LastStageManager>().isMaze = true;
            FindObjectOfType<LastStageManager>().isStage = false;
            FindObjectOfType<LastStageManager>().isTetris = false;
        }

        if (toBossFight)
        {
            sfx.PlayOneShot(mechClickSFX);
            CameraRigHandler camRig = CameraRigHandler.instance;
            for (int i = 0; i < camRig.camPlaceHolder1.Length; i++)
            {
                camRig.camPlaceHolder1[i] = mazePlayer;
            }
            FindObjectOfType<CameraRigHandler>().stage_PlaceHolders[0] = cameraRigTarget;
            FindObjectOfType<CameraRigHandler>().stage_PlaceHolders[1] = cameraRigTarget;
            camRig.index ++;

        }
    }
}
