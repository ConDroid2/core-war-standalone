using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnDeath : AbilityCondition
{
    InPlayCardController card;
    public OnDeath(InPlayCardController card)
    {
        this.card = card;
        (this.card as UnitController).unitDie.OnActionStart += HandleCondition;
    }

    protected override void HandleCondition()
    {
        if (card.photonView.IsMine)
        {
            MainSequenceManager.Instance.Add(abilities);
        }    
    }

    public override void Delete()
    {
        (card as UnitController).unitDie.OnActionStart -= HandleCondition;
    }

    public override void SetUp()
    {
        (card as UnitController).unitDie.OnActionStart += HandleCondition;
    }
}
