using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScavengingGhoul : CardScript
{
    public override void InPlaySetUp()
    {
        UnitController unit = GetComponent<UnitController>();
        OnTurnEnd turnEnd = new OnTurnEnd(unit);
        conditions.Add(turnEnd);

        SequenceSystem.AddActionToOpponent addAction = new SequenceSystem.AddActionToOpponent(typeof(SequenceSystem.DiscardFromTopOfDeck), new object[] { 1 });
        turnEnd.AddAbility(addAction);
    }
}
