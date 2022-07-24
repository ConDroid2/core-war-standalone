using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SequenceSystem 
{
    public class InPlayReturnToHand : GameAction
    {
        InPlayCardController controller;
        public InPlayReturnToHand(InPlayCardController controller)
        {
            this.controller = controller;
            gameObject = controller.gameObject;
        }

        public InPlayReturnToHand(InPlayReturnToHand template)
        {
            controller = template.controller;
            gameObject = template.gameObject;
        }

        public override void PerformGameAction()
        {
            if (controller.isMine)
            {
                CardController card = CardPool.Instance.Get();

                card.gameObject.SetActive(true);
                card.cardData = new Card(controller.cardData);
                card.transform.position = controller.transform.position;
                card.initialPos = controller.transform.position;

                card.SetUpCardFromJson(controller.cardData.GetJson());

                // Get put in hand action
                MainSequenceManager.Instance.AddNext(card.putInHand);
                MainSequenceManager.Instance.AddNext(controller.discard);               
            }

            OnEnd();
        }
    }
}
