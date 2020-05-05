using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Sohyun Yi
public class MenuSelect : MonoBehaviour
{
    [SerializeField] private GameObject[] panels; 
    [SerializeField] private Selectable[] defaultButtons; 

    public void PanelToggle(int position)
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

    public void SavePrefs()
    {
        PlayerPrefs.Save(); 
    }
    
    void Start()
    {
        Invoke("PanelToggle", 0.01f);
    }

    void PanelToggle()
    {
        PanelToggle(0); //Activate the main panel
    }

    void Update()
    {

    }
}
