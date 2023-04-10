using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField] protected Rigidbody _rigidbody;
    [SerializeField] protected float _moveSpeed;
    [SerializeField] protected float _rotateSpeed;
    [SerializeField] protected float force_Throw;

    [SerializeField] GameObject bulletPrefab;
    [SerializeField] protected Transform throwPoint;
    protected float waitThrow = 0.8f;

    private Collider characterCollider;
    public Collider CharacterCollider { get => characterCollider; set => characterCollider = value; }

    protected Character targetAttack;

    protected bool isReadyAttack = false;
    protected float attackTime = 2f;
    protected float timer = 0;
    protected float delayAttack = 0.1f;

    public List<Character> l_AttackTarget = new List<Character>();
    public List<Character> L_AttackTarget { get => l_AttackTarget; set => l_AttackTarget = value; }



    protected virtual void Start()
    {
        characterCollider = GetComponent<Collider>();
    }

    protected virtual void Update()
    {

    }

    public virtual void Run()
    {

    }

    public virtual void SetTargetDirect(Vector3 targetPos)
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

        SetTargetDirect(targetAttack.transform.position);
        ChangeAnim("attack");
        isReadyAttack = false;
        Vector3 direct = throwPoint.position - transform.position;
        StartCoroutine(Throw(direct));

    }

    public IEnumerator Throw(Vector3 direct)
    {
        yield return new WaitForSeconds(waitThrow);
        GameObject bullet = Pooling.GetInstance().GetGameObject(throwPoint.position);
        bullet.transform.rotation = transform.rotation;
        bullet.GetComponent<Rigidbody>().AddForce(direct.x * force_Throw, 0, direct.z * force_Throw);

    }

    //Anim
    string currentAnimName;
    [SerializeField] Animator anim;
    public void ChangeAnim(string animName)
    {
        //doan nay a sai
        if (currentAnimName != animName)
        {
            anim.ResetTrigger(animName);

            currentAnimName = animName;

            anim.SetTrigger(currentAnimName);
        }
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