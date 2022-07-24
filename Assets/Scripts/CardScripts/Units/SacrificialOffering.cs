using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SacrificialOffering : CardScript
{
    public override void InPlaySetUp()
    {
        UnitController unit = GetComponent<UnitController>();
        OnDeath onDeath = new OnDeath(unit);
        conditions.Add(onDeath);

        //SequenceSystem.OpponentDiscard opponentDiscard = new SequenceSystem.OpponentDiscard(unit.photonView.ViewID, CardSelector.TypeFilter.All, 1);
        //onDeath.AddAbility(opponentDiscard);
    }
}
