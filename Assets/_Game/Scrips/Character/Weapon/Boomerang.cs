using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boomerang : Bullet
{
    Vector3 firstPoint;
    [SerializeField] float speedBack;
    void Start()
    {
        rb = GetComponent<Rigidbody>(); 
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer > timeExist)
        {
            if (timer < timeExist + 0.1)
                ResetForce();
            transform.position = Vector3.Lerp(transform.position,firstPoint,Time.deltaTime + speedBack);
        }

        if (timer > timeExist * 2)
        {
            PoolingPro.GetInstance().ReturnToPool(tagWeapon.ToString(), gameObject);
        }

        transform.Rotate(0,rotateSpeed,0); 
    }

    public void SetFirstPoint(Vector3 point)
    {
        firstPoint = point;
    }

}
