using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//by Philipe Gouveia

public class TetrisPCHandler : MonoBehaviour
{
    public bool isActive;
    internal bool inPlace;
    private bool forbiddenMove;

    [SerializeField] private TetrisPCSpawner spawnerList;
    [SerializeField] private Transform rotateCenter;
    private Vector3 initialStatepos;
    private Quaternion initialStaterot;

    [SerializeField] private TetrisPCRayCaster[] casters;

    #region SFX 
    [Header("SFX")]
    private AudioSource sfx;
    [SerializeField] private AudioClip moveSFX;
    [SerializeField] private AudioClip placeSFX;
    #endregion

    private void Awake()
    {
        spawnerList.pieces.Add(this.gameObject);
        isActive = false;
        inPlace = false;
        initialStatepos = transform.position;
        initialStaterot = transform.rotation;
    }

    private void Start()
    {
        sfx = GetComponent<AudioSource>();
        gameObject.SetActive(false);
    }

    private void Update()
    {
        if (isActive)
        {
            Inputs();
            Raycasters();
        }

        else if (!isActive)
        {
            if (!inPlace)
            {
                if (Input.GetButtonDown("Action4"))
                {
                    isActive = true;
                    transform.position += new Vector3(0, .7f, 0);
                    foreach (TetrisPCRayCaster cast in casters)
                    {
                        cast.AvailableGrid(true);
                    }
                }
            }

            if (Input.GetButtonDown("Action3"))
            {
                inPlace = true;
                FindObjectOfType<EngineerHandler>().engineerMove = true;

                CameraRigHandler.stageIndex = 0;
                FindObjectOfType<CameraRigHandler>().camScheme = 1;
            }
        }

        
    }

    public void ResetPiece()
    {
        transform.position = initialStatepos;
        transform.rotation = initialStaterot;

        isActive = false;
        inPlace = false;

        foreach (TetrisPCRayCaster cast in casters)
        {
            cast.ResetCaster();
        }

        gameObject.SetActive(false);
    }

    private void Raycasters()
    {
        foreach (TetrisPCRayCaster cast in casters)
        {
            cast.Raycasters();
        }
    }

    private void Inputs()
    {
        if (Input.GetButtonDown("Vertical"))
        {
            if (Input.GetAxis("Vertical") > 0) //Move UP
            {
                foreach (TetrisPCRayCaster cast in casters)
                {
                    if ((cast.hitTag == "T") || (cast.hitTag == "TR") || (cast.hitTag == "TL"))
                    {
                        forbiddenMove = true;
                    }
                }
                if (!forbiddenMove)
                {
                    this.transform.position += new Vector3(-3, 0, 0);
                    sfx.PlayOneShot(moveSFX);
                }
                else
                    forbiddenMove = false;
            }
            else if (Input.GetAxis("Vertical") < 0) //Move Down
            {
                foreach (TetrisPCRayCaster cast in casters)
                {
                    if ((cast.hitTag == "B") || (cast.hitTag == "BR") || (cast.hitTag == "BL"))
                    {
                        forbiddenMove = true;
                    }
                }
                if (!forbiddenMove)
                { 
                    this.transform.position += new Vector3(3, 0, 0);
                    sfx.PlayOneShot(moveSFX);
                }
                else
                    forbiddenMove = false;
            }

        }
        if (Input.GetButtonDown("Horizontal"))
        {
            if (Input.GetAxis("Horizontal") > 0) //Move Right
            {
                foreach (TetrisPCRayCaster cast in casters)
                {
                    if ((cast.hitTag == "R") || (cast.hitTag == "TR") || (cast.hitTag == "BR"))
                    {
                        forbiddenMove = true;
                    }
                }
                if (!forbiddenMove)
                {
                    this.transform.position += new Vector3(0, 0, 3);
                    sfx.PlayOneShot(moveSFX);
                }
                else
                    forbiddenMove = false;
            }
            else if (Input.GetAxis("Horizontal") < 0) //Move Left
            {
                foreach (TetrisPCRayCaster cast in casters)
                {
                    if ((cast.hitTag == "L") || (cast.hitTag == "TL") || (cast.hitTag == "BL"))
                    {
                        forbiddenMove = true;
                    }
                }
                if (!forbiddenMove)
                {
                    this.transform.position += new Vector3(0, 0, -3);
                    sfx.PlayOneShot(moveSFX);
                }
                else
                    forbiddenMove = false;
            }
        }

        if (Input.GetButtonDown("Action4"))
        {
            foreach (TetrisPCRayCaster cast in casters)
            {
                if (!cast.isAvailable)
                {
                    forbiddenMove = true;
                }
            }

            if (!forbiddenMove)
            {
                foreach (TetrisPCRayCaster cast in casters)
                {
                    cast.AvailableGrid(false);
                }

                isActive = false;
                transform.position += new Vector3(0, -.7f, 0);
                sfx.PlayOneShot(placeSFX);
            }
            else
                forbiddenMove = false;
        }

        if (Input.GetButtonDown("Action1"))
        {
            if (casters[0].hitTag != "C")
            {
                forbiddenMove = true;
                FindObjectOfType<AIUI>().ShowText("# IMPORTANT #\nTo flip the pieces, their central piece needs to be on the darker area of the grid.                                          ");
            }

            if (!forbiddenMove)
            {
                transform.RotateAround(rotateCenter.position, new Vector3(0, 1, 0), 90);
                sfx.PlayOneShot(moveSFX);

                if (transform.rotation.y >= 360)
                {
                    transform.rotation = Quaternion.Euler(0, 0, 0);
                }
            }
            else forbiddenMove = false;
        }

        if (Input.GetButtonDown("Action3"))
        {
            isActive = false;
            inPlace = false;
            gameObject.SetActive(false);
            FindObjectOfType<EngineerHandler>().engineerMove = true;
            spawnerList.ReturnPc();
        }
    }
}
