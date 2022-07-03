using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using SequenceSystem;
public class CardScript : MonoBehaviour
{
    public List<AbilityCondition> conditions = new List<AbilityCondition>();

    public Action OnDeathEvent;

    public virtual void InHandSetUp() { }
    public virtual void InPlaySetUp() { }

    public AbilityCondition AddCondition(AbilityCondition condition)
    {
        conditions.Add(condition);
        return condition;
    }

    public virtual void OnDeath()
    {
        foreach(AbilityCondition condition in conditions)
        {
            condition.Delete();
        }

        OnDeathEvent?.Invoke();
    }

    public virtual void InHandDeath()
    {
        foreach (AbilityCondition condition in conditions)
        {
            condition.Delete();
        }

        OnDeathEvent?.Invoke();
    }

    public virtual void InPlayDeath()
    {
        foreach (AbilityCondition condition in conditions)
        {
            condition.Delete();
        }

        OnDeathEvent?.Invoke();
    }

    public void OnExorcise()
    {
        foreach(AbilityCondition condition in conditions)
        {
            condition.Delete();
        }
    }
}
