using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NightHag : CardScript
{
    public override void InPlaySetUp()
    {
        UnitController unit = GetComponent<UnitController>();
        OnEnterPlay onPlay = new OnEnterPlay(unit);
        conditions.Add(onPlay);

        SequenceSystem.AddActionToOpponent addNightmares =
            new SequenceSystem.AddActionToOpponent(typeof(SequenceSystem.AddCardsToHand),
            new object[] { 2, "Nightmare" });

        //SequenceSystem.OpponentDiscard opponentDiscard = new SequenceSystem.OpponentDiscard(unit.photonView.ViewID, CardSelector.TypeFilter.All, 2);

        //onPlay.AddAbility(addNightmares);
        //onPlay.AddAbility(opponentDiscard);
    }
}
