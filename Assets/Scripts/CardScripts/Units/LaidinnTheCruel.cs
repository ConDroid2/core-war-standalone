using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaidinnTheCruel : CardScript
{
    SequenceSystem.Damage damage;
    SequenceSystem.TargetAll targetAll;
    UnitController unit;
    public override void InPlaySetUp()
    {
        unit = GetComponent<UnitController>();

        GetComponent<Legendary>().Initialize("Flame Lash");

        targetAll = new SequenceSystem.TargetAll(unit, zoneFilter: CardSelector.ZoneFilter.All, typeFilter: CardSelector.TypeFilter.Unit);
        targetAll.ExcludeMe();


        GetComponent<UnitController>().takeDamage = LaidinnTakeDamage;
    }

    public void LaidinnTakeDamage(int damage)
    {
        //this.damage = new SequenceSystem.Damage(damage - unit.cardData.armor);
        //targetAll.abilities.Clear();
        //targetAll.abilities.Add(this.damage);

        //if (unit.photonView.IsMine)
        //{
        //    MainSequenceManager.Instance.Add(targetAll);
        //}

        //unit.DefaultTakeDamage(damage);
    }
}
