using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : IState
{
    private float timer = 0;
    private float ranTime;
    public void OnEnter(Bot bot)
    {
        Debug.Log("idle");
        ranTime = Random.Range(0.5f, 1f);
        bot.ChangeAnim("idle");
        bot.StopMoving();
    }

    public void OnExecute(Bot bot)
    {
        timer += Time.deltaTime;
        if (timer > ranTime)
        {
            bot.ChangeState(new PatrolState());
        }
    }

    public void OnExit(Bot bot)
    {

    }
}
