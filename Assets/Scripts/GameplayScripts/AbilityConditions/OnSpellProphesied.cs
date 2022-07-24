using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnSpellProphesied : AbilityCondition
{
    InPlayCardController card;
    public OnSpellProphesied(InPlayCardController card)
    {
        this.card = card;
        SetUp();
    }

    protected override void HandleCondition()
    {
        MainSequenceManager.Instance.Add(abilities);
    }

    public override void SetUp()
    {
        if (card.isMine)
        {
            ProphecyManager.Instance.OnSpellProphesied += HandleCondition;
        }
    }

    public override void Delete()
    {
        if (card.isMine)
        {
            ProphecyManager.Instance.OnSpellProphesied -= HandleCondition;
        }
    }
}
