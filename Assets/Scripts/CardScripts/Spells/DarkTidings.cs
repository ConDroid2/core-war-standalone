using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DarkTidings : CardScript
{
    public override void InHandSetUp()
    {
        CardController card = GetComponent<CardController>();
        OnPlay onPlay = new OnPlay(card);
        conditions.Add(onPlay);

        SequenceSystem.AddActionToOpponent addNightmares =
            new SequenceSystem.AddActionToOpponent(
                typeof(SequenceSystem.AddCardsToDeck),
                new object[] { 4, "Nightmare" });

        SequenceSystem.AddActionToOpponent drawSpell =
            new SequenceSystem.AddActionToOpponent(
                typeof(SequenceSystem.DrawRandomCards),
                new object[] { 1, CardSelector.TypeFilter.Spell, -1, CardSelector.CostCompare.EqualTo, "", ""});

        SequenceSystem.DrawRandomCards draw = new SequenceSystem.DrawRandomCards(amount: 1, typeFilter: CardSelector.TypeFilter.Spell);

        onPlay.AddAbility(addNightmares);
        onPlay.AddAbility(drawSpell);
        onPlay.AddAbility(draw);
    }
}
