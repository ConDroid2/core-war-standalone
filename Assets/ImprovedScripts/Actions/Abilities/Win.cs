using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SequenceSystem 
{
    public class Win : GameAction
    {
        private bool sendEvent;
        public Win(bool sendEvent)
        {
            this.sendEvent = sendEvent;
        }

        public Win(Win template)
        {
            sendEvent = template.sendEvent;
        }

        public override void PerformGameAction()
        {
            MatchManager.Instance.YouWin(sendEvent);
            MainSequenceManager.Instance.mainSequence.ClearSequencer();
            OnEnd();
        }
    }
}
