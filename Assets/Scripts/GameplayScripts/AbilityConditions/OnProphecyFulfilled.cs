using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnProphecyFulfilled : AbilityCondition
{
    InPlayCardController card;
    public OnProphecyFulfilled(InPlayCardController card)
    {
        this.card = card;
        SetUp();
    }

    protected override void HandleCondition()
    {
        MainSequenceManager.Instance.Add(abilities);
    }

    public override void Delete()
    {
        if (card.isMine)
        {
            ProphecyManager.Instance.OnRemoveProphecy -= HandleCondition;
        }
    }

    public override void SetUp()
    {
        if (card.isMine)
        {
            ProphecyManager.Instance.OnRemoveProphecy += HandleCondition;
        }
    }
}
