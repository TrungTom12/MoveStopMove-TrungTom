using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : Singleton<CameraFollow>
{
    [SerializeField] Transform player;
    [SerializeField] Vector3 intialOffset = new Vector3(0, 15, -30);
    [SerializeField] Vector3 offset = new Vector3(0, 15, -22);

    [SerializeField] Vector3 zoomInOffset = new Vector3(0, 5, -15);
    public Vector3 Offset { get => offset; set => offset = value; }
    private void Start()
    {
        GetInstance();
    }
    // Update is called once per frame
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
    public void ZoomOut()
    {
        offset = intialOffset;
    }
    public void ZoomIn()
    {
        offset = zoomInOffset;
    }

}