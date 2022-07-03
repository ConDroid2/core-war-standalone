using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetedTransformation : TargetedAbility
{
    Transformation transformation;
    private void Awake()
    {
        transformation = gameObject.AddComponent<Transformation>();
    }

    public override IEnumerator ActionCoroutine()
    {
        string transformInto = target.GetComponent<InPlayCardController>().cardData.name;

        transformation.Initialize(transformInto);

        ActionSequencer sequence = new ActionSequencer();

        sequence.AddGameAction(transformation);
        yield return StartCoroutine(sequence.RunSequence());

        OnEnd();
    }
}
