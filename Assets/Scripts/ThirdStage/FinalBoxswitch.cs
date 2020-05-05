using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Sohyun Yi
public class FinalBoxswitch : MonoBehaviour
{
    [SerializeField] Animator FinalDoorAnim;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == GameManager.engineerTag)
        {
            FinalDoorAnim.SetTrigger("Open");
            FindObjectOfType<AIUI>().ShowText("You completed this stage. You have gained one key to access the last resource space ship.");
            GameManager.hasKey1 = true;
            FindObjectOfType<GameManager>().LoadNewLevel("Planet");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == GameManager.engineerTag)
        {
            FindObjectOfType<GameManager>().ActivateNewScene();
        }
    }
}
