using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SequenceSystem 
{
    public class OpponentDiscard : Ability
    {
        int photonView;
        CardSelector.TypeFilter typeFilter;
        int amount;
        public OpponentDiscard(int photonView, CardSelector.TypeFilter typeFilter, int amount)
        {
            this.photonView = photonView;
            this.typeFilter = typeFilter;
            this.amount = amount;
        }

        public OpponentDiscard(OpponentDiscard template)
        {
            photonView = template.photonView;
            typeFilter = template.typeFilter;
            amount = template.amount;
        }

        public override void PerformGameAction()
        {
            Debug.Log("Forcing opponent to discard");
            WaitForDoneEvent wait = new WaitForDoneEvent();
            MainSequenceManager.Instance.AddNext(wait);
            //object[] rpcData = { photonView, amount, typeFilter };
            //MainSequenceManager.Instance.photonView.RPC("MakeOpponentDiscard", Photon.Pun.RpcTarget.Others, rpcData);

            OnEnd();
        }
    }
}
