using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChooseRandomAbility : Ability
{
    List<Ability> abilities = new List<Ability>();

    public void AddAbility(Ability ability)
    {
        abilities.Add(ability);
    }

    public override IEnumerator ActionCoroutine()
    {
        int randomIndex = Random.Range(0, abilities.Count);

        ActionSequencer sequence = new ActionSequencer();
        sequence.AddGameAction(abilities[randomIndex]);

        yield return StartCoroutine(sequence.RunSequence());

        OnEnd();
    }
}
