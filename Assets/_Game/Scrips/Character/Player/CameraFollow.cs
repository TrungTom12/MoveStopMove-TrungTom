using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] Transform player;
    [SerializeField] Vector3 offset = new Vector3(0, 15, -25);

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, player.position + offset, 0.5f);
    }
    public void SetTargetFollow(Transform target)
    {
        player = target;
    }
}