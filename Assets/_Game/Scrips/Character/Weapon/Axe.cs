using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Axe : Bullet
{
    protected override void Update()
    {
        base.Update();
        transform.Rotate(rotateSpeed, 0, 0);
    }
}
