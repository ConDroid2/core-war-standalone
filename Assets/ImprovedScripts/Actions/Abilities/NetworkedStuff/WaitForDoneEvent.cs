using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SequenceSystem 
{
    public class WaitForDoneEvent : GameAction
    {
        public WaitForDoneEvent()
        {
            
        }

        public WaitForDoneEvent(WaitForDoneEvent template)
        {
            
        }

        public override void PerformGameAction()
        {
            NetworkEventReceiver.OnNetworkEvent += HandleDone;
        }

        public void HandleDone()
        {
            NetworkEventReceiver.OnNetworkEvent -= HandleDone;
            OnEnd();
        }
    }
}
