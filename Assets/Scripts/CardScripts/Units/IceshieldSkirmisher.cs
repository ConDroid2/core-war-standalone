using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceshieldSkirmisher : CardScript
{
    public override void InPlaySetUp()
    {
        InPlayCardController card = GetComponent<InPlayCardController>();
        OnTurnEnd onTurnEnd = new OnTurnEnd(card);
        conditions.Add(onTurnEnd);

        SequenceSystem.AddKeyword addKeyword = new SequenceSystem.AddKeyword("Icetouch", card);

        onTurnEnd.AddAbility(addKeyword);

        OnTurnStart onTurnStart = new OnTurnStart(card);
        conditions.Add(onTurnStart);

        SequenceSystem.RemoveKeyword removeKeyword = new SequenceSystem.RemoveKeyword("Icetouch", card as UnitController);

        onTurnStart.AddAbility(removeKeyword);
    }
}
