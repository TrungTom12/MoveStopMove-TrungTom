using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private float rotateSpeed = 5f;
    private Rigidbody rb;
    private float timer = 0;
    private float timeExist = 3f;
    Character character;
    public string tagWeapon;

    public float Timer { get => timer; set => timer = value; }

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    void Update()
    {
        timer += Time.deltaTime;
        if (timer > timeExist)
        {
            //Pooling.GetInstance().ReturnGameObject(this.gameObject);
            PoolingPro.GetInstance().ReturnToPool(tagWeapon, gameObject);
        }
        transform.Rotate(rotateSpeed, 0, 0);
    }
    public void ResetForce()
    {
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            //character = other.GetComponent<Character>();
            //Pooling.GetInstance().ReturnGameObject(this.gameObject);
            //if (character is Bot)
            //{
            //    other.GetComponent<Bot>().ChangeState(new DieState());
            //}
            PoolingPro.GetInstance().ReturnToPool(tagWeapon, gameObject);
        }

        if (other.tag == "Bot")
        {
            PoolingPro.GetInstance().ReturnToPool(tagWeapon, gameObject);
            other.GetComponent<Bot>().ChangeState(new DieState());
        }
    }

}
