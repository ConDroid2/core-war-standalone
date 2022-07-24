using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnIgnitedSpell : AbilityCondition
{
    InPlayCardController card;
    public OnIgnitedSpell(InPlayCardController card)
    {
        this.card = card;
        if (card.isMine)
        {
            Ignite.OnIgnitedSpellPlayed += HandleCondition;
        }  
    }

    protected override void HandleCondition()
    {
        MainSequenceManager.Instance.Add(abilities);
    }

    public override void Delete()
    {
        if (card.isMine)
        {
            Ignite.OnIgnitedSpellPlayed -= HandleCondition;
        }
    }

    public override void SetUp()
    {
        if (card.isMine)
        {
            Ignite.OnIgnitedSpellPlayed += HandleCondition;
        }
    }
}
