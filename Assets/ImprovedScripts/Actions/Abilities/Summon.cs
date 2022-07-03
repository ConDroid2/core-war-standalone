using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SequenceSystem 
{
    public class Summon : Ability
    {
        int amount;
        string card;
        GameObject controller;
        public Summon(int amount, string card, GameObject controller)
        {
            this.amount = amount;
            this.card = card;
            this.controller = controller;
        }

        public Summon(Summon template)
        {
            amount = template.amount;
            card = template.card;
            controller = template.controller;
        }

        public override void PerformGameAction()
        {
            for (int i = 0; i < amount; i++)
            {
                UnitController cardController = InPlayCardPool.Instance.Get(controller.transform.position);
                cardController.turnPlayed = MatchManager.Instance.currentTurn;
                object[] rpcData = { card };
                cardController.photonView.RPC("SetUpCardFromName", Photon.Pun.RpcTarget.All, rpcData);
                cardController.gameObject.SetActive(true);

                MainSequenceManager.Instance.Add(cardController.unitAdvance);

                MainSequenceManager.Instance.Add(cardController.unitPlay);
            }

            OnEnd();
        }
    }
}
