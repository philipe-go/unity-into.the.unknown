using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//by Philipe Gouveia

public class EngineerRaycaster : MonoBehaviour
{
    private RaycastHit hit;
    private EngineerHandler engineer;
    private bool doOnce;
    
    public bool groundCaster = false;
    public bool frontCaster = false;

    private void Start()
    {
        engineer = FindObjectOfType<EngineerHandler>();
    }

    private void Update()
    {
        if (groundCaster)
        {
            if (Physics.Raycast(transform.position, -transform.TransformDirection(Vector3.up), out hit, 0.1f))
            {
                Debug.DrawRay(transform.position, -transform.TransformDirection(Vector3.up) * hit.distance, Color.yellow);
                engineer.canMove = true;
                doOnce = false;
            }
            else
            {
                Debug.DrawRay(transform.position, -transform.TransformDirection(Vector3.up) * 1f, Color.white);
                if (Physics.Raycast(transform.position, -transform.TransformDirection(Vector3.up), out hit))
                {
                    if (hit.transform.tag == "gap")
                    {
                        engineer.canMove = false;
                        if (!doOnce)
                        {
                            FindObjectOfType<AIUI>().ShowText($"You can not jump with this character.");
                            doOnce = true;
                        
                        }
                    }
                }
            }
        }
    }

}
