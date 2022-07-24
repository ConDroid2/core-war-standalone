using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TheFrostshroud : CardScript
{
    public override void InHandSetUp()
    {
        CardController card = GetComponent<CardController>();
        OnPlay onPlay = new OnPlay(card);
        conditions.Add(onPlay);

        SequenceSystem.ConditionalOngoingEffect ongoingEffect = new SequenceSystem.ConditionalOngoingEffect();
        //
        OnEnemyUnitEnteredPlay onEnemyUnitEnteredPlay = new OnEnemyUnitEnteredPlay(card.isMine);
        SequenceSystem.AddStatus addStatus = new SequenceSystem.AddStatus("Rooted", 2);
        onEnemyUnitEnteredPlay.AddAbility(addStatus);
        //
        ongoingEffect.AddCondition(onEnemyUnitEnteredPlay);
        onPlay.AddAbility(new SequenceSystem.InitiateOngoingEffect(ongoingEffect));

        SequenceSystem.ReplaceAllCopiesOf replace = new SequenceSystem.ReplaceAllCopiesOf("The Frostshroud", "Frostshroud Titan");
        onPlay.AddAbility(replace);
    }
}
