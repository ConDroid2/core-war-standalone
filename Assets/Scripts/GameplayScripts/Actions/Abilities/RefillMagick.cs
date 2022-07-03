using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RefillMagick : Ability
{
    public override IEnumerator ActionCoroutine()
    {
        ActionSequencer sequence = new ActionSequencer();
        sequence.AddGameAction(Player.Instance.GetComponent<PlayerResetEnergy>());

        yield return StartCoroutine(sequence.RunSequence());

        OnEnd();
    }
}
