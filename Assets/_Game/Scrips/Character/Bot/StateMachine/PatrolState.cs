using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolState : IState
{
    public void OnEnter(Bot bot)
    {
        //Debug.Log("Run");
        bot.SetRandomTargetFollow();
    }

    public void OnExecute(Bot bot)
    {
        bot.FollowTarget();
        if (bot.IsHaveTargetInRange())
        {
            // thuc hien tan cong
            bot.ChangeState(new AttackState());
        }
    }

    public void OnExit(Bot bot)
    {

    }
}
