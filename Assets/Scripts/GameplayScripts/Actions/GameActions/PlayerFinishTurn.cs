using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFinishTurn : GameAction
{
    public override IEnumerator ActionCoroutine()
    {
        object[] eventContent = { };
        NetworkEventSender.Instance.SendEvent(eventContent, NetworkingUtilities.eventDictionary["TurnEnded"]);

        return null;
    }
}
