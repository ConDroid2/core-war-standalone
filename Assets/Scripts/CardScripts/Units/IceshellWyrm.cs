using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceshellWyrm : CardScript
{
    SequenceSystem.BuffInPlay buff;
    SequenceSystem.Heal heal;
    UnitController unit;

    public override void InPlaySetUp()
    {
        buff = new SequenceSystem.BuffInPlay(1, 1, unit);

        heal = new SequenceSystem.Heal();

        unit = GetComponent<UnitController>();
        unit.cardData.statusImmunities.Add("Frozen");

        buff = new SequenceSystem.BuffInPlay(1, 1, unit);
        heal = new SequenceSystem.Heal();

        if (unit.photonView.IsMine)
        {
            unit.OnAddStatus += HandleFrozen;
        }     
    }

    public override void InPlayDeath()
    {
        base.InPlayDeath();

        if (unit.photonView.IsMine)
        {
            GetComponent<UnitController>().OnAddStatus -= HandleFrozen;
        }      
    }

    public void HandleFrozen(string status)
    {
        if(status == "Frozen")
        {
            buff.SetTarget(gameObject);
            heal.SetTarget(gameObject);

            MainSequenceManager.Instance.Add(heal);
            MainSequenceManager.Instance.Add(buff);
        }
    }
}
