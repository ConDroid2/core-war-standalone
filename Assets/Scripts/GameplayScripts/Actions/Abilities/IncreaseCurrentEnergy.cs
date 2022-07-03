using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IncreaseCurrentEnergy : Ability
{
    int amount;
    string school = "";

    public void Initialize(int amount, string school = "")
    {
        this.amount = amount;
        this.school = school;
    }

    public override IEnumerator ActionCoroutine()
    {
        if(school == "")
        {
            Debug.Log("Do some magic or something I guess?");
            MagickManager.Instance.OnCurrentChange += HandleCurrentChange;
            MagickManager.Instance.ChangeMode(MagickManager.Mode.IncreaseCurrent);
            while(amount > 0)
            {
                yield return null;
            }
            MagickManager.Instance.OnCurrentChange -= HandleCurrentChange;
            MagickManager.Instance.ChangeMode(MagickManager.Mode.Default);
        }
        else
        {
            MagickManager.Instance.ChangeCurrent(school, amount);
        }

        OnEnd();
    }

    public void HandleCurrentChange(string color, int amount)
    {
        this.amount--;
    }
}
