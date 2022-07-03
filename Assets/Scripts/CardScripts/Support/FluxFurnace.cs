using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FluxFurnace : CardScript
{
    public override void InPlaySetUp()
    {
        SupportController support = GetComponent<SupportController>();

        OnTurnEnd turnEnd = new OnTurnEnd(support);
        conditions.Add(turnEnd);
        OnTurnStart turnStart = new OnTurnStart(support);
        conditions.Add(turnStart);

        SequenceSystem.TargetAll targetAll = new SequenceSystem.TargetAll(support, CardSelector.HandFilter.MyHand);
        SequenceSystem.DiscardCard discard = new SequenceSystem.DiscardCard();
        targetAll.AddAbility(discard);
        SequenceSystem.DiscardFromTopOfDeck discardFromTopOfDeck = new SequenceSystem.DiscardFromTopOfDeck(3);

        turnEnd.AddAbility(targetAll);
        turnEnd.AddAbility(discardFromTopOfDeck);

        SequenceSystem.DrawCards draw = new SequenceSystem.DrawCards(7);
        SequenceSystem.IncreaseCurrentMagick increaseMagick = new SequenceSystem.IncreaseCurrentMagick(1, "Red");

        turnStart.AddAbility(draw);
        turnStart.AddAbility(increaseMagick);
    }
}
