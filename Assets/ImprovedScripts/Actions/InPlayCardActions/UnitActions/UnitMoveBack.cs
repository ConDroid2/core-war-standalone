using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace SequenceSystem 
{
    public class UnitMoveBack : GameAction, NetworkedAction
    {
        UnitController controller;
        public UnitMoveBack(UnitController controller)
        {
            this.controller = controller;
            gameObject = controller.gameObject;
        }

        public UnitMoveBack(UnitMoveBack template)
        {
            controller = template.controller;
            gameObject = template.gameObject;
        }

        public override void PerformGameAction()
        {
            Vector3 newPos = controller.transform.position;
            if (controller.currentZone != null)
            {
                controller.currentZone.RemoveCard(controller);
                if (controller.currentZone.prevZone != null)
                {
                    newPos = controller.currentZone.prevZone.AddCard(controller);
                    controller.currentZone = controller.currentZone.prevZone;

                    controller.transform.DOMove(newPos, 0.2f).OnComplete(() => {
                        OnEnd();
                        controller.currentZoneNum--;
                    });
                }               
            }
            else
            {
                OnEnd();
            }
        }
    }
}
