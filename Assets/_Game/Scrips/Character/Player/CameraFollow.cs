using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] Transform player;
    Vector3 intialOffset = new Vector3(0, 15, -30);
    Vector3 offset = new Vector3(0, 15, -30);

    public Vector3 Offset { get => offset; set => offset = value; }


    void Update()
    {
        if (player != null)
            transform.position = Vector3.Lerp(transform.position, player.position + offset, 0.5f);
    }
    public void SetTargetFollow(Transform target)
    {
        player = target;
    }
    public void ResetOffset()
    {
        offset = intialOffset;
    }


}