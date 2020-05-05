using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//by Philipe Gouveia

public class PlayerSpawner : MonoBehaviour
{
    [SerializeField] private Transform respawnPlace;
    [SerializeField] private GameObject engineerPrefab;
    [SerializeField] private GameObject spherePrefab;
    [SerializeField] private GameObject boxPrefab;
    public bool begin;

    private void OnTriggerEnter(Collider other)
    {
        if (begin)
        {
            if (other.gameObject.tag == GameManager.engineerTag)
            {
                StartCoroutine(RespawnChar(other.gameObject));
                Instantiate(engineerPrefab, respawnPlace);

                FindObjectOfType<AIUI>().ShowText($"<< Try to ask the {GameManager.sphereName} to jump over this gap.>>");
                begin = false;
            }
        }

        if (!begin)
        {
            if (other.gameObject.tag == engineerPrefab.tag)
            {
                StartCoroutine(RespawnChar(other.gameObject));
                Instantiate(engineerPrefab, respawnPlace);
            }

            else if (other.gameObject.tag == spherePrefab.tag)
            {
                StartCoroutine(RespawnChar(other.gameObject));
                Instantiate(spherePrefab, respawnPlace);
            }
        }

        if (other.gameObject.tag == "boxSwitch")
        {
            other.gameObject.transform.position = respawnPlace.position;
        }
    }

    IEnumerator RespawnChar(GameObject player)
    {
        //player.SetActive(false);
        player.transform.position = respawnPlace.position;
        player.transform.rotation = Quaternion.identity;
        yield return new WaitForSeconds(0.1f);
        player.SetActive(true);
        yield return null;
        StopCoroutine(RespawnChar(player));
    }
}
