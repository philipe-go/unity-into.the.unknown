using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

//Sohyun Yi
[RequireComponent(typeof(Slider))]
public class Quality : MonoBehaviour
{
    [SerializeField] private Text txtGFX; //Textfield to display the quality setting.
    private string[] GFXNames; //A list of all the preset's names
    private Slider slide;

    // Start is called before the first frame update
    void Start()
    {
        slide = GetComponent<Slider>(); 
        GFXNames = QualitySettings.names; 
        float v = QualitySettings.GetQualityLevel();
        slide.value = v; 
        txtGFX.text = GFXNames[(int)v]; 
    }

    public void SetGraphics(float val)
    {
        slide.value = val;
        QualitySettings.SetQualityLevel((int)val, true);
        txtGFX.text = GFXNames[(int)val]; 
    }
    
    // Update is called once per frame
    void Update()
    {

    }
}


