using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ScrollWeaponShow : MonoBehaviour
{
    private Transform tf;
    [SerializeField] private float rotateSpeed = 3f;
    public Transform TF
    {
        get
        {
            if (tf == null)
            {
                tf = transform;
            }
            return tf;
        }
    }
    private void Start()
    {
        tf = transform;
    }

    // Update is called once per frame
    void Update()
    {
        tf.Rotate(0, rotateSpeed, 0);
    }
}
