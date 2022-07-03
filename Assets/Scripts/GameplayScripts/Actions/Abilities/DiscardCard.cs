using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiscardCard : TargetedAbility
{
    public override IEnumerator ActionCoroutine()
    {
        CardDiscard discard = target.GetComponent<CardDiscard>();

        ActionSequencer sequence = new ActionSequencer();

        sequence.AddGameAction(discard);

        yield return StartCoroutine(sequence.RunSequence());

        OnEnd();
    }
}
