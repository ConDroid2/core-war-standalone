using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SylkanDrone : CardScript
{
    UnitController unit;
    bool buffed = false;
    SequenceSystem.BuffInPlay buff;
    SequenceSystem.BuffInPlay removeBuff;
    public override void InPlaySetUp()
    {
        unit = GetComponent<UnitController>();

        buff = new SequenceSystem.BuffInPlay(1, 1, unit);

        removeBuff = new SequenceSystem.BuffInPlay(-1, -1, unit);

        if (unit.isMine)
        {
            ApplyBuff(unit);

            Player.Instance.zones[0].cards.OnCardRemoved += RemoveBuff;
            Player.Instance.zones[1].cards.OnCardRemoved += RemoveBuff;
            Player.Instance.zones[0].cards.OnCardAdded += ApplyBuff;
            Player.Instance.zones[1].cards.OnCardAdded += ApplyBuff;
        }
    }

    public override void InPlayDeath()
    {
        base.InPlayDeath();

        if (unit.isMine)
        {
            Player.Instance.zones[0].cards.OnCardRemoved -= RemoveBuff;
            Player.Instance.zones[1].cards.OnCardRemoved -= RemoveBuff;
            Player.Instance.zones[0].cards.OnCardAdded -= ApplyBuff;
            Player.Instance.zones[1].cards.OnCardAdded -= ApplyBuff;
        }
    }

    public void ApplyBuff(CardParent card)
    {
        List<CardParent> cards = CardSelector.GetCards(zoneFilter: CardSelector.ZoneFilter.MyZones, typeFilter: CardSelector.TypeFilter.Unit, subtypeFilter: "Sylkan");
        cards.Remove(unit);
        if (cards.Count > 0 && !buffed)
        {
            MainSequenceManager.Instance.Add(buff);
            buffed = true;
        }
    }

    public void RemoveBuff(CardParent card)
    {
        List<CardParent> cards = CardSelector.GetCards(zoneFilter: CardSelector.ZoneFilter.MyZones, typeFilter: CardSelector.TypeFilter.Unit, subtypeFilter: "Sylkan");
        cards.Remove(unit);
        if (cards.Count == 0 && buffed)
        {
            MainSequenceManager.Instance.Add(removeBuff);
            buffed = false;
        }
    }
}
