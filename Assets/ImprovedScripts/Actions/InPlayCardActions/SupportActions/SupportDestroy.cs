using UnityEngine;
using DG.Tweening;
using System;

namespace SequenceSystem 
{  
    public class SupportDestroy : GameAction, NetworkedAction
    {
        public event Action<SupportController> OnDestroyEvent;

        SupportController controller;
        public SupportDestroy(SupportController controller)
        {
            this.controller = controller;
            this.gameObject = controller.gameObject;
        }

        public SupportDestroy(SupportDestroy template)
        {
            controller = template.controller;
            gameObject = template.gameObject;
        }

        public override void PerformGameAction()
        {
            controller.InvokeOnRemovedFromPlay();

            controller.transform.DOScale(Vector3.zero, 0.75f);
            controller.transform.DORotate(new Vector3(0, 0, 359), 0.75f).OnComplete(() => {

                if (controller.currentZone != null)
                {
                    controller.currentZone.RemoveCard(controller);
                    controller.currentZone = null;
                }

                if (controller.cardData.script != "")
                {
                    controller.GetComponent<CardScript>().InPlayDeath();
                }

                OnDestroyEvent?.Invoke(controller);

                OnEnd();
            });
        }
    }
}
