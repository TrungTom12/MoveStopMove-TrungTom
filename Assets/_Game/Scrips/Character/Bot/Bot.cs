﻿using System.Collections;
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
        OnInit();
    }

    protected override void Update()
    {
        if (targetAttack != null && targetAttack.GetComponent<Character>() is Bot)
            if (targetAttack.GetComponent<Bot>().IsDead)
            {
                l_AttackTarget.Remove(targetAttack);
            }

        if (targetAttack != null && targetAttack.GetComponent<Character>() is Player)
            if (targetAttack.GetComponent<Player>().MyState is PlayerState.Dead)
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

        if (targetFollow == null)
        {
            SetRandomTargetFollow();
            return;
        }

        Bot bot;

        if (targetFollow.TryGetComponent<Bot>(out bot)) // Ktra neu bot chet thi tim doi tuong khac
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

    public void SetRandomTargetFollow() // ktra cac doi tuong trong list không chet thì them vao va chon ngau nhien de di chuyen toi 
    {
        ChangeAnim(Constan.ANIM_RUN);
        List<Transform> targets = new List<Transform>();

        for (int i = 0; i < l_targetFollow.Count; i++)
        {
            if (!l_targetFollow[i].GetComponent<Character>().IsDead)
            {
                targets.Add(l_targetFollow[i]);
            }
        }

        if (targets.Count > 0)
        {
            targetFollow = targets[Random.Range(0, targets.Count)];
            destination = targetFollow.position;
            agent.destination = destination;
        }

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

    public override void OnInit()
    {
        base.OnInit();
        foreach (Transform t in GameManager.GetInstance().L_character)
        {
            if (!t.Equals(transform) && !l_targetFollow.Contains(t))
                l_targetFollow.Add(t);
        }
        weaponHold.SetActive(true);
        skin.SetActive(true);
        CharacterCollider.enabled = true;
        ChangeState(new IdleState());
    }

    public void Despawn()
    {
        //TODO
        
        skin.SetActive(false);
        if (!GameManager.GetInstance().IsSpawnEnemy())
        {
            PoolingPro.GetInstance().ReturnToPool(CharacterType.Bot.ToString(), this.gameObject);
            return;
        }
        GameManager.GetInstance().NumSpawn -= 1;
        OnInit();
        //transform.position = GameController.GetInstance().GetRandomSpawnPos();
        TF.position = GameManager.GetInstance().GetRandomSpawnPos();
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
        destination = TF.position;
        agent.destination = destination;
    }


    public void ChangeState(IState newState)
    {
        currentState?.OnExit(this);
        currentState = newState;
        currentState?.OnEnter(this);
        //if (currentState != null)
        //{
        //    currentState.OnExit(this);
        //}

        //currentState = newState;

        //if (currentState != null)
        //{
        //    currentState.OnEnter(this);
        //}
    }



}
