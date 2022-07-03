using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reap : CardScript
{
    public override void InHandSetUp()
    {
        CardController card = GetComponent<CardController>();
        OnPlay onPlay = new OnPlay(card);
        //conditions.Add(onPlay);

        //SequenceSystem.Target target = new SequenceSystem.Target(card, zoneFilter: CardSelector.ZoneFilter.EnemyZones, typeFilter: CardSelector.TypeFilter.Unit);
        //SequenceSystem.Damage damage = new SequenceSystem.Damage(3);
        //target.AddAbility(damage);

        //onPlay.AddAbility(target);
    }
}
