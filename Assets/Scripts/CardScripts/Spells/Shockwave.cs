using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shockwave : CardScript
{
    public override void InHandSetUp()
    {
        CardController card = GetComponent<CardController>();
        OnPlay onPlay = new OnPlay(card);

        SequenceSystem.TargetAllUnitsInZone targetUnits = new SequenceSystem.TargetAllUnitsInZone(2, CardSelector.ZoneFilter.EnemyZones);
        SequenceSystem.KnockbackUnit knockback = new SequenceSystem.KnockbackUnit();

        targetUnits.AddAbility(knockback);

        onPlay.AddAbility(targetUnits);
    }
}
