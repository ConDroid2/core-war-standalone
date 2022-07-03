using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnEnterCore : AbilityCondition
{
    InPlayCardController card;
    public OnEnterCore(InPlayCardController card)
    {
        this.card = card;
        SetUp();
    }
    public override void Delete()
    {
        if (card.photonView.IsMine)
        {
            Core.Instance.OnCorePrimed -= HandleCondition;
        }
    }

    public override void SetUp()
    {
        if (card.photonView.IsMine)
        {
            Core.Instance.OnCorePrimed += HandleCondition;
        }
    }

    protected override void HandleCondition()
    {
        if(Core.Instance.enteredCard == card)
        {
            MainSequenceManager.Instance.Add(abilities);
        }
    }
}
