using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//by Philipe Gouveia

public class PortalAfterTetris : MonoBehaviour
{
    #region SFX 
    [Space]
    [Header("SFX")]
    private AudioSource sfx;
    [SerializeField] private AudioClip portalSFX;
    #endregion

    private void Start()
    {
        sfx = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
       if (other.gameObject.tag == GameManager.engineerTag)
        {
            sfx.PlayOneShot(portalSFX);
            Invoke("NewLevel", 1.5f);
        }
    }

    private void NewLevel()
    {
        FindObjectOfType<GameManager>().ActivateNewScene();
    }
}
