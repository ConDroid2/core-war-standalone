using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnSpellCast : AbilityCondition
{
    InPlayCardController card;
    public OnSpellCast(InPlayCardController card)
    {
        this.card = card;
        if (card.photonView.IsMine)
        {
            Player.Instance.spellManager.OnSpellCast += HandleCondition;
        }
    }

    public OnSpellCast()
    {
        this.card = null;
        Player.Instance.spellManager.OnSpellCast += HandleCondition;
    }
    protected override void HandleCondition()
    {
        MainSequenceManager.Instance.Add(abilities);
    }

    public override void Delete()
    {
        if ((card != null && card.photonView.IsMine) || card == null)
        {
            Player.Instance.spellManager.OnSpellCast -= HandleCondition;
        }
    }

    public override void SetUp()
    {
        if (card.photonView.IsMine)
        {
            Player.Instance.spellManager.OnSpellCast += HandleCondition;
        }
    }
}
