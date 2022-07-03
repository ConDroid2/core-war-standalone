using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbilityCondition
{
    public List<SequenceSystem.GameAction> abilities = new List<SequenceSystem.GameAction>();

    public virtual void AddAbility(SequenceSystem.GameAction ability) 
    {
        abilities.Add(ability);
    }

    protected abstract void HandleCondition();
    public abstract void Delete();
    public abstract void SetUp();
}
