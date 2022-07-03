using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnTurnEnd : AbilityCondition
{
    InPlayCardController card;
    public OnTurnEnd(InPlayCardController card)
    {
        this.card = card;
        Player.Instance.OnEndTurn += HandleCondition;
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
        Player.Instance.OnEndTurn -= HandleCondition;
    }

    public override void SetUp()
    {
        Player.Instance.OnEndTurn += HandleCondition;
    }
}
