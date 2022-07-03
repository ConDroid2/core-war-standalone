using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MalevolentGeist : CardScript
{
    public override void InPlaySetUp()
    {
        UnitController unit = GetComponent<UnitController>();
        OnDeath onDeath = new OnDeath(unit);
        conditions.Add(onDeath);

        SequenceSystem.AddActionToOpponent addAction = new SequenceSystem.AddActionToOpponent(typeof(SequenceSystem.DiscardFromTopOfDeck), new object[] { 4 });
        onDeath.AddAbility(addAction);
    }
}
