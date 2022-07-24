using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarmThornhide : CardScript
{
    UnitController unit;
    public override void InPlaySetUp()
    {
        GetComponent<Legendary>().Initialize("Aggression");

        unit = GetComponent<UnitController>();
        unit.takeDamage = BarmTakeDamage;
    }

    public void BarmTakeDamage(int damage)
    {
        unit.DefaultTakeDamage(damage);

        unit.ChangeDamage(unit.cardData.currentStrength + (damage - unit.cardData.armor));
    }
}
