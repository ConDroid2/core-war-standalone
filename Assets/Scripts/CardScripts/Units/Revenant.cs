using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Revenant : CardScript
{
    public override void InPlaySetUp()
    {
        UnitController unit = GetComponent<UnitController>();
        OnDeath onDeath = new OnDeath(unit);
        conditions.Add(onDeath);
        OnNightmareDiscarded nightmareDiscard = new OnNightmareDiscarded(unit.photonView.IsMine);
        conditions.Add(nightmareDiscard);

        SequenceSystem.AddActionToOpponent addAction = new SequenceSystem.AddActionToOpponent(typeof(SequenceSystem.AddCardsToHand), new object[] { 1, "Nightmare"});
        onDeath.AddAbility(addAction);

        SequenceSystem.ResurrectMe resurrectMe = new SequenceSystem.ResurrectMe(unit);
        nightmareDiscard.AddAbility(resurrectMe);
    }
}
