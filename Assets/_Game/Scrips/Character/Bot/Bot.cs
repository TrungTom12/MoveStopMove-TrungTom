using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class Bot : Character
{
    [SerializeField] Transform spawnPosTrans;
    [SerializeField] GameObject skin;
    private NavMeshAgent agent;
    public Transform targetFollow;

    Vector3 spawnPos;
    private Vector3 destination;

    private List<Transform> l_targetFollow = new List<Transform>();
    //trang thai bot 
    private IState currentState;
    public IState CurrentState { get => currentState; set => currentState = value; }

    protected override void Start()
    {
        base.Start();
        agent = GetComponent<NavMeshAgent>();
        destination = agent.destination;
        spawnPos = spawnPosTrans.position;
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

    public void FollowTarget() //neu vi tri diem den hon 1f voi target thi cap nhat cho vi tri diem den 
    {
        ChangeAnim(Constan.ANIM_RUN);
        Bot bot;
        if (targetFollow.TryGetComponent<Bot>(out bot))
        {
            if (bot.CurrentState is DieState)
            {
                SetRandomTargetFollow();
            }
        }
        if (Vector3.Distance(destination, targetFollow.position) > 1.0f)
        {
            destination = targetFollow.position;
            agent.destination = destination;
        }
    }

    public void SetRandomTargetFollow()
    {
        ChangeAnim(Constan.ANIM_RUN);
        List<Transform> targets = new List<Transform>();
        Bot bot;

        foreach (Transform t in l_targetFollow)
        {
            if (t.gameObject.TryGetComponent<Bot>(out bot))
            {
                if (bot.CurrentState is not DieState)
                    targets.Add(t);
            }
            else
            {
                targets.Add(t);
            }

        }
      
        targetFollow = targets[Random.Range(0, targets.Count)];
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
        foreach (Transform t in GameManager.GetInstance().L_character)
        {
            if (!t.Equals(transform) && !l_targetFollow.Contains(t))
                l_targetFollow.Add(t);
        }
        skin.SetActive(true);
        //Debug.Log(l_targetFollow.Count);
        CharacterCollider.enabled = true;
        ChangeState(new IdleState());
    }

    public void Despawn()
    {
        //TODO
        //ChangeAnim(Constan.ANIM_DEAD);
        skin.SetActive(false);
        if (!GameManager.GetInstance().IsSpawnEnemy())
        {
            return;
        }
        GameManager.GetInstance().NumSpawn -= 1;
        OnInit();
        //transform.position = GameController.GetInstance().GetRandomSpawnPos();
        transform.position = GameManager.GetInstance().GetRandomSpawnPos();
    }

    public override void OnDeath()
    {
        //TODO
        base.OnDeath();
        GameManager.GetInstance().UpdateAliveText();
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
