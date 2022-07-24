using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnUnitMaxResIncreased : AbilityCondition
{
    UnitController card;
    public OnUnitMaxResIncreased(UnitController card)
    {
        this.card = card;
        SetUp();
    }

    public override void Delete()
    {
        if (card.isMine)
        {
            card.OnMaxResillienceIncreased -= HandleCondition;
        }
    }

    public override void SetUp()
    {
        if (card.isMine)
        {
            card.OnMaxResillienceIncreased += HandleCondition;
        }
    }

    protected override void HandleCondition()
    {
        MainSequenceManager.Instance.Add(abilities);
    }
}
