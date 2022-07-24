using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnUnitStrengthIncreased : AbilityCondition
{
    UnitController card;
    public OnUnitStrengthIncreased(UnitController card)
    {
        this.card = card;
        SetUp();
    }

    public override void Delete()
    {
        if (card.isMine)
        {
            card.OnStrengthIncreased -= HandleCondition;
        }
    }

    public override void SetUp()
    {
        if (card.isMine)
        {
            card.OnStrengthIncreased += HandleCondition;
        }
    }

    protected override void HandleCondition()
    {
        MainSequenceManager.Instance.Add(abilities);
    }
}
