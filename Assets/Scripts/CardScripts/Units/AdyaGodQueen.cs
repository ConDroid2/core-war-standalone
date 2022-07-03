using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdyaGodQueen : CardScript
{
    public override void InPlaySetUp()
    {
        UnitController unit = GetComponent<UnitController>();
        GetComponent<Legendary>().Initialize("Praetorian Guard");

        OnEnterCore onEnterCore = new OnEnterCore(GetComponent<InPlayCardController>());
        conditions.Add(onEnterCore);

        SequenceSystem.Win win = new SequenceSystem.Win(true);
        onEnterCore.AddAbility(win);

        unit.unitDie = unit.returnToHand;
    }
}
