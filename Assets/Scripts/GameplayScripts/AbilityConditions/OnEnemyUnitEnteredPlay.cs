using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnEnemyUnitEnteredPlay : AbilityCondition
{
    List<SequenceSystem.TargetedAbility> targetedAbilities = new List<SequenceSystem.TargetedAbility>();
    bool isMine;

    public OnEnemyUnitEnteredPlay(bool isMine)
    {
        this.isMine = isMine;
        if (isMine)
        {
            Enemy.Instance.unitManager.OnUnitAdded += HandleCondition;
        }
    }
    public override void Delete()
    {
        if (isMine)
        {
            Enemy.Instance.unitManager.OnUnitAdded -= HandleCondition;
        }
    }

    public void AddAbility(SequenceSystem.TargetedAbility ability)
    {
        targetedAbilities.Add(ability);
    }

    protected void HandleCondition(CardParent unit)
    {
        Debug.Log("Targeting entered unit");
        foreach(SequenceSystem.TargetedAbility ability in targetedAbilities)
        {
            ability.SetTarget(unit.gameObject);
            MainSequenceManager.Instance.Add(ability);
        }
    }

    public override void SetUp()
    {
        if (isMine)
        {
            Enemy.Instance.unitManager.OnUnitAdded += HandleCondition;
        }
    }

    protected override void HandleCondition()
    {
        throw new System.NotImplementedException();
    }
}
