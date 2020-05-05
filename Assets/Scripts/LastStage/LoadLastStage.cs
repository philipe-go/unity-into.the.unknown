using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//by Philipe Gouveia

public class LoadLastStage : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == GameManager.engineerTag)
        {
            FindObjectOfType<GameManager>().LoadNewLevel("FinalBoss");
            FindObjectOfType<AIUI>().ShowText("After passing through the teleport portal you will not be allowed to go back");
        }
    }
}
