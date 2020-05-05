using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//by Philipe Gouveia

public class BossStageHandler : MonoBehaviour
{
    [SerializeField] private GameObject[] portal;
    [SerializeField] private GameObject engineer;
    [SerializeField] private GameObject sphere;

    private void Awake()
    {
        engineer.isStatic = true;
        sphere.isStatic = true;
        engineer.transform.localScale = Vector3.zero;
        sphere.transform.localScale = Vector3.zero;
    }

    void Start()
    {
        Invoke("Begin", 3);
    }

    void Update()
    {
        
    }

    void Begin()
    {
        foreach(GameObject obj in portal)
        {
            Destroy(obj);
        }

        engineer.transform.localScale = Vector3.one;
        sphere.transform.localScale = Vector3.one;
        engineer.isStatic = false;
        sphere.isStatic = false;
    }
}
