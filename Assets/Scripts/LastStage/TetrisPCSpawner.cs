using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//by Philipe Gouveia

public class TetrisPCSpawner : MonoBehaviour
{
    [SerializeField] private Material mat;
    [SerializeField] private GameObject[] symbol;
    public string boxTag = "boxSwitch";
    internal List<GameObject> pieces = new List<GameObject>();
    internal int pieceCounter = 0;

    void Start()
    {
        pieceCounter = 0;
        mat = GetComponent<Renderer>().material;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (pieceCounter <= 2 && collision.gameObject.tag == boxTag)
        {
            CameraRigHandler.stageIndex = 1;
            FindObjectOfType<CameraRigHandler>().camScheme = 3;
            SpawnPc();
        }
        else if (pieceCounter > 2)
        {
            FindObjectOfType<AIUI>().ShowText("You can only use a maximum of 3 pieces of each type");

        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == boxTag)
        {
            OnClose();
        }
    }

    public void SpawnPc()
    {
        mat.SetColor("_EmissionColor", Color.green);
        pieces[pieceCounter].SetActive(true);
        pieces[pieceCounter].GetComponent<TetrisPCHandler>().isActive = true;
        FindObjectOfType<EngineerHandler>().engineerMove = false;
        symbol[pieceCounter].SetActive(false);
        pieceCounter++;
    }

    internal void ReturnPc()
    {
        pieceCounter--;
        symbol[pieceCounter].SetActive(true);
    }

    public void OnClose()
    {
        mat.SetColor("_EmissionColor", Color.red);
    }

    internal void ResetSwitch()
    {
        pieceCounter = 0;
        foreach(GameObject obj in symbol)
        {
            obj.SetActive(true);
        }
    }
}
