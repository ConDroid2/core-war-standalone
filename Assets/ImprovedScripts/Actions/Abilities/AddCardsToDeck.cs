using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SequenceSystem 
{
    public class AddCardsToDeck : Ability
    {
        int amount = 0;
        string cardName = "";
        public AddCardsToDeck(int amount, string cardName)
        {
            this.amount = amount;
            this.cardName = cardName;
        }

        public AddCardsToDeck(AddCardsToDeck template)
        {
            amount = template.amount;
            cardName = template.cardName;
        }

        public override void PerformGameAction()
        {
            for (int i = 0; i < amount; i++)
            {
                Player.Instance.GetDeck().AddCard(cardName);
            }

            OnEnd();
        }
    }
}
