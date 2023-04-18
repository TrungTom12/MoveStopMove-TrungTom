using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public enum WeaponType
{
    Axe, /*Bullet*/ Knife, Boomerang, /*Arrow*/ Candy, /*Uzi*/
}

public class Bullet : MonoBehaviour
{
    private Transform tf;
    protected Rigidbody rb;

    [SerializeField] protected float rotateSpeed = 10f;

    
    protected float timer = 0;
    protected float timeExist = 1.5f;
    Character character;
    Character owner;
   
    public WeaponType tagWeapon;
    [SerializeField] float range;
    protected Rigidbody rbWeapon;


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

    public Rigidbody RbWeapon
    {
        get
        {
            if (rbWeapon == null) 
            {
                rbWeapon = GetComponent<Rigidbody>();
            }
            return rbWeapon;
        }
    }

    public float Timer { get => timer; set => timer = value; }

    private void Start()
    {
        
    }
    protected virtual void Update()
    {
        timer += Time.deltaTime;
        if (timer > timeExist)
        {
            //Pooling.GetInstance().ReturnGameObject(this.gameObject);
            PoolingPro.GetInstance().ReturnToPool(tagWeapon.ToString(), gameObject);
        }
        
    }
    public void ResetForce()
    {
        rbWeapon.velocity = Vector3.zero;
        rbWeapon.angularVelocity = Vector3.zero;
    }
    protected void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Constan.TAG_OBSTACLE))
        {
            PoolingPro.GetInstance().ReturnToPool(tagWeapon.ToString(), gameObject);
        }

        if (other.CompareTag(Constan.TAG_PLAYER))
        {
            //character = other.GetComponent<Character>();
            //Pooling.GetInstance().ReturnGameObject(this.gameObject);
            //if (character is Bot)
            //{
            //    other.GetComponent<Bot>().ChangeState(new DieState());
            //}
            PoolingPro.GetInstance().ReturnToPool(tagWeapon.ToString(), gameObject);
            //other.GetComponent<Player>().OnDeath();
            Cache.GetCharacter(other).OnDeath();
            owner.UpPoint(1);
            SoundManager.GetInstance().PlayOneShot(SoundManager.GetInstance().killSound);

        }

        if (other.CompareTag(Constan.TAG_BOT))
        {
            PoolingPro.GetInstance().ReturnToPool(tagWeapon.ToString(), gameObject);
            other.GetComponent<Bot>().ChangeState(new DieState());
            owner.UpPoint(1);
            SoundManager.GetInstance().PlayOneShot(SoundManager.GetInstance().killSound);
            
            if (owner is Player)
            {
                SaveLoadManager.GetInstance().Data1.Coin += 1;
                SaveLoadManager.GetInstance().Save();
            }
        }

    }

    public void SetOwner(Character character)
    {
        owner = character;
    }

    public void AddForce(float v1,float v2,float v3)
    {
        RbWeapon.AddForce(v1,v2,v3);
    }
}
