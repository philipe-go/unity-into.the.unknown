using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//by Philipe Gouveia

public class SwitchBridge : MonoBehaviour
{
    [SerializeField] private ItemHighlight highlight;
    [SerializeField] private GameObject nextHighlight;
    [SerializeField] private GameObject floorHighlight;
    [SerializeField] private PuzzleCubes cube;

    [SerializeField] private GameObject resultObj;
    private Material mat;

    private bool doOnce;

    #region SFX 
    [Space]
    [Header("SFX")]
    private AudioSource sfx;
    [SerializeField] private AudioClip clickSFX;
    #endregion

    void Start()
    {
        sfx = GetComponent<AudioSource>();
        mat = GetComponent<Renderer>().material;
        doOnce = true;
        mat.SetColor("_EmissionColor", Color.red);
        resultObj.GetComponent<Animator>().SetBool("isOpen", false);
    }

    private void Update()
    {
        if (cube.isCarried && doOnce)
        {
            if (nextHighlight) nextHighlight.SetActive(false);
            if (floorHighlight) floorHighlight.SetActive(true);
            doOnce = false;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == GameManager.sphereTag)
        {
            if (Input.GetButtonDown("Action4"))
            {
                OnClick();
            }
        }
    }

    public void OnClick()
    {
        sfx.PlayOneShot(clickSFX);
        if (highlight) highlight.blink = false;
        if (nextHighlight) nextHighlight.SetActive(true);
        mat.SetColor("_EmissionColor", Color.green);
        resultObj.GetComponent<Animator>().SetBool("isOpen", true);
        FindObjectOfType<AIUI>().ShowText($"<< _to_player: You can lift heavy objects with {GameManager.engineerName} using -> keyboard [Q] / Joystick [X]. To drop them, press the same key again.>>");
    }
}
