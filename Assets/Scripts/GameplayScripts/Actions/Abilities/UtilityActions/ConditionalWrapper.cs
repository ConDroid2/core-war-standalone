using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConditionalWrapper : Ability
{
    public delegate bool CheckCondition();
    public CheckCondition checkCondition;
    public List<Ability> abilities = new List<Ability>();

    public void Initialize(CheckCondition checkCondition)
    {
        this.checkCondition = checkCondition;
    }

    public override IEnumerator ActionCoroutine()
    {
        if (checkCondition())
        {
            ActionSequencer sequence = new ActionSequencer();
            sequence.AddGameAction(abilities);
            yield return StartCoroutine(sequence.RunSequence());
        }

        OnEnd();
    }
}
