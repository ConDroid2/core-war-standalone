using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnTurnStart : AbilityCondition
{
    InPlayCardController card;
    public OnTurnStart(InPlayCardController card) 
    {
        this.card = card;
        Player.Instance.OnStartTurn += HandleCondition;
    }

    public OnTurnStart()
    {
        this.card = null;
        Player.Instance.OnStartTurn += HandleCondition;
    }

    protected override void HandleCondition()
    {
        if ((card != null && card.isMine) || card == null)
        {
            MainSequenceManager.Instance.Add(abilities);
        }  
    }

    public override void Delete()
    {
        Player.Instance.OnStartTurn -= HandleCondition;
    }

    public override void SetUp()
    {
        Player.Instance.OnStartTurn += HandleCondition;
    }
}
