using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemonOfDilemmas : CardScript
{
    public override void InPlaySetUp()
    {
        UnitController unit = GetComponent<UnitController>();
        OnEnterCore onEnterCore = new OnEnterCore(unit);
        conditions.Add(onEnterCore);
        OnDeath onDeath = new OnDeath(unit);
        conditions.Add(onDeath);

        SequenceSystem.TargetMultiple targetMultiple = new SequenceSystem.TargetMultiple(unit, handFilter: CardSelector.HandFilter.MyHand);
        targetMultiple.SetAmountOfTargets(2);
        targetMultiple.SetTargetMode(SequenceSystem.TargetMultiple.TargetMode.ExactAmount);
        SequenceSystem.DiscardCard meDiscard = new SequenceSystem.DiscardCard();
        targetMultiple.AddAbility(meDiscard);

        SequenceSystem.OpponentDiscard opponentDiscard = new SequenceSystem.OpponentDiscard(unit.photonView.ViewID, CardSelector.TypeFilter.All, 2);

        onEnterCore.AddAbility(targetMultiple);
        onDeath.AddAbility(opponentDiscard);
    }

}
