using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FleetingShadow : CardScript
{
    public override void InHandSetUp()
    {
        CardController card = GetComponent<CardController>();
        PlayInsteadOfDiscard play = new PlayInsteadOfDiscard(card);
        conditions.Add(play);
    }
}
