using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//by Philipe Gouveia
public class SpiderKiller : MonoBehaviour
{
    [SerializeField] private GameObject spider;

    private void OnTriggerExit(Collider other)
    {
        if ((other.gameObject.tag == GameManager.engineerTag) || (other.gameObject.tag == GameManager.sphereTag))
        {
            Invoke("Killer", 2f);
        }
    }

    private void Killer()
    {
        spider.GetComponent<KillSpider>().Die();
    }

}
