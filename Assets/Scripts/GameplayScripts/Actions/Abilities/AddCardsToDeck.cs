using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddCardsToDeck : Ability
{
    int amount = 0;
    string cardName = "";

    public void Initialize(int amount, string cardName)
    {
        this.amount = amount;
        this.cardName = cardName;
    }

    public override IEnumerator ActionCoroutine()
    {
        for(int i = 0; i < amount; i++)
        {
            Player.Instance.GetDeck().AddCard(cardName);
        }

        OnEnd();

        return null;
    }
}
