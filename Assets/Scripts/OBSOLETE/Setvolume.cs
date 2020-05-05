using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

//by Sidakpreet Singh

public class Setvolume : MonoBehaviour
{
    [SerializeField] private AudioMixer audioM;
    [SerializeField] private string nameParameter;

    void Start()
    {
        Slider slide = GetComponent<Slider>();
        float v = PlayerPrefs.GetFloat(nameParameter, 0);
        slide.value = v;
    }

    public void SetVolume(float vol)
    {

        Slider slide = GetComponent<Slider>();
        audioM.SetFloat(nameParameter, vol);
        slide.value = vol;
        PlayerPrefs.SetFloat(nameParameter, vol);
        PlayerPrefs.Save();
    }
}
