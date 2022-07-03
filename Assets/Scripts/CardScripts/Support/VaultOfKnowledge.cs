using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VaultOfKnowledge : CardScript
{
    public override void InPlaySetUp()
    {
        SupportController support = GetComponent<SupportController>();

        OnEnterPlay onEnterPlay = new OnEnterPlay(support);
        conditions.Add(onEnterPlay);
        OnRemovedFromPlay onRemovedFromPlay = new OnRemovedFromPlay(support);
        conditions.Add(onRemovedFromPlay);

        SequenceSystem.ChangeHandSize increaseHandSize = new SequenceSystem.ChangeHandSize(1);
        SequenceSystem.DrawCards drawCards = new SequenceSystem.DrawCards(1);
        SequenceSystem.ChangeHandSize decreaseHandSize = new SequenceSystem.ChangeHandSize(-1);

        onEnterPlay.AddAbility(increaseHandSize);
        onEnterPlay.AddAbility(drawCards);
        onRemovedFromPlay.AddAbility(decreaseHandSize);
    }
}
