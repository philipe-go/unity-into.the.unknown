using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//by Philipe Gouveia

public class FollowSphereMaze : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private Transform groundRot;
    public float heightOffset;

    private void Update()
    {
        transform.position = new Vector3(player.position.x, player.position.y - heightOffset, player.position.z);
        transform.rotation = Quaternion.Euler(new Vector3(90f + groundRot.rotation.x, 0, 0 + groundRot.rotation.z));
    }
}
