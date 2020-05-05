using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//by Philipe Gouveia 

public class SwitchSphere : MonoBehaviour
{
    [SerializeField] private Material mat;
    [SerializeField] ItemHighlight highlightSphere;
    [SerializeField] ItemHighlight highlightSwitch;
    [SerializeField] GameObject nextHighlight;

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
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == GameManager.engineerTag)
        {
            if (Input.GetButton("Action4"))
            {
                OnClick();
            }
        }
    }

    internal void OnClick()
    {
        if (highlightSphere) highlightSphere.blink = false;
        if (highlightSwitch) highlightSwitch.blink = false;
        if (nextHighlight) nextHighlight.SetActive(true);
        sfx.PlayOneShot(clickSFX);
        CameraRigHandler.isTopView = true;
        FindObjectOfType<CameraRigHandler>().IndexChanger(+4);
        mat.SetColor("_EmissionColor", Color.green);
        FindObjectOfType<GameManager>().sphereOn = true;
        FindObjectOfType<AIUI>().ShowText("<< _to_player: You can control the engineer or the sphere by swapping between them with the keyboard key [TAB] / joystick [left stick click] ...\n The sphere can roll or walk. To switch its mode use keyboard key [Q].>>");
    }
}
