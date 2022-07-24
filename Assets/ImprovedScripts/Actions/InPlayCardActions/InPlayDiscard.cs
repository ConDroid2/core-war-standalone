using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace SequenceSystem 
{
    public class InPlayDiscard : GameAction
    {
        InPlayCardController controller;
        public InPlayDiscard(InPlayCardController controller)
        {
            this.controller = controller;
            gameObject = controller.gameObject;
        }

        public InPlayDiscard(InPlayDiscard template)
        {
            controller = template.controller;
            gameObject = controller.gameObject;
        }

        public override void PerformGameAction()
        {
            if (controller.currentZone != null)
            {
                controller.currentZone.RemoveCard(controller);
                controller.currentZone = null;
            }

            controller.transform.DOScale(Vector3.zero, 0.5f).OnComplete(() => { 

                controller.InvokeOnRemovedFromPlay();

                if (controller.cardData.script != "")
                {
                    CardScript script = controller.GetComponent<CardScript>();
                    script.InPlayDeath();
                    script.OnExorcise();
                }

                OnEnd();

                GameObject.Destroy(gameObject);
            });
        }
    }
}
