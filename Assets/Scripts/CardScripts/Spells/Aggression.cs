using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aggression : CardScript
{
    public override void InHandSetUp()
    {
        CardController card = GetComponent<CardController>();
        OnPlay onPlay = new OnPlay(GetComponent<CardController>());
        conditions.Add(onPlay);

        SequenceSystem.Target target = new SequenceSystem.Target(card, zoneFilter: CardSelector.ZoneFilter.MyZones, typeFilter: CardSelector.TypeFilter.Unit);

        SequenceSystem.RefreshActions refresh = new SequenceSystem.RefreshActions();

        SequenceSystem.AddStatus status = new SequenceSystem.AddStatus("Rooted", 1);

        target.abilities.Add(refresh);
        target.abilities.Add(status);

        onPlay.AddAbility(target);
    }
}
