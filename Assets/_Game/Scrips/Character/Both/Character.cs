using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CharacterType
{
    Player,Bot
}
public class Character : MonoBehaviour
{
    //setup
    [SerializeField] protected Rigidbody _rigidbody;
    [SerializeField] protected float _moveSpeed;
    [SerializeField] protected float _rotateSpeed;
    [SerializeField] protected float _forceThrow;
    //bullet
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] protected Transform throwPoint;
    
    //doi tuong va cham
    private Collider characterCollider;
    public Collider CharacterCollider { get => characterCollider; set => characterCollider = value; }
    //list doi tuong muc tieu 
    public List<Character> l_AttackTarget = new List<Character>();
    public List<Character> L_AttackTarget { get => l_AttackTarget; set => l_AttackTarget = value; }
    //parameter
    protected float waitThrow = 0.4f;
    protected bool isReadyAttack = false;
    protected float attackTime = 2f;
    protected float timer = 0;
    protected float delayAttack = 0.1f;

    protected Character targetAttack;
    //[SerializeField] private string tagWeapon;
    //UpPoint
    protected int point = 0;
    //Weapon
    [SerializeField] protected WeaponType currentWeapon;
    protected virtual void Start()
    {
        characterCollider = GetComponent<Collider>();
    }

    protected virtual void Update() { }
    
    public virtual void Run() { }
    
    public virtual void SetTargetDirect(Vector3 targetPos) //Setup huong toi doi tuong
    {
        transform.LookAt(targetPos);
    }

    public virtual void OnDeath()
    {
        StopAllCoroutines();
        ChangeAnim(Constan.ANIM_DEAD);
    }

    public virtual void Attack()
    {
        //dinh huong den doi tuong
        //tinh khoang cach tu diem ban toi doi tuong
        SetTargetDirect(targetAttack.transform.position); 
        ChangeAnim(Constan.ANIM_ATTACK);
        isReadyAttack = false;
        Vector3 direct = throwPoint.position - transform.position;
        StartCoroutine(Throw(direct));

    }

    public IEnumerator Throw(Vector3 direct) 
    {
        //thoi gian tre khi nem
        //lay bullet tu pooling tai vi tri 
        //dinh huong quay
        //them luc cho bullet
        //nang Scale 
        yield return new WaitForSeconds(waitThrow);
        GameObject bullet = PoolingPro.GetInstance().GetFromPool(currentWeapon.ToString(),throwPoint.position);
        bullet.GetComponent<Bullet>().tagWeapon = currentWeapon;
        bullet.transform.rotation = transform.rotation;
        bullet.GetComponent<Rigidbody>().AddForce(direct.x * _forceThrow, 0, direct.z * _forceThrow);
        bullet.GetComponent<Bullet>().SetOwner(this);
        bullet.transform.localScale *= (1 + 0.01f * point);
    }

    //Anim
    string currentAnimName;
    [SerializeField] Animator anim;
    public void ChangeAnim(string animName)
    {
        
        if (currentAnimName != animName)
        {
            anim.ResetTrigger(animName);

            currentAnimName = animName;

            anim.SetTrigger(currentAnimName);
        }

    }

    public void UpPoint(int point)
    {
        this.point += point;
        if (this is Player)
        {
            GameManager.GetInstance().cameraFollow.Offset += new Vector3(0, 1 - 1);
        }
        this.transform.localScale = Vector3.one * this.point * 0.1f + Vector3.one;
    }

    public void ChangeEquipment()
    {

    }





    ////TF
    //private Transform tf;
    //public Transform TF
    //{
    //    get
    //    {
    //        if (tf == null)
    //        {
    //            tf = transform;
    //        }
    //        return tf;
    //    }
    //}
    
}