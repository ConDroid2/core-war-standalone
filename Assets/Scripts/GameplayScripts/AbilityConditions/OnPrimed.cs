using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnPrimed : AbilityCondition
{
    InPlayCardController unit;

    public OnPrimed(InPlayCardController unit)
    {
        this.unit = unit;

        Core.Instance.OnCorePrimed += HandleCondition;
    }
    protected override void HandleCondition()
    {
        if(Core.Instance.state == Core.PrimedState.ByEnemy && unit.isMine)
        {
            MainSequenceManager.Instance.Add(abilities);
        }
    }

    public override void Delete()
    {
        Core.Instance.OnCorePrimed -= HandleCondition;
    }

    public override void SetUp()
    {
        Core.Instance.OnCorePrimed += HandleCondition;
    }
}
