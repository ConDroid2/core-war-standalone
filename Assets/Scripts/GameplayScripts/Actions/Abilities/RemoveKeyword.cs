using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoveKeyword : TargetedAbility
{
    string keywordName = "";

    private void Awake()
    {
        SetTarget(gameObject);
    }

    public void Initialize(string keywordName)
    {
        this.keywordName = keywordName;
    }

    public override IEnumerator ActionCoroutine()
    {
        UnitController unit = target.GetComponent<UnitController>();
        unit.RemoveKeyword(keywordName);

        OnEnd();
        return null;
    }
}
