using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;

namespace SequenceSystem 
{
    public class UnitDie : GameAction, NetworkedAction
    {
        UnitController controller;
        public UnitDie(UnitController controller)
        {
            this.controller = controller;
            gameObject = controller.gameObject;
        }

        public UnitDie(UnitDie template)
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

                if (controller.photonView.IsMine)
                {
                    object[] rpcData = { controller.photonView.ViewID };
                    UnderworldManager.Instance.photonView.RPC("AddToUnderworld", Photon.Pun.RpcTarget.All, rpcData);
                }

                OnEnd();
            });
        }
    }
}
