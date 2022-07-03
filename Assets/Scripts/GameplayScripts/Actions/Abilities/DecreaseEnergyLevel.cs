using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DecreaseEnergyLevel : Ability
{
    int amount = 0;
    string color = "";

    public void Initialize(int amount, string color)
    {
        this.amount = amount;
        this.color = color;
    }

    public override IEnumerator ActionCoroutine()
    {
        MagickManager.Instance.ChangeLevel(color, amount * -1);

        OnEnd();
        return null;
    }

    public override void SetUpAbility(string[] str_args)
    {
        amount = str_args[0].ConvertToInt();
        if (str_args.Length > 1)
        {
            color = str_args[1];
        }
    }
}
