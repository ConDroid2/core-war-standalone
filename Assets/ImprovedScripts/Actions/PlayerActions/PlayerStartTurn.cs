using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SequenceSystem 
{
    public class PlayerStartTurn : GameAction
    {
        public PlayerStartTurn() : base() { }

        public PlayerStartTurn(PlayerStartTurn template) { }

        public override void PerformGameAction()
        {
            NotificationManager.Instance.FireNotification("Your Turn");
            MultiUseButton.Instance.buttonTimer.StartTimer(75f);
            MultiUseButton.Instance.SetColorYellow();

            OnEnd();
        }
    }
}
