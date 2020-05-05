using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

//by Philipe Gouveia & Sidakpreet Singh

public class MenuButtonHandler : MonoBehaviour, IPointerEnterHandler, IDeselectHandler, IPointerDownHandler
{
    private AudioSource asource;

    [Header("Audio Clips")]
    [SerializeField] private AudioClip hoverClip;
    [SerializeField] private AudioClip clickClip;

    void Start() //by Philipe Gouveia
    {
        asource = GetComponent<AudioSource>(); 
    }

    public void OnPointerEnter(PointerEventData eventData) //by Sidakpreet Singh
    {
        asource.PlayOneShot(hoverClip, 0.5f);
        GetComponent<Selectable>().Select();
    }

    public void OnPointerDown(PointerEventData eventData) //by Sidakpreet Singh
    {
        if (GetComponent<Button>() != null)
        {
            asource.PlayOneShot(clickClip);
            //GetComponent<Button>().onClick.Invoke();
            //Input.ResetInputAxes();
        }
    }

    public void OnDeselect(BaseEventData eventData) //by Sidakpreet Singh
    {
        //GetComponent<Selectable>().OnPointerExit(null);
        //Input.ResetInputAxes();
    }
}
