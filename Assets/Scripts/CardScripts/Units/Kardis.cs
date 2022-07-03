using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kardis : CardScript
{
    public override void InPlaySetUp()
    {
        GetComponent<Legendary>().Initialize("Night Terror");

        UnitController unit = GetComponent<UnitController>();
        OnEnterPlay onEnter = new OnEnterPlay(unit);
        conditions.Add(onEnter);
        OnRemovedFromPlay onRemoved = new OnRemovedFromPlay(unit);
        conditions.Add(onRemoved);

        SequenceSystem.OpponentDiscard opponentDiscard = new SequenceSystem.OpponentDiscard(unit.photonView.ViewID, CardSelector.TypeFilter.Spell, 1);
        SequenceSystem.AddActionToOpponent decreaseHandSize =
            new SequenceSystem.AddActionToOpponent(typeof(SequenceSystem.ChangeHandSize),
            new object[] { -1 });
        SequenceSystem.AddActionToOpponent increaseHandSize =
            new SequenceSystem.AddActionToOpponent(typeof(SequenceSystem.ChangeHandSize),
            new object[] { 1 });

        onEnter.AddAbility(opponentDiscard);
        onEnter.AddAbility(decreaseHandSize);
        onRemoved.AddAbility(increaseHandSize);
    }
}
