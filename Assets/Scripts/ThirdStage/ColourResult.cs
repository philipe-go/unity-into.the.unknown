using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//by Sohyun Yi & Philipe Gouveia
public class ColourResult : MonoBehaviour
{
    internal int[] checker = { 0, 0, 0, 0};
    private bool accomplished = false;
    public int index = 0;
    private bool doOnce = true;

    [SerializeField] private GameObject door3;

    private AudioSource sfx;
    [SerializeField] private AudioClip end;
    [SerializeField] private AudioClip wrong;

    private void Start()
    {
        index = 0;
        sfx = GetComponent<AudioSource>();
    }

    private void Update()
    {
        ColorChecker();

        if (accomplished)
        {
            if (doOnce)
            {
                doOnce = false;
                door3.GetComponent<Animator>().SetBool("isOpen", true);
                sfx.PlayOneShot(end);
            }
        }
    }

    void ColorChecker()
    {
        if (checker[0] == 1 && checker[1] == 2 && checker[2] == 3 && checker[3] == 4)
        {
            accomplished = true;
        }
        else
        {
            if (index > 3)
            {
                sfx.PlayOneShot(wrong);
                index = 0;
            }
            accomplished = false;
        }
    }
}
