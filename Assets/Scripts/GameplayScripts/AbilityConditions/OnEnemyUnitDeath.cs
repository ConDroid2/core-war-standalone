using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnEnemyUnitDeath : AbilityCondition
{
    bool isMine;
    public OnEnemyUnitDeath(bool isMine)
    {
        this.isMine = isMine;
        if (this.isMine)
        {
            Enemy.Instance.unitManager.OnUnitDeath += HandleCondition;
        }
    }
    public override void Delete()
    {
        if (isMine)
        {
            Enemy.Instance.unitManager.OnUnitDeath -= HandleCondition;
        }
    }

    private void HandleCondition(UnitController unit)
    {
        MainSequenceManager.Instance.Add(abilities);
    }

    public override void SetUp()
    {
        if (isMine)
        {
            Enemy.Instance.unitManager.OnUnitDeath += HandleCondition;
        }
    }

    protected override void HandleCondition()
    {
        throw new System.NotImplementedException();
    }
}
