using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//by Sohyun Yi
public class ThirdStagePuzzle : MonoBehaviour
{
    [SerializeField] private GameObject text;
    private bool activated = false;
    public bool hasSwitchOff = false;
    [SerializeField] private AudioClip click;
    private AudioSource sfx;

    // Start is called before the first frame update
    void Start()
    {
        sfx = GetComponent<AudioSource>();
        text.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.tag == GameManager.sphereTag)
        {
            if (Input.GetButtonDown("Action4"))
            {
                sfx.PlayOneShot(click);
                text.SetActive(true);
            }
        }      
        
    }
}
