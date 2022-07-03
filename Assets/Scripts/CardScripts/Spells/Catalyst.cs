using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Catalyst : CardScript
{
    public override void InHandSetUp()
    {
        CardController card = GetComponent<CardController>();
        OnPlay onPlay = new OnPlay(card);
        conditions.Add(onPlay);

        SequenceSystem.TargetMultiple target = new SequenceSystem.TargetMultiple(card, CardSelector.HandFilter.MyHand);
        target.setActionPipeData = (thisAction) => {
            MainSequenceManager.Instance.mainSequence.SetPipeData(
            new object[] { (thisAction as SequenceSystem.TargetMultiple).targets.Count }); 
        };

        SequenceSystem.DiscardCard discard = new SequenceSystem.DiscardCard();
        target.AddAbility(discard);

        SequenceSystem.IncreaseCurrentMagick increaseMagick = new SequenceSystem.IncreaseCurrentMagick(2, "Red");
        target.AddAbility(increaseMagick);

        onPlay.AddAbility(target);
        onPlay.AddAbility(increaseMagick);
    }
}
