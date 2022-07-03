using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnDrawn : AbilityCondition
{
    CardController card;
    public OnDrawn(CardController card)
    {
        
        this.card = card;
        SetUp();
    }
    public override void Delete()
    {
        if (card.photonView.IsMine)
        {
            card.putInHand.OnActionStart -= HandleCondition;
        }
    }

    public override void SetUp()
    {
        if (card.photonView.IsMine)
        {
            card.putInHand.OnActionStart += HandleCondition;
        }
    }

    protected override void HandleCondition()
    {
        MainSequenceManager.Instance.Add(abilities);
    }
}
