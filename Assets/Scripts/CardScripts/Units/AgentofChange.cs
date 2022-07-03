using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class AgentofChange : CardScript
{
    public override void InPlaySetUp()
    {
        UnitController unit = GetComponent<UnitController>();
        OnEnterPlay onPlay = new OnEnterPlay(unit);
        conditions.Add(onPlay);

        SequenceSystem.TargetAll targetAll = new SequenceSystem.TargetAll(unit, handFilter: CardSelector.HandFilter.MyHand);
        SequenceSystem.DiscardCard discard = new SequenceSystem.DiscardCard();
        targetAll.AddAbility(discard);

        SequenceSystem.DrawCards draw = new SequenceSystem.DrawCards(3);

        onPlay.AddAbility(targetAll);
        onPlay.AddAbility(draw);
    }
}
