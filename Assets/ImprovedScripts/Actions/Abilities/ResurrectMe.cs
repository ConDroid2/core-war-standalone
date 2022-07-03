using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SequenceSystem 
{
    public class ResurrectMe : Ability
    {
        UnitController unit;
        public ResurrectMe(UnitController unit)
        {
            this.unit = unit;
        }

        public ResurrectMe(ResurrectMe template)
        {
            unit = template.unit;
        }

        public override void PerformGameAction()
        {
            if (UnderworldManager.Instance.souls.Contains(unit))
            {
                UnitController inPlayCard = InPlayCardPool.Instance.Get();
                inPlayCard.turnPlayed = MatchManager.Instance.currentTurn;
                inPlayCard.photonView.RPC("SetUpCardFromName", Photon.Pun.RpcTarget.All, unit.cardData.name);
                inPlayCard.gameObject.SetActive(true);

                object[] rpcData = { unit.photonView.ViewID };
                UnderworldManager.Instance.photonView.RPC("ExorciseFromUnderworld", Photon.Pun.RpcTarget.All, rpcData);

                MainSequenceManager.Instance.Add(inPlayCard.unitAdvance);
                MainSequenceManager.Instance.Add(inPlayCard.unitPlay);
            }

            OnEnd();
        }
    }
}
