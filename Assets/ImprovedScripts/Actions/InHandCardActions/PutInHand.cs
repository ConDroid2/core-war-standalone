using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SequenceSystem;

namespace SequenceSystem
{ 
    public class PutInHand : GameAction
    {
        private CardController card;

        public PutInHand(CardController card, GameObject gameObject)
        {
            base.gameObject = gameObject;
            this.card = card;
        }

        public PutInHand(PutInHand template)
        {
            gameObject = template.gameObject;
            card = template.card;
        }

        public override void PerformGameAction()
        {
            if (card.isMine)
            {
                Player.Instance.hand.AddCard(card);
            }
            else
            {
                Enemy.Instance.hand.AddCard(card);
            }

            OnEnd();
        }
    }
}
