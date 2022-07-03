using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnEnterPlay : AbilityCondition
{
    InPlayCardController card;
    public OnEnterPlay(InPlayCardController card)
    {
        this.card = card;
        SetUp();      
    }

    protected override void HandleCondition()
    {
        // If show, Add show ability
        MainSequenceManager.Instance.Add(abilities);
        // If show, Add stop show ability
    }

    public override void Delete()
    {
        if(card is UnitController)
            (this.card as UnitController).unitPlay.OnActionStart -= HandleCondition;
    }

    public override void SetUp()
    {
        if (card.photonView.IsMine)
        {
            if (card is UnitController)
            {
                (card as UnitController).unitPlay.OnActionStart += HandleCondition;
            }
            else if (card is SupportController)
            {
                HandleCondition();
            }
        }
    }
}
