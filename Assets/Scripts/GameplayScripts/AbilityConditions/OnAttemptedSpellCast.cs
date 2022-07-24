using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnAttemptedSpellCast : AbilityCondition
{
    InPlayCardController card;
    public OnAttemptedSpellCast(InPlayCardController card)
    {
        this.card = card;
        if (card.isMine)
        {
            MainSequenceManager.Instance.mainSequence.OnActionAddedToQueue += HandleCondition;
            // MainSequenceManager.Instance.secondarySequence.OnActionAddedToQueue += HandleCondition;
        }
    }
    public override void Delete()
    {
        if (card.isMine)
        {
            MainSequenceManager.Instance.mainSequence.OnActionAddedToQueue -= HandleCondition;
        }
    }

    public override void SetUp()
    {
        if(card.isMine)
            MainSequenceManager.Instance.mainSequence.OnActionAddedToQueue += HandleCondition;
    }

    protected void HandleCondition(System.Type type)
    {
        // Debug.Log(type.ToString() + " was added to the queue");
        if(type == typeof(SequenceSystem.InHandSpellPlay))
        {
            MainSequenceManager.Instance.Add(abilities);
        }
    }

    protected override void HandleCondition()
    {
        throw new System.NotImplementedException();
    }
}
