using System.Collections;
using UnityEngine;
using UnityEngine.TextCore.Text;
using UnityEngine.UIElements;

public class Player : Character
{
    private Vector3 moveVector;
    [SerializeField] private FixedJoystick _joystick;

    bool isAttack = false;
    bool isAttacking = false;


    protected override void Start()
    {
        //base.Start();
        ChangeAnim("idle");
    }

    protected override void Update()
    {
        if (isAttack)
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
        if (Input.GetKeyDown(KeyCode.J))
        {
            Attack();
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
            isAttacking = false;
            timer = 0;
            Vector3 direction = Vector3.RotateTowards(transform.forward, moveVector, _rotateSpeed * Time.deltaTime, 0.0f);
            transform.rotation = Quaternion.LookRotation(direction);
            ChangeAnim("run");
        }
        else if (_joystick.Horizontal == 0 && _joystick.Vertical == 0 && !isAttacking)
        {
            timer += Time.deltaTime;
            ChangeAnim("idle");
        }
        transform.position = Vector3.Lerp(transform.position, transform.position + moveVector, 1f);
    }

    IEnumerator ResetAttack()
    {
        yield return new WaitForSeconds(attackTime);
        isAttack = false;
        isAttacking = false;
    }
    IEnumerator ActiveAttack()
    {
        yield return new WaitForSeconds(waitThrow);
        isAttack = true;
    }
    public override void Attack()
    {
        if (!isReadyAttack)
        {
            return;
        }
        base.Attack();
        isAttacking = true;
        StartCoroutine(ActiveAttack());
        StartCoroutine(ResetAttack());
    }
}
