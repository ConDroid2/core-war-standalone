using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MysticOfCalm : CardScript
{
    public override void InPlaySetUp()
    {
        UnitController unit = GetComponent<UnitController>();
        OnEnterPlay onEnterPlay = new OnEnterPlay(unit);
        conditions.Add(onEnterPlay);

        SequenceSystem.AddStatus addStatus = new SequenceSystem.AddStatus("Rooted", MagickManager.Instance.level["Blue"] / 2);

        SequenceSystem.Target targetAbility = new SequenceSystem.Target(unit, zoneFilter: CardSelector.ZoneFilter.All, typeFilter: CardSelector.TypeFilter.Unit);
        targetAbility.abilities.Add(addStatus);
        targetAbility.optional = true;

        onEnterPlay.AddAbility(targetAbility);
    }
}
