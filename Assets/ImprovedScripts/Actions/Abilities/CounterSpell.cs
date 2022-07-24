using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SequenceSystem 
{
    public class CounterSpell : Ability
    {
        public CounterSpell()
        {
            
        }

        public CounterSpell(CounterSpell template)
        {
            //object[] rpcData = { };
            //MainSequenceManager.Instance.photonView.RPC("InterruptCurrentAction", Photon.Pun.RpcTarget.All, rpcData);
        }

        public override void PerformGameAction()
        {
            //Debug.Log("Countering Spell");
            //MultiUseButton.Instance.SetButtonFunction(Counter);
            //MultiUseButton.Instance.SetButtonText("Counter");
            OnEnd();
        }

        public void Counter()
        {
            //object[] rpcData = { };
            //MainSequenceManager.Instance.photonView.RPC("InterruptCurrentAction", Photon.Pun.RpcTarget.All, rpcData);
            //MultiUseButton.Instance.BackToDefault();
            //MultiUseButton.Instance.SetInteractable(false);
            OnEnd();
        }
    }
}
