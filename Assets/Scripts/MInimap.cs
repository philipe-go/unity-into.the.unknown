using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//by Sidakpreet Singh

public class MInimap : MonoBehaviour
{
    [SerializeField] private Transform FollowCar;
    [SerializeField] private GameObject carIcon;
    Vector3 offset;
    private float height = 650;

    //phil: code added to handle next stage highlight on the minimap
    [SerializeField] private GameObject[] pointOfInterestIcon;
    [SerializeField] private Transform[] poiStages;

    void Start()
    {
        transform.position = FollowCar.position;
        offset = new Vector3(1, height, 1);
        //transform.SetParent(null);
    }

    private void Update()
    {
        carIcon.transform.position = FollowCar.position + new Vector3(1, 600, 1);
        carIcon.transform.rotation = FollowCar.rotation.normalized * Quaternion.Euler(90, 0, FollowCar.rotation.y); //phil
        carIcon.transform.parent = FollowCar.transform;

        for (int i = 0; i < poiStages.Length; i++)
        {
            pointOfInterestIcon[i].transform.position = poiStages[i].position + new Vector3(1, 600, 1);
            //pointOfInterestIcon[i].transform.parent = poiStages[i].transform;
        }
    }

    void LateUpdate()
    {
        if (FollowCar != null)
        {
            transform.position = FollowCar.position + offset;
            //Quaternion.LookRotation(-FollowCar.up, FollowCar.forward);
        }
    }
}
