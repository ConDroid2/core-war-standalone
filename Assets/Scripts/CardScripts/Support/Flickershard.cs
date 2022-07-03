using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flickershard : CardScript
{
    public override void InPlaySetUp()
    {
        SupportController support = GetComponent<SupportController>();

        OnTurnEnd turnEnd = new OnTurnEnd(support);
        conditions.Add(turnEnd);

        SequenceSystem.TargetMultiple targetMultiple =
            new SequenceSystem.TargetMultiple(
                support,
                zoneFilter: CardSelector.ZoneFilter.MyZones,
                typeFilter: CardSelector.TypeFilter.Unit);
        targetMultiple.SetAmountOfTargets(100);
        targetMultiple.SetTargetMode(SequenceSystem.TargetMultiple.TargetMode.UpTo);

        SequenceSystem.ReturnTargetToHand returnUnit = new SequenceSystem.ReturnTargetToHand();
        targetMultiple.AddAbility(returnUnit);

        turnEnd.AddAbility(targetMultiple);
    }
}
