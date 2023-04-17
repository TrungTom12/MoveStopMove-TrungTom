using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolState : IState
{
    float timer = 0;
    float duration;
    public void OnEnter(Bot bot)
    {
        bot.SetRandomTargetFollow();
        duration = Random.Range(1f, 4f);
    }

    public void OnExecute(Bot bot)
    {
        timer += Time.deltaTime;
        bot.FollowTarget();
        if (timer > duration)
        {
            bot.ChangeState(new IdleState());
        }
        if (bot.IsHaveTargetInRange() && timer > 1f)
        {
            // thuc hien tan cong
            bot.ChangeState(new AttackState());
        }
    }

    public void OnExit(Bot bot)
    {
    }
}
