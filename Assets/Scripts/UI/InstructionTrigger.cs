using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//by Philipe Gouveia

public class InstructionTrigger : MonoBehaviour
{
    public string[] instruction;
    private Queue<string> speakeAI = new Queue<string>();
    private bool start;

    private void Start()
    {
        start = false;

        foreach (string inst in instruction)
        {
            speakeAI.Enqueue(inst);
        }
    }

    private void Update()
    {
        if (start && speakeAI.Count > 0)
        {
            FindObjectOfType<AIUI>().ShowText(speakeAI);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == GameManager.engineerTag || other.gameObject.tag == GameManager.sphereTag)
        {
            start = true;
        }
    }
}
