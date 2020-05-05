using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//by Philipe Gouveia

public class AIInstructions : MonoBehaviour
{
    public Queue<string> instruction = new Queue<string>();
    public Queue<string> history = new Queue<string>();

    public bool beginTutorial;
    public bool beginHistory;

    AIInstructions()
    {
        instruction.Enqueue("... ... ...");
        instruction.Enqueue($"Hello, {GameManager.engineerName}...");
        instruction.Enqueue("You can SWITCH BETWEEN CAMERAS to explore the room using = keyboard [1] and [2] / Joystick [L] and [R] shoulder>>          ");
        instruction.Enqueue("to INTERACT with switches you must use -> keyboard [E]  / Joystick [Y] >>        ");
        instruction.Enqueue("... ... ...");

        history.Enqueue("My system shows that we are at the year 287 D.M.; what means we were switched off for about 13 years... ... ...");
        history.Enqueue($"and ... ... ... I do not have any data entry within those years' gap. It is very strange... My last data is when you finished the sphere project and had her working as you desired.");
        history.Enqueue("Another strange thing is that my sensor can only detect a weak readio frequency from a nearby land station... and nothing else.");
    }

    private void Awake()
    {
        beginTutorial = false;
        beginHistory = false;
    }

    private void Update()
    {
        if (beginTutorial && instruction.Count > 0)
        {
            FindObjectOfType<AIUI>().ShowText(instruction);
        }
        else if (beginHistory && history.Count > 0)
        {
            FindObjectOfType<AIUI>().ShowText(history);
        }
    }
}
