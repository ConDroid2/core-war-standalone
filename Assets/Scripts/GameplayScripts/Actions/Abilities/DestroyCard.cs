using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyCard : TargetedAbility
{    
    public override IEnumerator ActionCoroutine()
    {
        ActionSequencer actionSequence = new ActionSequencer();
        actionSequence.AddGameAction(target.GetComponent<Die>());

        yield return StartCoroutine(actionSequence.RunSequence());

        OnEnd();
    }
}
