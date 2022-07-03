using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddStatus : TargetedAbility
{
    string status = "";
    int count = 0;

    public override IEnumerator ActionCoroutine()
    {
        UnitController unit = target.GetComponent<UnitController>();

        unit.AddStatus(status, count);

        OnEnd();
        return null;
    }

    public void Initialize(string status, int count)
    {
        this.status = status;
        this.count = count;
    }
}
