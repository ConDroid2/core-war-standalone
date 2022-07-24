using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Immobile : Keyword
{
    UnitController unit;
    private void Awake()
    {
        unit = GetComponent<UnitController>();
        unit.AdvanceStack.Clear();
    }

    public override void RemoveKeyword()
    {
        base.RemoveKeyword();

        unit.AdvanceStack.Add(unit.unitAdvance);
    }
}
