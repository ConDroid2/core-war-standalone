using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayInsteadOfDiscard : AbilityCondition
{
    CardController card;
    public PlayInsteadOfDiscard(CardController card)
    {
        this.card = card;
        SetUp();
    }
    public override void Delete()
    {
        card.discard = new SequenceSystem.CardDiscard(card);
    }

    public override void SetUp()
    {
        card.discard = card.play;
    }

    protected override void HandleCondition()
    {
        throw new System.NotImplementedException();
    }
}
