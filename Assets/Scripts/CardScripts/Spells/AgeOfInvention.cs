using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgeOfInvention : CardScript
{
    public override void InHandSetUp()
    {
        OnPlay onPlay = new OnPlay(GetComponent<CardController>());
        conditions.Add(onPlay);       

        OnSpellCast onSpell = new OnSpellCast();
        SequenceSystem.DrawCards draw = new SequenceSystem.DrawCards(1);
        onSpell.AddAbility(draw);
        SequenceSystem.ConditionalOngoingEffect ongoingEffect = new SequenceSystem.ConditionalOngoingEffect(SequenceSystem.OngoingEffectWrapper.WhenToEnd.TurnEnd);
        ongoingEffect.AddCondition(onSpell);

        SequenceSystem.InitiateOngoingEffect initiate = new SequenceSystem.InitiateOngoingEffect(ongoingEffect);


        onPlay.AddAbility(initiate);
    }
}
