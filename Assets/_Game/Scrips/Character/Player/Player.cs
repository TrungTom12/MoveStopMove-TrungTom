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
    float timerDead = 0f;

    internal PlayerState MyState { get => _state; set => _state = value; }
  
    protected override void Start()
    {
        base.Start();
        OnInit();
    }

    void OnInit()
    {
        base.OnInit();
        _joystick = GameManager.GetInstance().joystick;
        GameManager.GetInstance().cameraFollow.SetTargetFollow(transform);
        timerDead = 0;
        _state = PlayerState.Idle;
        ChangeAnim(Constan.ANIM_IDLE);
    }

    protected override void Update()
    {
        if (_state is PlayerState.Dead)
        {
            timerDead += Time.deltaTime;
            if (timerDead > 2f)
            {
                PoolingPro.GetInstance().ReturnToPool(CharacterType.Player.ToString(), this.gameObject);
                GameManager.GetInstance().Lose();
            }
            return;
        }
        if (_state is PlayerState.Attacked)
        {
            return;
        }

        Run();
        //neu muc tieu da xác dinh va chet thi loai bo va chon random tu danh sach neu con 
        if (targetAttack != null && targetAttack.GetComponent<Character>().IsDead)
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

        // xac dinh xem khi nào co the tan cong muc tiêu 
        if (l_AttackTarget.Contains(targetAttack) && timer >= delayAttack)
        {
            Attack();
            timer = 0;
        }
       
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
        
        _state = PlayerState.Dead;
        base.OnDeath();
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
