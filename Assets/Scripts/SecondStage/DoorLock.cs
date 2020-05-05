using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Sidakpreet Singh

public class DoorLock : MonoBehaviour
{
    private string currPassword = "1234";
    private string input="";
    private bool onTrigger=false;


    private Button button1;
    private Button button2;
    private Button button3;
    private Button button4;
    private Button button5;
    private Button button6;
    private Button button7;
    private Button button8;
    private Button button9;
    private Button buttonzero;
    private Button clearbutton;
    private Button gobutton;
   




    // Start is called before the first frame update
    void Start()
    {
        button1.GetComponent<Button>().onClick.AddListener(PressOne);
        button2.GetComponent<Button>().onClick.AddListener(PressTwo);
        button3.GetComponent<Button>().onClick.AddListener(PressThree);
        button4.GetComponent<Button>().onClick.AddListener(PressFour);
        button5.GetComponent<Button>().onClick.AddListener(PressFive);
        button6.GetComponent<Button>().onClick.AddListener(PressSix);
        button7.GetComponent<Button>().onClick.AddListener(PressSeven);
        button8.GetComponent<Button>().onClick.AddListener(PressEight);
        button9.GetComponent<Button>().onClick.AddListener(PressNine);
        buttonzero.GetComponent<Button>().onClick.AddListener(PressZero);
        clearbutton.GetComponent<Button>().onClick.AddListener(PressClear);
        gobutton.GetComponent<Button>().onClick.AddListener(PressGo);



    }


    void OnTriggerEnter(Collider other)
    {
        if(other.tag=="Player")
        {
            onTrigger = true;
        }
       
    }

    void OnTriggerExit(Collider collider)
    {
        if (collider.tag == "Player")
        {
            onTrigger = false;
        }
           

    }



    void PressOne()
    {

        input = input + "1";

    }
    void PressTwo()
    {
        input = input + "2";
    }
    void PressThree()
    {
        input = input + "3";
    }
    void PressFour()
    {
        input = input + "4";

    }
    void PressFive()
    {
        input = input + "5";
    }
    void PressSix()
    {
        input = input + "6";
    }
    void PressSeven()
    {
        input = input + "7";

    }
    void PressEight()
    {
        input = input + "8";
    }
    void PressNine()
    {
        input = input + "9";
    }
    void PressZero()
    {
        input = input + "0";
    }
    void PressClear()
    {
        input = "";
    }
    void PressGo()
    {
        if (input == currPassword)         
        {
            Debug.Log("Correct Password ");
        }
        else
        {
            Debug.Log("Wrong Password");

        }
    }
}


