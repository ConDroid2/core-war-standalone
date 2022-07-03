using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicMirror : CardScript
{
    public override void InPlaySetUp()
    {
        SupportController support = GetComponent<SupportController>();

        OnEnterPlay onEnter = new OnEnterPlay(support);
        conditions.Add(onEnter);

        SequenceSystem.Target target = new SequenceSystem.Target(support, zoneFilter: CardSelector.ZoneFilter.MyZones, typeFilter: CardSelector.TypeFilter.Support);
        SequenceSystem.TargetedTransformation transformation = new SequenceSystem.TargetedTransformation(support);
        target.AddAbility(transformation);
        onEnter.AddAbility(target);
    }
}
