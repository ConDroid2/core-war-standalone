using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JudgementDay : CardScript
{
    public override void InHandSetUp()
    {
        CardController controller = GetComponent<CardController>();
        OnPlay onPlay = new OnPlay(controller);
        conditions.Add(onPlay);

        SequenceSystem.TargetAll targetAll = new SequenceSystem.TargetAll(controller, zoneFilter: CardSelector.ZoneFilter.All);
        SequenceSystem.ReturnTargetToHand returnToHand = new SequenceSystem.ReturnTargetToHand();
        targetAll.AddAbility(returnToHand);

        SequenceSystem.ExorciseSouls exorcise = new SequenceSystem.ExorciseSouls();

        onPlay.AddAbility(targetAll);
        onPlay.AddAbility(exorcise);
    }
}
