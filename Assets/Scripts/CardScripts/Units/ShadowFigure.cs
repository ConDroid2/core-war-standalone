using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowFigure : CardScript
{
    public override void InPlaySetUp()
    {
        UnitController unit = GetComponent<UnitController>();
        OnTurnEnd turnEnd = new OnTurnEnd(unit);
        conditions.Add(turnEnd);
        //OnNightmareDiscarded nightmareDiscard = new OnNightmareDiscarded(unit.photonView.IsMine);
        //conditions.Add(nightmareDiscard);
    }
}
