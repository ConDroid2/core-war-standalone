using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LumberingHill : CardScript
{
    public override void InPlaySetUp()
    {
        UnitController unit = GetComponent<UnitController>();
        unit.cardData.statusImmunities.Add("Frozen");
        unit.cardData.statusImmunities.Add("Rooted");
    }
}
