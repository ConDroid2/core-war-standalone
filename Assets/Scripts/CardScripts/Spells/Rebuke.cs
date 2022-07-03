using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rebuke : CardScript
{
    public override void InHandSetUp()
    {
        CardController controller = GetComponent<CardController>();
        OnPlay onPlay = new OnPlay(controller);
        conditions.Add(onPlay);

        SequenceSystem.Target target = new SequenceSystem.Target(controller, zoneFilter: CardSelector.ZoneFilter.All);
        SequenceSystem.ReturnTargetToHand returnToHand = new SequenceSystem.ReturnTargetToHand();
        target.AddAbility(returnToHand);

        SequenceSystem.DrawCards draw = new SequenceSystem.DrawCards(1);

        onPlay.AddAbility(target);
        onPlay.AddAbility(draw);
    }
}
