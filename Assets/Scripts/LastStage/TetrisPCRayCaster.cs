using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//by Philipe Gouveia

public class TetrisPCRayCaster : MonoBehaviour
{
    internal string hitTag;
    internal bool isAvailable;
    private RaycastHit hit;

    internal void Raycasters()
    {
        Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.right) * 1f, Color.white);

        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.right) * 2f, out hit, 2.0f))
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.right) * 2f, Color.yellow);
            hitTag = hit.transform.GetComponent<TetrisGrid>().name;
            isAvailable = hit.transform.GetComponent<TetrisGrid>().isAvailable;
        }
    }

    internal void AvailableGrid(bool value)
    {
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.right) * 2f, out hit, 2f))
        {
            hit.transform.GetComponent<TetrisGrid>().isAvailable = value;
        }
    }

    public void ResetCaster()
    {
        hitTag = "";
        isAvailable = true;
    }
}

