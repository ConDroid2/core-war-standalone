using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SequenceSystem 
{
    public class SendDoneEvent : GameAction
    {
        public SendDoneEvent()
        {
            
        }

        public SendDoneEvent(SendDoneEvent template)
        {
            
        }

        public override void PerformGameAction()
        {
            NetworkEventSender.Instance.SendEvent(new object[] { }, NetworkingUtilities.eventDictionary["DoneWaitingForOpponent"]);
            OnEnd();
        }
    }
}
