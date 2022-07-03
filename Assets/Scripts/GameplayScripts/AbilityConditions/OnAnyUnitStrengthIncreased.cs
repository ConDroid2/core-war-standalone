using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnAnyUnitStrengthIncreased : AbilityCondition
{
    UnitController card;
    public OnAnyUnitStrengthIncreased(UnitController card)
    {
        this.card = card;
        SetUp();
    }

    public override void Delete()
    {
        if (card.photonView.IsMine)
        {
            Player.Instance.unitManager.OnUnitStrengthIncreased -= HandleCondition;
        }
    }

    public override void SetUp()
    {
        if (card.photonView.IsMine)
        {
            Player.Instance.unitManager.OnUnitStrengthIncreased += HandleCondition;
        }
    }

    protected override void HandleCondition()
    {
        MainSequenceManager.Instance.Add(abilities);
    }
}
