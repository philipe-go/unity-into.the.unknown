using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//by Philipe Gouveia

public class Portal : MonoBehaviour
{
    #region Attributes
    [SerializeField] private GameObject portal;
    [SerializeField] private GameObject explosion;
    [SerializeField] private GameObject playerToDestroy;
    public int timeInterval;
    public bool isActive;
    #endregion

    #region SFX 
    [Header("SFX")]
    private AudioSource sfx;
    [SerializeField] private AudioClip activatedSFX;
    #endregion

    private void Start()
    {
        sfx = GetComponent<AudioSource>();
    }

    private void OnTriggerStay(Collider other)
    {
        if (isActive)
        {
            if (other.gameObject.tag == playerToDestroy.tag)
            {
                sfx.PlayOneShot(activatedSFX);
                Invoke("DestroyPlayer", timeInterval);
                Invoke("Teleport", 2f);
            }
        }
    }

    void Teleport()
    {
        portal.SetActive(false);
        explosion.SetActive(true);
        if (playerToDestroy.tag == GameManager.sphereTag) FindObjectOfType<GameManager>().sphereOn = false;
        else if (playerToDestroy.tag == GameManager.engineerTag) FindObjectOfType<GameManager>().engineerOn = false;
        Destroy(this.gameObject);
    }

    private void DestroyPlayer()
    {
        playerToDestroy.SetActive(false);
    }
}
