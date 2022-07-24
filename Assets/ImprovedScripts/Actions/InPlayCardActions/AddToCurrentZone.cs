using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace SequenceSystem 
{
    public class AddToCurrentZone : GameAction, NetworkedAction
    {
        InPlayCardController controller;
        public AddToCurrentZone(InPlayCardController controller)
        {
            this.controller = controller;
            gameObject = controller.gameObject;
        }

        public AddToCurrentZone(AddToCurrentZone template)
        {
            controller = template.controller;
            gameObject = template.gameObject;
        }

        public override void PerformGameAction()
        {
            if (controller.currentZoneNum > 0)
            {
                Vector3 newPos = Vector3.zero;
                controller.interactable = false;
                if (controller.isMine)
                {
                    newPos = Player.Instance.zones[controller.currentZoneNum - 1].AddCard(controller);
                    controller.currentZone = Player.Instance.zones[controller.currentZoneNum - 1];
                }
                else
                {
                    newPos = Enemy.Instance.zones[controller.currentZoneNum - 1].AddCard(controller);
                    controller.currentZone = Enemy.Instance.zones[controller.currentZoneNum - 1];
                }

                controller.transform.DOMoveZ(controller.transform.position.z - 4, 0.3f).OnComplete(() =>
                {
                    controller.transform.DOMove(newPos, 0.2f).OnComplete(OnEnd);
                    controller.interactable = true;
                });

            }
            else
            {
                OnEnd();
            }
        }
    }
}
