using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SequenceSystem 
{
    public class Transformation : Ability
    {
        string transformInto;
        InPlayCardController controller;
        public Transformation(string transformInto, InPlayCardController controller)
        {
            this.transformInto = transformInto;
            this.controller = controller;
        }

        public Transformation(Transformation template)
        {
            transformInto = template.transformInto;
            controller = template.controller;
        }

        public override void PerformGameAction()
        {
            InPlayCardController newCard = InPlayCardPool.Instance.Get(controller.transform.position);
            newCard.turnPlayed = controller.turnPlayed;
            newCard.photonView.RPC("SetUpCardFromName", Photon.Pun.RpcTarget.All, transformInto);
            newCard.gameObject.SetActive(true);    

            // Put it in this card's zone
            newCard.SetCurrentZoneNum(controller.currentZoneNum);
            MainSequenceManager.Instance.Add(newCard.addToCurrentZone);

            // Set it's action states to the same as this card's
            if (newCard.cardData.type == CardUtilities.Type.Character)
            {
                UnitController newUnit = newCard as UnitController;
                UnitController unit = controller as UnitController;

                newUnit.SetAttackActionState(unit.attackState);
                newUnit.SetMoveActionState(unit.moveState);
                newUnit.actedThisTurn = unit.actedThisTurn;
            }

            MainSequenceManager.Instance.Add(controller.discard);

            OnEnd();
        }
    }
}
