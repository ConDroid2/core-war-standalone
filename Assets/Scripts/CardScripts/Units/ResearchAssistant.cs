using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResearchAssistant : CardScript
{
    public override void InPlaySetUp()
    {
        UnitController unit = GetComponent<UnitController>();
        OnEnterPlay onPlay = new OnEnterPlay(unit);
        conditions.Add(onPlay);

        SequenceSystem.DrawCards draw = new SequenceSystem.DrawCards(3);
        onPlay.AddAbility(draw);

        SequenceSystem.TargetMultiple target =
            new SequenceSystem.TargetMultiple(
                unit,
                handFilter: CardSelector.HandFilter.MyHand);
        target.SetAmountOfTargets(2);
        target.SetTargetMode(SequenceSystem.TargetMultiple.TargetMode.ExactAmount);

        SequenceSystem.ShuffleCardIntoDeck shuffleIntoDeck = new SequenceSystem.ShuffleCardIntoDeck();
        target.AddAbility(shuffleIntoDeck);

        onPlay.AddAbility(target);
    }
}
