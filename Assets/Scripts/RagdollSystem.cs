using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//by Philipe Gouveia

public class RagdollSystem : MonoBehaviour
{
    private Component[] hingeJoints;
    [SerializeField] private BoxCollider core;
    [SerializeField] private SphereCollider fightZone;

    private void Start()
    {
        hingeJoints = GetComponentsInChildren<Collider>();

        foreach (Collider col in hingeJoints)
        {
            if (col != core)
            {
                col.enabled = false;
            }
            if (col == fightZone)
            {
                col.enabled = true;
            }
            if (col is SphereCollider)
            {
                col.enabled = true;
            }
        }
    }

    internal void Die(float power, Vector3 direction)
    {
        GetComponent<Animator>().enabled = false;

        foreach (Collider col in this.hingeJoints)
        {
            col.enabled = true;
            col.GetComponent<Rigidbody>().isKinematic = false;
            col.GetComponent<Rigidbody>().AddForce(power * -direction);
        }
    }
}
