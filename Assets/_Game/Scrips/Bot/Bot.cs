using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.AI;

public class Bot : Character
{
    [SerializeField] Transform spawnPosTrans;

    Vector3 spawnPos;

    private NavMeshAgent agent;

    private Vector3 destination;

    public Transform targetFollow;

    private IState currentState;

    private List<Transform> l_targetFollow = new List<Transform>();

    public IState CurrentState { get => currentState; set => currentState = value; }



    protected override void Start()
    {
        base.Start();
        agent = GetComponent<NavMeshAgent>();
        destination = agent.destination;
        spawnPos = spawnPosTrans.position;

        foreach (Transform t in GameManager.GetInstance().L_character)
        {
            if (!t.Equals(transform))

                l_targetFollow.Add(t);

        }
        OnInit();

    }

    protected override void Update()
    {
        if (targetAttack != null && targetAttack.GetComponent<Character>() is Bot)
            if (targetAttack.GetComponent<Bot>().CurrentState is DieState)
            {
                l_AttackTarget.Remove(targetAttack);
            }

        if (currentState != null)
        {
            currentState.OnExecute(this);
        }
    }

    public void FollowTarget()
    {
        ChangeAnim(Constan.ANIM_RUN);
        if (Vector3.Distance(destination, targetFollow.position) > 1.0f)
        {
            destination = targetFollow.position;
            agent.destination = destination;
        }
    }

    public void SetRandomTargetFollow()
    {
        ChangeAnim(Constan.ANIM_RUN);
        targetFollow = l_targetFollow[Random.Range(0, l_targetFollow.Count)];
        destination = targetFollow.position;
        agent.destination = destination;
    }

    public bool IsHaveTargetInRange()
    {
        if (L_AttackTarget.Count > 0)
        {
            targetAttack = l_AttackTarget[Random.Range(0, l_AttackTarget.Count)];
            return true;

        }
        return false;
        //Debug.Log("da co Enermy trong Range");
    }

    public void OnInit()
    {
        CharacterCollider.enabled = true;
        ChangeState(new IdleState());
    }

    public void Despawn()
    {
        OnInit();

        transform.position = spawnPos;
    }

    public override void OnDeath()
    {
        base.OnDeath();
    }

    public void StopMoving()
    {
        ChangeAnim(Constan.ANIM_IDLE);
        destination = transform.position;
        agent.destination = destination;
    }


    public void ChangeState(IState newState)
    {

        if (currentState != null)
        {
            currentState.OnExit(this);
        }
        currentState = newState;
        if (currentState != null)
        {
            currentState.OnEnter(this);
        }
    }



}
