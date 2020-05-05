using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//by Philipe Gouveia

public class TetrisSwitchReset : MonoBehaviour
{
    [SerializeField] private Material mat;
    [SerializeField] private GameObject hologram;
    private int holoCounter;
    [SerializeField] private GameObject box;
    private Vector3 boxInitpos;
    private Quaternion boxInitrot;

    [SerializeField] private TetrisGrid[] place;
    [SerializeField] private GameObject bridge;
    [SerializeField] private GameObject newGrid;
    private Transform bridgeTransform;

    #region SFX 
    [Header("SFX")]
    private AudioSource sfx;
    [SerializeField] private AudioClip clickSFX;
    #endregion

    void Start()
    {
        holoCounter = 0;
        boxInitpos = box.transform.position;
        boxInitrot = box.transform.rotation;
        mat = GetComponent<Renderer>().material;
        sfx = GetComponent<AudioSource>();
    }

    private void OnTriggerStay(Collider other)
    {

        if (other.gameObject.tag == GameManager.engineerTag)
        {
            if (Input.GetButtonDown("Action4"))
            {
                mat.SetColor("_EmissionColor", Color.blue);
                hologram.GetComponent<TextMesh>().color = Color.blue;
                hologram.GetComponent<TextMesh>().text = "";
                sfx.PlayOneShot(clickSFX);
                StartCoroutine(ResetPuzzle(4f));
            }
        }
    }

    private void ResetTetris()
    {
        box.transform.position = boxInitpos;
        box.transform.rotation = boxInitrot;
        foreach (TetrisPCSpawner spawner in FindObjectsOfType<TetrisPCSpawner>())
        {
            spawner.ResetSwitch();
        }
       
        foreach (TetrisGrid grid in place)
        {
            grid.isAvailable = true;
        }
        foreach (TetrisPCHandler piece in FindObjectsOfType<TetrisPCHandler>())
        {
            piece.ResetPiece();
        }

        //bridgeTransform = bridge.transform;
        //Destroy(bridge);
        //Instantiate(newGrid, bridgeTransform);
    }

    IEnumerator ResetPuzzle(float time)
    {
        while (holoCounter < (int)time)
        {
            if (Input.GetButton("Action4"))
            {
                hologram.GetComponent<TextMesh>().text += "|||";
                yield return new WaitForSeconds(time * 0.2f);
                holoCounter++;
            }

            else
            {
                break;
            }
        }

        if (holoCounter >= time)
        {
            ResetTetris();
            hologram.GetComponent<TextMesh>().color = Color.green;
            hologram.GetComponent<TextMesh>().text = "SUCCESS";
            yield return new WaitForSeconds(1f);
        }

        else
        {
            hologram.GetComponent<TextMesh>().color = Color.red;
            hologram.GetComponent<TextMesh>().text = "FAIL";
            yield return new WaitForSeconds(1f);

        }

        hologram.GetComponent<TextMesh>().text = "RESET";
        hologram.GetComponent<TextMesh>().color = Color.red;
        mat.SetColor("_EmissionColor", Color.red);
        holoCounter = 0;
        StopCoroutine("ResetPuzzle");
    }
}
