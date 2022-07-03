using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnNightmareDiscarded : AbilityCondition
{
    bool isMine;
    public OnNightmareDiscarded(bool isMine)
    {
        this.isMine = isMine;
        SetUp();
    }
    public override void Delete()
    {
        if (isMine)
        {
            Nightmare.OnDiscarded -= HandleCondition;
        }
    }

    public override void SetUp()
    {
        if (isMine)
        {
            Nightmare.OnDiscarded += HandleCondition;
        }
    }

    protected override void HandleCondition()
    {
        MainSequenceManager.Instance.Add(abilities);
    }
}
