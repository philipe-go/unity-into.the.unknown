using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//by Sidakpreet Singh

public class Select_pannels : MonoBehaviour
{

    [SerializeField] private GameObject[] panels;
    [SerializeField] private Selectable[] defaultButtons;

    public void PannelToggle()
    {
        PannelToggle(0);

    }


    public void PannelToggle(int position)
    {


        Input.ResetInputAxes();

        for (int i = 0; i < panels.Length; i++)
        {

            panels[i].SetActive(position == i);
            if (position == i)
            {

                defaultButtons[i].Select();

            }
        }

    }
}
