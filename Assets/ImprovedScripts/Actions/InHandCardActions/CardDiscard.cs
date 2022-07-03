using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SequenceSystem 
{
    public class CardDiscard : GameAction, NetworkedAction
    {
        CardController cardController;

        public CardDiscard(CardController card)
        {
            cardController = card;
            gameObject = card.gameObject;
        }

        public CardDiscard(CardDiscard template)
        {
            cardController = template.cardController;
            gameObject = template.gameObject;
        }

        public override void PerformGameAction()
        {
            if (cardController.photonView.IsMine && (cardController.currentState == CardController.CardState.InHand || cardController.currentState == CardController.CardState.InHandNoCount))
            {
                Player.Instance.hand.RemoveCard(cardController);
            }
            else if (!cardController.photonView.IsMine)
            {
                Enemy.Instance.hand.RemoveCard(cardController);
            }

            if (cardController.cardData.type == CardUtilities.Type.Spell && cardController.cardData.script != "")
            {
                cardController.GetComponent<CardScript>().InHandDeath();
            }

            OnEnd();

            GameObject.Destroy(gameObject);
        }
    }
}
