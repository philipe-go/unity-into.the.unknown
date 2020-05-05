using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//by Philipe Gouveia

public class ParticleControl : MonoBehaviour
{
    ParticleSystem ps;

    private void Start()
    {
        ps = GetComponent<ParticleSystem>();
    }


    void LateUpdate()
    {
        if (!ps.isPlaying) Invoke("EraseParticle", 0.1f);
    }

    void EraseParticle()
    {
        Destroy(gameObject);
    }


}
