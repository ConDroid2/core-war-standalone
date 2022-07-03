using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NimKaderen : CardScript
{
    public override void InPlaySetUp()
    {
        GetComponent<Legendary>().Initialize("Dark Tidings");
        UnitController unit = GetComponent<UnitController>();
        OnEnterPlay onEnter = new OnEnterPlay(unit);
        conditions.Add(onEnter);
        OnDeath onDeath = new OnDeath(unit);
        conditions.Add(onDeath);

        SequenceSystem.AddActionToOpponent addDraw =
            new SequenceSystem.AddActionToOpponent(
                typeof(SequenceSystem.DrawCards),
                new object[] { 2 });
        SequenceSystem.DrawCards draw = new SequenceSystem.DrawCards(2);
        SequenceSystem.OpponentDiscard discard = new SequenceSystem.OpponentDiscard(unit.photonView.ViewID, CardSelector.TypeFilter.All, 2);

        onEnter.AddAbility(addDraw);
        onEnter.AddAbility(draw);
        onDeath.AddAbility(discard);
    }
}
