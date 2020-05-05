using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Sohyun Yi
public class FinalWallswitch : MonoBehaviour
{
    [SerializeField] GameObject[] trapLine;
    [SerializeField] Animator FloatAnim;
    [SerializeField] GameObject spawner;

    #region SFX
    private AudioSource sfx;
    [SerializeField] private AudioClip elevator;
    [SerializeField] private AudioClip click;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        sfx = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == GameManager.sphereTag)
        {    
            if (Input.GetButtonDown("Action4"))
            {
                spawner.SetActive(false);
                sfx.PlayOneShot(click);
                FloatAnim.SetTrigger("Unlock");
                for (int i= 0; i<trapLine.Length; i++)
                {
                    Destroy(trapLine[i]);
                    sfx.PlayOneShot(elevator);
                }
            }
        }
    }
}
