using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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


    [SerializeField] private float rangeDetect;
    private float intialRadiusSightZone;
    [SerializeField] SphereCollider sightZone;

    [SerializeField] protected Transform weaponPos;
    [SerializeField] protected GameObject weaponHold;

    //TF
    private Transform tf;
    private Transform weaponHoldTransform;
    private Transform sightZoneTransform;

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

    public Transform WeaponHoldTransform
    {
        get
        {
            if (weaponHoldTransform == null)
            {
                weaponHoldTransform = weaponHold.transform;
            }
            return weaponHoldTransform;
        }
    }
    public Transform SightZoneTransform
    {
        get
        {
            if (sightZoneTransform == null)
            {
                sightZoneTransform = sightZone.transform;
            }
            return sightZoneTransform;
        }
    }

    public bool IsDead = false;

    protected virtual void Start()
    {
        characterCollider = GetComponent<Collider>();
        intialRadiusSightZone = sightZone.radius;
    }

    virtual protected  void Update() { }
    
    public virtual void Run() { }
    
    public virtual void OnInit()
    {
        point = 0;
        ChangeEquipment(currentWeapon);
        weaponHold.SetActive(true);
        L_AttackTarget.Clear();
        IsDead = false;
    }

    public virtual void SetTargetDirect(Vector3 targetPos) //Setup huong toi doi tuong
    {
        transform.LookAt(targetPos);
    }

    public virtual void OnDeath()
    {
        StopAllCoroutines();
        ChangeAnim(Constan.ANIM_DEAD);
        IsDead = true;
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
        weaponHold.SetActive(false);
        SoundManager.GetInstance().PlayOneShot(SoundManager.GetInstance().attackSound);
        
        Bullet bullet = PoolingPro.GetInstance().GetFromPool(currentWeapon.ToString(),throwPoint.position).GetComponent<Bullet>();
        bullet.tagWeapon = currentWeapon;
        bullet.TF.rotation = transform.rotation;
        bullet/*.GetComponent<Rigidbody>()*/.AddForce(direct.x * _forceThrow, 0, direct.z * _forceThrow);
        bullet/*.GetComponent<Bullet>()*/.SetOwner(this);
        bullet.TF.localScale *= (1 + 0.1f * point);
        yield return new WaitForSeconds(attackTime * 0.5f);
        weaponHold.SetActive(true);

        
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

    public void ChangeEquipment(WeaponType weapon)
    {
        SetWeapon(weapon);
    }
    
    public void SetWeapon(WeaponType weapon)
    {
        PoolingPro.GetInstance().ReturnToPool(PoolingPro.GetInstance().weaponHolds[currentWeapon].ToString(), weaponHold);
        this.currentWeapon = weapon;
        this.weaponHold = PoolingPro.GetInstance().GetFromPool(PoolingPro.GetInstance().weaponHolds[weapon].ToString(), weaponPos.position);
        //TODO: cache transform
        this.weaponHold.transform.SetParent(weaponPos);
        SightZoneTransform.localScale = new Vector3(1f, 1f, 1f) * StaticData.RangeWeapon[weapon];
    }

    public void UpPoint(int point)
    {
        this.point += point;
        if (this is Player)
        {
            GameManager.GetInstance().cameraFollow.Offset += new Vector3(0, 1 - 1);
        }
        TF.localScale = Vector3.one * this.point * 0.1f + Vector3.one;
    }

}