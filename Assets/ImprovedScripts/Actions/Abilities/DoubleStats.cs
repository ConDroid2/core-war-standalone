using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SequenceSystem 
{
    public class DoubleStats : TargetedAbility
    {
        private UnitController unit;
        public DoubleStats(UnitController unit)
        {
            this.unit = unit;
        }

        public DoubleStats(DoubleStats template)
        {
            unit = template.unit;
        }

        public override void PerformGameAction()
        {
            int maxResilienceIncrease = Mathf.Abs((unit.cardData.currentResilience * 2) - unit.cardData.currentResilience);
            int newDamage = unit.cardData.currentStrength * 2;

            object[] resilienceRPCData = { maxResilienceIncrease };
            unit.photonView.RPC("IncreaseMaxResilience", Photon.Pun.RpcTarget.All, resilienceRPCData);
            object[] damageRPCData = { newDamage };
            unit.photonView.RPC("ChangeDamage", Photon.Pun.RpcTarget.All, damageRPCData);

            OnEnd();
        }
    }
}
