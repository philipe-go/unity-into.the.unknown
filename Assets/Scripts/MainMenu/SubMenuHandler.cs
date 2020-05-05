using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//by Philipe Gouveia

public class SubMenuHandler : MonoBehaviour
{
    [SerializeField] private GameObject[] items;

    public void AppearItems()
    {
        if (items.Length > 0)
        {
            foreach (GameObject obj in items)
            {
                obj.SetActive(true);
            }
        }
    }

    public void DesappearItems()
    {
        if (items.Length > 0)
        {
            foreach (GameObject obj in items)
            {
                obj.SetActive(false);
            }
        }
    }
}
