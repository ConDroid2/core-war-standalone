using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrawlingCastle : CardScript
{
    UnitController card;
    Zone currentZoneAffected = null;
    public override void InPlaySetUp()
    {
        card = GetComponent<UnitController>();
        OnEnterPlay onPlay = new OnEnterPlay(card);
        conditions.Add(onPlay);

        SequenceSystem.ChangeArmor addArmor = new SequenceSystem.ChangeArmor(1, card);
        SequenceSystem.ChangeArmor removeArmor = new SequenceSystem.ChangeArmor(-1, card);


        SequenceSystem.ZoneAura zoneAura = new SequenceSystem.ZoneAura(addArmor, removeArmor, card);

        onPlay.AddAbility(zoneAura);
    }
}
