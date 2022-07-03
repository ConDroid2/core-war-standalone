using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MindReaper : CardScript
{
    public override void InPlaySetUp()
    {
        UnitController unit = GetComponent<UnitController>();
        OnEnterPlay onEnterPlay = new OnEnterPlay(unit);
        conditions.Add(onEnterPlay);
        OnDeath onDeath = new OnDeath(unit);
        conditions.Add(onDeath);

        SequenceSystem.AddActionToOpponent discardDeck =
            new SequenceSystem.AddActionToOpponent(typeof(SequenceSystem.DiscardFromTopOfDeck),
            new object[] { 5 });
        SequenceSystem.OpponentDiscard opponentDiscard = new SequenceSystem.OpponentDiscard(unit.photonView.ViewID, CardSelector.TypeFilter.All, 2);

        onEnterPlay.AddAbility(discardDeck);
        unit.AttackStack.Add(discardDeck);
        unit.AdvanceStack.Add(discardDeck);
        onDeath.AddAbility(opponentDiscard);
    }
}
