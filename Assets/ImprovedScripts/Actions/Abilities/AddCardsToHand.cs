using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SequenceSystem 
{
    public class AddCardsToHand : Ability
    {
        int amount = 0;
        string cardName = "";
        public AddCardsToHand(int amount, string cardName)
        {
            this.amount = amount;
            this.cardName = cardName;
        }

        public AddCardsToHand(AddCardsToHand template)
        {
            amount = template.amount;
            cardName = template.cardName;
        }

        public override void PerformGameAction()
        {
            for (int i = 0; i < amount; i++)
            {
                CardController card = CardPool.Instance.Get();

                card.gameObject.SetActive(true);
                card.transform.position = Vector3.zero;
                card.initialPos = Vector3.zero;

                card.SetUpCardFromName(cardName);

                // Get put in hand action
                MainSequenceManager.Instance.AddNext(card.putInHand);
            }

            OnEnd();
        }
    }
}
