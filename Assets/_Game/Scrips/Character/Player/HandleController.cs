using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandleController : MonoBehaviour
{
    private bool isTouch = false;
    void Update()
    {
        if (!isTouch)
        {
            transform.position = Input.mousePosition;
        }
        if (Input.GetMouseButtonDown(0))
        {
            isTouch = true;
        }
        if (Input.GetMouseButtonUp(0))
        {
            isTouch = false;
        }
    }
}
