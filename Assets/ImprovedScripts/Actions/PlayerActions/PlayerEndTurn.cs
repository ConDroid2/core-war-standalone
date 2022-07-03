using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace SequenceSystem
{
    public class PlayerEndTurn : GameAction
    {
        public PlayerEndTurn() : base() { }
        public PlayerEndTurn(PlayerEndTurn template) { }

        public override void PerformGameAction()
        {
            NotificationManager.Instance.FireNotification("Opponent's Turn");
            MultiUseButton.Instance.buttonTimer.EndTimer();
            MultiUseButton.Instance.SetColorYellow();
            object[] eventContent = { };
            NetworkEventSender.Instance.SendEvent(eventContent, NetworkingUtilities.eventDictionary["TurnEnded"]);

            OnEnd();
        }
    }
}
