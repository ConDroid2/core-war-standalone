using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SequenceSystem 
{
    public class Heal : TargetedAbility
    {
        public Heal()
        {
            
        }

        public Heal(Heal template)
        {
            target = template.target;
        }

        public override void PerformGameAction()
        {
            UnitController card = target.GetComponent<UnitController>();
            object[] rpcData = { card.cardData.maxResilience };
            card.photonView.RPC("ChangeResilience", Photon.Pun.RpcTarget.All, rpcData);
            // card.ChangeResilience(card.cardData.maxResilience);

            OnEnd();
        }
    }
}
