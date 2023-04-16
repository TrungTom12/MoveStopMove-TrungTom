using System.Collections;
using UnityEngine;
using UnityEngine.TextCore.Text;
using UnityEngine.UIElements;

enum PlayerState
{
    Attacked,Attacking,Dead,Run,Idle
}
public class Player : Character
{
    private Vector3 moveVector;
    [SerializeField] private FixedJoystick _joystick;
    PlayerState _state;

    internal PlayerState MyState { get => _state; set => _state = value; }
  
    protected override void Start()
    {
        base.Start();
        _joystick = FindAnyObjectByType<FixedJoystick>();
        GameManager.GetInstance().cameraFollow.SetTargetFollow(transform);
        OnInit();
    }

    void OnInit()
    {
        _state = PlayerState.Idle;
        ChangeAnim(Constan.ANIM_IDLE);
    }

    protected override void Update()
    {
        if (_state is PlayerState.Attacked || _state is PlayerState.Dead)
        {
            return;
        }

        Run();
        if (targetAttack != null && targetAttack.GetComponent<Bot>().CurrentState is DieState)
        {
            L_AttackTarget.Remove(targetAttack);
            if (l_AttackTarget.Count > 0)
                targetAttack = l_AttackTarget[Random.Range(0, l_AttackTarget.Count)];
        }
        if (l_AttackTarget.Count > 0)
        {

            if (!l_AttackTarget.Contains(targetAttack))
                targetAttack = l_AttackTarget[Random.Range(0, l_AttackTarget.Count)];
        }

        if (l_AttackTarget.Contains(targetAttack) && timer >= delayAttack)
        {
            Attack();
            timer = 0;
        }
        //if (Input.GetKeyDown(KeyCode.J))
        //{
        //    Attack();
        //}
    }

    public override void Run()
    {
        base.Run();
        moveVector = Vector3.zero;
        moveVector.x = _joystick.Horizontal * _moveSpeed * Time.deltaTime;
        moveVector.z = _joystick.Vertical * _moveSpeed * Time.deltaTime;

        if (_joystick.Horizontal != 0 || _joystick.Vertical != 0)
        {
            StopAllCoroutines();
            isReadyAttack = true;
            _state = PlayerState.Run;
            timer = 0;
            Vector3 direction = Vector3.RotateTowards(transform.forward, moveVector, _rotateSpeed * Time.deltaTime, 0.0f);
            transform.rotation = Quaternion.LookRotation(direction);
            ChangeAnim(Constan.ANIM_RUN);
        }

        else if (_joystick.Horizontal == 0 && _joystick.Vertical == 0 )
        {
            if (_state is PlayerState.Attacked || _state == PlayerState.Attacking)
            {

            }
            else
            {
                timer += Time.deltaTime;
                _state = PlayerState.Idle;
                ChangeAnim(Constan.ANIM_IDLE);
            }
        }
        transform.position = Vector3.Lerp(transform.position, transform.position + moveVector, 1f);
    }

    public override void OnDeath()
    {
        if (_state is PlayerState.Dead)
        {
            return;
        }
        base.OnDeath();
        _state = PlayerState.Dead;
        SaveLoadManager.GetInstance().Data1.Coin += point;
        SaveLoadManager.GetInstance().Data1.WeaponCurrent = currentWeapon.ToString();
        SaveLoadManager.GetInstance().Save();
        Debug.Log("Now Coin: " + SaveLoadManager.GetInstance().Data1.Coin);
        Debug.Log("Now Weapon: " + SaveLoadManager.GetInstance().Data1.WeaponCurrent);
    }

    //Attack
    IEnumerator ResetAttack()
    {
        yield return new WaitForSeconds(attackTime);
        _state = PlayerState.Idle;
    }

    IEnumerator ActiveAttack()
    {
        yield return new WaitForSeconds(waitThrow);
        _state = PlayerState.Attacked;
    }

    public override void Attack()
    {
        if (!isReadyAttack)
        {
            return;
        }

        base.Attack();
        _state = PlayerState.Attacking;
        StartCoroutine(ActiveAttack());
        StartCoroutine(ResetAttack());
    }
}
