using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//by Philipe Gouveia

[RequireComponent(typeof(BoxCollider))]
public class HistoryTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == GameManager.engineerTag || other.gameObject.tag == GameManager.sphereTag)
        {
            GameObject.FindObjectOfType<AIInstructions>().beginHistory = true;
        }
    }
}
