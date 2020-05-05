using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//by Sohyun Yi & Philipe Gouveia
public class PlanetHandler : MonoBehaviour
{
    #region Spawn Attributes
    [SerializeField] private GameObject carPrefab;
    [SerializeField] private Transform initalSpawner;
    private static bool firsStage;
    internal static Transform spawnerPos;
    #endregion

    #region Singleton
    public static PlanetHandler instance = null;
    private void Awake()
    {
        firsStage = true;
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

    }
    #endregion


    #region Spiders Handler
    [SerializeField] private GameObject spiders1;
    internal static bool spider1dead = false;
    [SerializeField] private GameObject spiders2;
    internal static bool spider2dead = false;
    [SerializeField] private GameObject spiders3;
    internal static bool spider3dead = false;

    #endregion

    private void Start()
    {
        if (firsStage)
        {
            spawnerPos = initalSpawner;
            firsStage = false;
        }
        else carPrefab.transform.position = spawnerPos.position;
    }

    private void Update()
    {
        if (spider1dead)
        {

        }
        if (spider2dead)
        {

        }
        if (spider3dead)
        {

        }
    }
}
