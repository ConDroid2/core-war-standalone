using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShepherdOfSouls : CardScript
{
    public override void InPlaySetUp()
    {
        UnitController unit = GetComponent<UnitController>();
        OnEnterPlay onEnter = new OnEnterPlay(unit);
        conditions.Add(onEnter);

        SequenceSystem.ResurrectRandomSoul resurrect = new SequenceSystem.ResurrectRandomSoul();
        onEnter.AddAbility(resurrect);
    }
}
