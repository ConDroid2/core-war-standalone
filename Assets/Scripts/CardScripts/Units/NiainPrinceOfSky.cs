using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NiainPrinceOfSky : CardScript
{
    SequenceSystem.Summon summon;
    bool unitDied = false;

    public override void InPlaySetUp()
    {
        UnitController unit = GetComponent<UnitController>();
        if (unit.photonView.IsMine)
        {
            Enemy.Instance.unitManager.OnUnitDeath += HandleUnitDeath;
        }

        GetComponent<Legendary>().Initialize("Talon Gouge");

        OnTurnEnd turnEnd = new OnTurnEnd(GetComponent<InPlayCardController>());
        conditions.Add(turnEnd);

        summon = new SequenceSystem.Summon(1, "Raven", unit.gameObject);

        NiainAction action = new NiainAction(this);
        turnEnd.AddAbility(action);
    }

    public override void InPlayDeath()
    {
        base.InPlayDeath();

        Enemy.Instance.unitManager.OnUnitDeath -= HandleUnitDeath;
    }

    public void HandleUnitDeath(UnitController unit)
    {
        unitDied = true;
    }

    public class NiainAction : SequenceSystem.Ability
    {
        NiainPrinceOfSky script;
        public void Initialize(NiainPrinceOfSky script)
        {
            this.script = script;
        }

        public NiainAction(NiainPrinceOfSky script)
        {
            this.script = script;
        }

        public NiainAction(NiainAction template)
        {
            script = template.script;
        }

        public override void PerformGameAction()
        {
            if (script.unitDied)
            {
                MainSequenceManager.Instance.Add(script.summon);
            }

            script.unitDied = false;

            OnEnd();
        }
    }
}
