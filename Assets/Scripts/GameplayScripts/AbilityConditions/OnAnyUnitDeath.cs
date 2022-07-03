using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnAnyUnitDeath : AbilityCondition
{
    bool isMine;
    public OnAnyUnitDeath(bool isMine)
    {
        this.isMine = isMine;
        if (this.isMine)
        {
            Player.Instance.unitManager.OnUnitDeath += HandleCondition;
            Enemy.Instance.unitManager.OnUnitDeath += HandleCondition;
        }
    }
    public override void Delete()
    {
        if (isMine)
        {
            Player.Instance.unitManager.OnUnitDeath -= HandleCondition;
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
            Player.Instance.unitManager.OnUnitDeath += HandleCondition;
            Enemy.Instance.unitManager.OnUnitDeath += HandleCondition;
        }
    }

    protected override void HandleCondition()
    {
        throw new System.NotImplementedException();
    }
}
