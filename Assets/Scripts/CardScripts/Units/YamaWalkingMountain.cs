using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YamaWalkingMountain : CardScript
{
    public override void InPlaySetUp()
    {
        UnitController unit = GetComponent<UnitController>();
        GetComponent<Legendary>().Initialize("World Wake");

        unit.cardData.statusImmunities.Add("Frozen");
        unit.cardData.statusImmunities.Add("Rooted");
    }
}
