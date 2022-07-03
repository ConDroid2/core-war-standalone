using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoveStatus : TargetedAbility
{
    public override IEnumerator ActionCoroutine()
    {
        UnitController unit = target.GetComponent<UnitController>();

        List<string> statuses = new List<string>(unit.cardData.activeStatuses);

        foreach(string status in statuses)
        {
            unit.RemoveStatus(status);
        }

        OnEnd();
        return null;
    }
}
