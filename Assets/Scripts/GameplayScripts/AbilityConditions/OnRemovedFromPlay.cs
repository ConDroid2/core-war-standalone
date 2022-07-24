using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnRemovedFromPlay : AbilityCondition
{
    InPlayCardController card;
    public OnRemovedFromPlay(InPlayCardController card)
    {
        this.card = card;
        if(card.isMine)
            this.card.OnRemovedFromPlay += HandleCondition;
    }

    protected override void HandleCondition()
    {
        MainSequenceManager.Instance.Add(abilities);
    }

    public override void Delete()
    {
        this.card.OnRemovedFromPlay -= HandleCondition;
    }

    public override void SetUp()
    {
        if(card.isMine)
            card.OnRemovedFromPlay += HandleCondition;
    }
}
