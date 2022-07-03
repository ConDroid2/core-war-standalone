using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuffcapBed : CardScript
{
    public override void InPlaySetUp()
    {
        UnitController card = GetComponent<UnitController>();
        OnTurnEnd onTurnEnd = new OnTurnEnd(card);
        conditions.Add(onTurnEnd);

        SequenceSystem.Summon summon = new SequenceSystem.Summon(1, "Sporeling", card.gameObject);

        onTurnEnd.AddAbility(summon);
    }
}
