using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//by Philipe Gouveia

public class ManualMenuHandler : MonoBehaviour
{
    [SerializeField] private GameObject[] schemes;
    private int index = 1;

    public void Start()
    {
        schemes[index].SetActive(true);
    }

    public void SetScheme(int inc)
    {
        foreach(GameObject obj in schemes)
        {
                obj.SetActive(false);
        }

        index = index + inc;
        if(index > 2)
        {
            index = 0;
        }
        else if (index < 0)
        {
            index = 2;
        }

        schemes[index].SetActive(true);
    }

}
