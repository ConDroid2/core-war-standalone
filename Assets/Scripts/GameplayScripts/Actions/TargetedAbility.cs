using System;
using UnityEngine;

public class TargetedAbility : Ability
{
    protected Type targetType;
    protected GameObject target = null;

    public virtual void SetTarget(GameObject newTarget) {
        target = newTarget;
    }
}
