﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SequenceSystem 
{
    public class BuffInPlay : TargetedAbility
    {
        int damageIncrease;
        int resilienceIncrease;
        UnitController controller;
        public BuffInPlay(int damageIncrease, int resilienceIncrease, UnitController controller = null)
        {
            this.damageIncrease = damageIncrease;
            this.resilienceIncrease = resilienceIncrease;
            this.controller = controller;

            if(controller != null)
                SetTarget(controller.gameObject);
        }

        public BuffInPlay(BuffInPlay template)
        {
            damageIncrease = template.damageIncrease;
            resilienceIncrease = template.resilienceIncrease;
            controller = template.controller;
            target = template.target;
        }

        public override void PerformGameAction()
        {
            UnitController card = target.GetComponent<UnitController>();

            object[] resilienceData = { resilienceIncrease };
            card.photonView.RPC("IncreaseMaxResilience", Photon.Pun.RpcTarget.All, resilienceData);

            object[] damageData = { card.cardData.currentStrength + damageIncrease };
            card.photonView.RPC("ChangeDamage", Photon.Pun.RpcTarget.All, damageData);


            OnEnd();
        }
    }
}
