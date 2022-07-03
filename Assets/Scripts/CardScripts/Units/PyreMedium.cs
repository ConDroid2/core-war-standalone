using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PyreMedium : CardScript
{
    public override void InPlaySetUp()
    {
        UnitController unit = GetComponent<UnitController>();
        OnTurnEnd onTurnEnd = new OnTurnEnd(unit);
        conditions.Add(onTurnEnd);

        SequenceSystem.AddActionToOpponent discardDeck =
            new SequenceSystem.AddActionToOpponent(
                typeof(SequenceSystem.DiscardFromTopOfDeck),
                () => { return new object[] { IgniteManager.Instance.igniteCount }; });

        onTurnEnd.AddAbility(discardDeck);
    }
}
