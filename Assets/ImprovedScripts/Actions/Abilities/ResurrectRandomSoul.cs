using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SequenceSystem 
{
    public class ResurrectRandomSoul : Ability
    {
        public ResurrectRandomSoul()
        {
            
        }

        public ResurrectRandomSoul(ResurrectRandomSoul template)
        {
            
        }

        public override void PerformGameAction()
        {
            List<InPlayCardController> souls = new List<InPlayCardController>(UnderworldManager.Instance.souls);
            souls.RemoveAll((card) => { return !card.photonView.IsMine; });

            if (souls.Count > 0)
            {
                int randomIndex = Random.Range(0, souls.Count);
                InPlayCardController soulToResurrect = souls[randomIndex];

                UnitController inPlayCard = InPlayCardPool.Instance.Get();
                inPlayCard.turnPlayed = MatchManager.Instance.currentTurn;
                inPlayCard.photonView.RPC("SetUpCardFromName", Photon.Pun.RpcTarget.All, soulToResurrect.cardData.name);
                inPlayCard.gameObject.SetActive(true);

                object[] rpcData = { soulToResurrect.photonView.ViewID };
                UnderworldManager.Instance.photonView.RPC("ExorciseFromUnderworld", Photon.Pun.RpcTarget.All, rpcData);

                MainSequenceManager.Instance.Add(inPlayCard.unitAdvance);
                MainSequenceManager.Instance.Add(inPlayCard.unitPlay);
            }

            OnEnd();
        }
    }
}
