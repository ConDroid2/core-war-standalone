using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SequenceSystem 
{
    public class AttackAbility : TargetedAbility
    {
        UnitController controller;
        public AttackAbility(UnitController controller)
        {
            this.controller = controller;
        }

        public AttackAbility(AttackAbility template)
        {
            controller = template.controller;
            target = template.target;
        }

        public override void PerformGameAction()
        {
            UnitController unit = target.GetComponent<UnitController>();
            object[] rpcData = { unit.photonView.ViewID };
            controller.photonView.RPC("SetAttackTarget", Photon.Pun.RpcTarget.All, rpcData);
            MainSequenceManager.Instance.Add(controller.AttackStack);

            OnEnd();
        }
    }
}
