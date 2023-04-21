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
    protected float attackTime = 1f;
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

    //TypeShop
    //Weapon
    [SerializeField] protected Transform weaponPos;
    [SerializeField] protected GameObject weaponHold;
    //Head
    [SerializeField] protected HeadType currentHead;
    [SerializeField] protected GameObject headShow;
    [SerializeField] protected Transform headPos;
    //Paint
    protected Material currentPantMaterial;
    [SerializeField] protected SkinnedMeshRenderer pantMeshRender;
    //Shield
    [SerializeField] protected ShieldType currentShield;
    [SerializeField] protected GameObject shieldShow;
    [SerializeField] protected Transform shieldPos;
    //Wing
    [SerializeField] protected Transform wingPos;
    [SerializeField] protected GameObject wingShow;
    [SerializeField] protected WingType currentWingType;
    //Tail
    [SerializeField] protected TailType currentTailType;
    [SerializeField] protected GameObject tailShow;
    [SerializeField] protected Transform tailPos;
    //SetType
    [SerializeField] protected SetType currentSetType;
    [SerializeField] protected ParticleSystem bloodSystem;
    [SerializeField] protected SkinnedMeshRenderer colorSkin;
    [SerializeField] protected Material defaultMaterial;


    //TF - cache
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

	public ParticleSystem BloodSystem { get => bloodSystem; set => bloodSystem = value; }
    public bool IsDead = false;

    protected virtual void Start()
    {
        characterCollider = GetComponent<Collider>();
        intialRadiusSightZone = sightZone.radius;
    }

    virtual protected void Update() { }

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
		SoundManager2.GetInstance().PlaySound(Constan.DEATH_MUSIC_NAME);
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
		SoundManager2.GetInstance().PlaySound("Nem vu khi");
        Bullet bullet = PoolingPro.GetInstance().GetFromPool(currentWeapon.ToString(), throwPoint.position).GetComponent<Bullet>();

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
        this.weaponHold.transform.rotation = new Quaternion(0, 0, 0, 0);
        //this.WeaponHoldTransform.SetParent(weaponPos);
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

    //Clother

    public void SetPant(Material material)
    {
        if (material == null)
        {
            pantMeshRender.gameObject.SetActive(false);
        }
        else
        {
            pantMeshRender.gameObject.SetActive(true);
        }
        currentPantMaterial = material;
        pantMeshRender.material = material;
    }


    public void SetHead(HeadType head)
    {
        try
        {
            PoolingPro.GetInstance().ReturnToPool(currentHead.ToString(), headShow);
        }
        catch
        {
            //Debug.Log("L?i Thu h?i Head");
        }
        this.currentHead = head;
        this.headShow = PoolingPro.GetInstance().GetFromPool(currentHead.ToString(), headPos.position);
        headShow.transform.SetParent(headPos);
        headShow.transform.rotation = new Quaternion(0, 0, 0, 0);
    }

    public void SetShield(ShieldType shield)
    {
        try
        {
            PoolingPro.GetInstance().ReturnToPool(currentShield.ToString(), shieldShow);
        }
        catch
        {
            //Debug.Log("Can not Return Shield");
        }
        this.currentShield = shield;
        this.shieldShow = PoolingPro.GetInstance().GetFromPool(currentShield.ToString(), shieldPos.position);
        shieldShow.transform.SetParent(shieldPos);
        shieldShow.transform.rotation = new Quaternion(0, 0, 0, 0);
    }

    public void SetWing(WingType wing)
    {
        try
        {
            PoolingPro.GetInstance().ReturnToPool(currentWingType.ToString(), wingShow);
        }
        catch
        {
        }
        this.currentWingType = wing;
        this.wingShow = PoolingPro.GetInstance().GetFromPool(currentWingType.ToString(), wingPos.position);
        wingShow.transform.SetParent(wingPos);
        wingShow.transform.rotation = new Quaternion(0, 0, 0, 0);
    }

    public void SetTail(TailType tail)
    {
        try
        {
            PoolingPro.GetInstance().ReturnToPool(currentTailType.ToString(), tailShow);
        }
        catch
        {
        }
        this.currentTailType = tail;
        this.tailShow = PoolingPro.GetInstance().GetFromPool(currentTailType.ToString(), tailPos.position);
        tailShow.transform.SetParent(tailPos);
        tailShow.transform.rotation = new Quaternion(0, 0, 0, 0);
    }

    public void SetFullSet(SetType set)
    {
        RemoveAllEquip();
        currentSetType = set;
        Equipment infor = PoolingPro.GetInstance().SetValue[set];

        try
        {
            SetPant(PoolingPro.GetInstance().pantMaterials[infor.IdPant - 1]);
        }

        catch
        {
            SetPant(null);
        }

        if (infor.HeadName != null)
        {
            SetHead(StaticData.HeadEnum[infor.HeadName]);
        }

        if (infor.WingName != null)
        {
            SetWing(StaticData.WingEnum[infor.WingName]);
        }

        if (infor.TailName != null)
        {
            SetTail(StaticData.TailEnum[infor.TailName]);
        }

        if (infor.ShieldName != null)
        {
            SetShield(StaticData.ShieldEnum[infor.ShieldName]);
        }

        if (infor.IdColor > 0)
        {
            SetColorSkin(PoolingPro.GetInstance().characterMaterial[infor.IdColor - 1]);
        }

    }

    public void SetColorSkin(Material material)
    {
        colorSkin.material = material;
    }

    public void RemoveAllEquip()
    {
        try
        {
            PoolingPro.GetInstance().ReturnToPool(currentTailType.ToString(), tailShow);
        }

        catch
        {

        }

        try
        {
            PoolingPro.GetInstance().ReturnToPool(currentWingType.ToString(), wingShow);
        }

        catch
        {

        }

        try
        {
            PoolingPro.GetInstance().ReturnToPool(currentShield.ToString(), shieldShow);
        }

        catch
        {
            //Debug.Log("Can not Return Shield");
        }

        try
        {
            PoolingPro.GetInstance().ReturnToPool(currentHead.ToString(), headShow);
        }

        catch
        {
            //Debug.Log("L?i Thu h?i Head");
        }

        colorSkin.material = defaultMaterial;

    }



}