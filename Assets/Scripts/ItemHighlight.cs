using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//by Philipe Gouveia

public class ItemHighlight : MonoBehaviour
{
    private Light highlight;
    internal bool blink;

    void Awake()
    {
        highlight = GetComponent<Light>();
        blink = true;
    }

    private void Start()
    {
        StartCoroutine(Glow());
    }

    private void Update()
    {
        if (!blink)
        {
            StopCoroutine(Glow());
            Destroy(gameObject);
        }
    }

    IEnumerator Glow()
    {
        while (blink)
        {
            while (highlight.intensity > 0.1f)
            {
                highlight.intensity -= 0.1f;
                yield return new WaitForSeconds(0.05f);
            }
            while (highlight.intensity < 1f)
            {
                highlight.intensity += 0.1f;
                yield return new WaitForSeconds(0.05f);
            }
        }
    }

}
