using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnNextSpell : AbilityCondition
{
    private bool isMine;
    public OnNextSpell(bool isMine)
    {
        this.isMine = isMine;
        SetUp();
    }

    public override void SetUp()
    {
        if (this.isMine)
        {
            Player.Instance.spellManager.OnSpellCast += HandleCondition;
        }
    }

    public override void Delete()
    {
        if (this.isMine)
        {
            Player.Instance.spellManager.OnSpellCast -= HandleCondition;
        }
    }

    protected override void HandleCondition()
    {
        MainSequenceManager.Instance.Add(abilities);
        Delete();
    }
}
