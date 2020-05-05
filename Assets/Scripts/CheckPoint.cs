using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//by Philipe Gouveia and Sidakpreet Singh

public class CheckPoint : MonoBehaviour
{
    [SerializeField] private Transform engineerPlaceHolder;
    [SerializeField] private Transform spherePlaceHolder;
    [SerializeField] private Transform carPlaceHolder;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == GameManager.engineerTag)
        {

        }
        if (other.gameObject.tag == GameManager.sphereTag)
        {

        }
        if (other.gameObject.tag == GameManager.carTag)
        {

        }

        FindObjectOfType<MenuUI>().Save();
    }

}
