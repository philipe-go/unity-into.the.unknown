using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//by Sohyun Yi

public class TerrainLoader : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if ((other.gameObject.tag == GameManager.carTag) || (other.gameObject.tag == GameManager.engineerTag)
                || (other.gameObject.tag == GameManager.sphereTag))
        {
            LoadTerrain();
        }
    }

    internal void LoadTerrain()
    {
        SceneManager.LoadScene("Planet");
    }
}
