using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//by Sohyun Yi
public class KillSpider : MonoBehaviour
{
    [SerializeField] private Animator anim;
    [SerializeField] private GameObject web;
    [SerializeField] private GameObject platform;
    [SerializeField] private GameObject boxSwitch;

    public void Die()
    {
        anim.SetBool("Dead", true); //phil: isntead of trigger I changed to boolean

        FindObjectOfType<AIUI>().ShowText("The spider has died. spider and the spider web is removed.");
        //web.SetActive(false); //phil: is it not better to destroy the gameObject?
        Destroy(web);
        Invoke("Remove", 1.5f);

        platform.GetComponent<Animation>().Play("PlatformUp");
        boxSwitch.GetComponent<Rigidbody>().isKinematic = false;
    }

    public void OnCollisionEnter(Collision collision)
    {
        if ((collision.gameObject.tag == GameManager.engineerTag) || (collision.gameObject.tag == GameManager.sphereTag))
        {
            Die();
        }
    }

    public void Remove()
    {
        //gameObject.SetActive(false); //phil: is it not better to destroy the gameObject?
        Destroy(gameObject);
    }


    void Start()
    {
        anim = GetComponent<Animator>(); //phil: added this so the gameobject retrieves the animator automatically
    }
}
