using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Icetouch : Keyword
{
    UnitController unit;
    private void Awake()
    {
        unit = GetComponent<UnitController>();

        unit.OnDealDamageTo += HandleDealDamageTo;
    }

    public override void RemoveKeyword()
    {
        unit.OnDealDamageTo -= HandleDealDamageTo;
        base.RemoveKeyword();
    }

    public void HandleDealDamageTo(UnitController enemy)
    {
        enemy.AddStatus("Frozen", 2);
    }
}
