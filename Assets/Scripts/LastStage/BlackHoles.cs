using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//by Philipe Gouveia

public class BlackHoles : MonoBehaviour
{
    [SerializeField] private ParticleSystem[] explosion;

    #region SFX 
    [Header("SFX")]
    private AudioSource sfx;
    [SerializeField] private AudioClip spawnSFX;
    #endregion

    private void Start()
    {
        sfx = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == GameManager.sphereTag)
        {
            FindObjectOfType<MazePlayerHandler>().GetComponent<Rigidbody>().AddForce(-transform.forward);
            foreach(ParticleSystem particle in explosion)
            {
                sfx.PlayOneShot(spawnSFX);
                particle.Play();
            }
        }
    }
}
