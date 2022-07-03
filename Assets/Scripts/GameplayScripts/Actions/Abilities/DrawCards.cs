using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawCards : Ability
{

    private int amount;

    public override IEnumerator ActionCoroutine()
    {
        ActionSequencer sequence = new ActionSequencer();

        for (int i = 0; i < amount; i++)
        {
            sequence.AddGameAction(Player.Instance.GetComponent<PlayerDraw>());

            yield return StartCoroutine(sequence.RunSequence());
        }

        OnEnd();
    }

    public void Initialize(int amount)
    {
        this.amount = amount;
    }

    public override void SetUpAbility(string[] str_args) 
    {
       amount = str_args[0].ConvertToInt();
    }
}
