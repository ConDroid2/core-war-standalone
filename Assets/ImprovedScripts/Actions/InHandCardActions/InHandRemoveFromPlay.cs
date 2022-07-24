using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SequenceSystem 
{
    public class InHandRemoveFromPlay : GameAction, NetworkedAction
    {
        CardController controller;
        public InHandRemoveFromPlay(CardController controller)
        {
            this.controller = controller;
            gameObject = controller.gameObject;
        }

        public InHandRemoveFromPlay(InHandRemoveFromPlay template) : base(template)
        {
            
            controller = template.controller;
            gameObject = template.gameObject;
        }

        public override void PerformGameAction()
        {
            if (controller.isMine && (controller.currentState == CardController.CardState.InHand || controller.currentState == CardController.CardState.InHandNoCount || controller.currentState == CardController.CardState.Waiting))
            {
                if (controller.cardData.script != "")
                {
                    if(controller.GetComponent<CardScript>() != null)
                        controller.GetComponent<CardScript>().InHandDeath();
                }
                Player.Instance.hand.RemoveCard(controller);
                GameObject.Destroy(gameObject);
            }
            else if (!controller.isMine)
            {
                Enemy.Instance.hand.RemoveCard(controller);
                GameObject.Destroy(gameObject);
            }

            OnEnd();
        }
    }
}
