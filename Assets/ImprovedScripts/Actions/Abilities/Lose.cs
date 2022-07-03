using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SequenceSystem 
{
    public class Lose : GameAction
    {
        private bool sendEvent;
        public Lose(bool sendEvent)
        {
            this.sendEvent = sendEvent;
        }

        public Lose(Lose template)
        {
            sendEvent = template.sendEvent;
        }

        public override void PerformGameAction()
        {
            MatchManager.Instance.YouLose(sendEvent);
            MainSequenceManager.Instance.mainSequence.ClearSequencer();
            OnEnd();
        }
    }
}
