using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReverseTime : CardScript
{
    public override void InHandSetUp()
    {
        CardController card = GetComponent<CardController>();
        OnPlay onPlay = new OnPlay(card);
        conditions.Add(onPlay);

        SequenceSystem.TargetAll target = new SequenceSystem.TargetAll(card, handFilter: CardSelector.HandFilter.All);
        SequenceSystem.DiscardCard discard = new SequenceSystem.DiscardCard();
        target.AddAbility(discard);

        SequenceSystem.DrawCards playerDraw = new SequenceSystem.DrawCards(7);
        SequenceSystem.AddActionToOpponent enemyDraw =
            new SequenceSystem.AddActionToOpponent(
                typeof(SequenceSystem.DrawCards),
                new object[] { 7 });

        SequenceSystem.ResetLevels playerReset = new SequenceSystem.ResetLevels();
        SequenceSystem.AddActionToOpponent enemyReset =
            new SequenceSystem.AddActionToOpponent(
                typeof(SequenceSystem.ResetLevels),
                new object[] { });

        SequenceSystem.IncreaseLevel increaseLevel = new SequenceSystem.IncreaseLevel(amount: 1);
        SequenceSystem.RefillMagick refill = new SequenceSystem.RefillMagick();

        onPlay.AddAbility(target);
        onPlay.AddAbility(playerDraw);
        onPlay.AddAbility(enemyDraw);
        onPlay.AddAbility(playerReset);
        onPlay.AddAbility(enemyReset);
        onPlay.AddAbility(increaseLevel);
        onPlay.AddAbility(refill);
    }
}
