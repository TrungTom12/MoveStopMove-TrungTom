using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DieState : IState
{
    float timer = 0;
    float timeDelay = 3f;
    public void OnEnter(Bot bot)
    {
        bot.CharacterCollider.enabled = false;
        bot.StopMoving();
        bot.OnDeath();

    }

    public void OnExecute(Bot bot)
    {
        timer += Time.deltaTime;
        if (timer > timeDelay)
        {
            bot.Despawn();
        }
    }

    public void OnExit(Bot bot)
    {
    }
}
