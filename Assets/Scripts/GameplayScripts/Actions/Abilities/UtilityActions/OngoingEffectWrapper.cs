using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class OngoingEffectWrapper : MonoBehaviour
{
    protected enum WhenToEnd { TurnStart, TurnEnd, Never }
    protected WhenToEnd whenToEnd = WhenToEnd.Never;

    public List<AbilityCondition> conditions = new List<AbilityCondition>();

    public event Action OnEffectEnded;

    public void SetUp()
    {
        if(whenToEnd == WhenToEnd.TurnEnd)
        {
            Player.Instance.OnEndTurn += EndEffect;
        }
        else if(whenToEnd == WhenToEnd.TurnStart)
        {
            Player.Instance.OnStartTurn += EndEffect;
        }
    }

    public void EndEffect()
    {
        if(whenToEnd == WhenToEnd.TurnEnd)
        {
            Player.Instance.OnEndTurn -= EndEffect;
        }
        else if(whenToEnd == WhenToEnd.TurnStart)
        {
            Player.Instance.OnStartTurn -= EndEffect;
        }

        OnEffectEnded?.Invoke();

        foreach(AbilityCondition condition in conditions)
        {
            condition.Delete();
        }

        Destroy(this);
    }
}
