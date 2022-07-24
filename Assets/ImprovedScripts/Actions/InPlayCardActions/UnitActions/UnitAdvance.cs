using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace SequenceSystem 
{
    public class UnitAdvance : GameAction, NetworkedAction
    {
        UnitController controller;
        public UnitAdvance(UnitController controller)
        {
            this.controller = controller;
            gameObject = controller.gameObject;
        }

        public UnitAdvance(UnitAdvance template)
        {
            controller = template.controller;
            gameObject = controller.gameObject;
            
        }

        public override void PerformGameAction()
        {
            Vector3 newPos = controller.transform.position;
            controller.interactable = false;
            if (controller.currentZone == null)
            {
                if (controller.isMine)
                {
                    newPos = Player.Instance.zones[0].AddCard(controller);

                    controller.currentZone = Player.Instance.zones[0];
                }
                else
                {
                    newPos = Enemy.Instance.zones[0].AddCard(controller);

                    controller.currentZone = Enemy.Instance.zones[0];
                }
            }
            else if (controller.currentZone.nextZone != Core.Instance || (controller.isMine && !Core.Instance.lockedForMe) || (!controller.isMine && !Core.Instance.lockedForOpponent))
            {

                controller.currentZone.RemoveCard(controller);
                newPos = controller.currentZone.nextZone.AddCard(controller);
                controller.currentZone = controller.currentZone.nextZone;
                controller.SetAttackActionState(UnitController.ActionState.Acted);
                controller.SetMoveActionState(UnitController.ActionState.Acted);
            }

            controller.transform.DOMoveZ(controller.transform.position.z - 4, 0.3f).OnComplete(() =>
            {
                controller.transform.DOMove(newPos, 0.2f).OnComplete(OnEnd);
                controller.interactable = true;
                controller.currentZoneNum++;
            });
        }
    }
}
