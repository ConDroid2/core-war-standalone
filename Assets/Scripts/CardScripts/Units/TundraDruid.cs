using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TundraDruid : CardScript
{
    public override void InPlaySetUp()
    {
        UnitController card = GetComponent<UnitController>();
        OnEnterPlay onEnterPlay = new OnEnterPlay(card);
        conditions.Add(onEnterPlay);

        TundraDruidAction action = new TundraDruidAction(card);
        onEnterPlay.AddAbility(action);

        card.cardData.statusImmunities.Add("Rooted");
    }

    public class TundraDruidAction : SequenceSystem.Ability
    {
        SequenceSystem.AddStatus addStatus;
        UnitController unit;

        public TundraDruidAction(UnitController unit)
        {
            this.unit = unit;

            addStatus = new SequenceSystem.AddStatus("Rooted", 1);
        }

        public TundraDruidAction(TundraDruidAction template)
        {
            unit = template.unit;

            addStatus = new SequenceSystem.AddStatus("Rooted", 1);
        }

        public override void PerformGameAction()
        {
            List<CardParent> units = CardSelector.GetCards(zoneFilter: CardSelector.ZoneFilter.All, typeFilter: CardSelector.TypeFilter.Unit);
            units.RemoveAll(unit => unit.cardData.currentResilience > 1);

            foreach (CardParent unit in units)
            {
                addStatus.SetTarget(unit.gameObject);

                MainSequenceManager.Instance.Add(addStatus);
            }

            OnEnd();
        }
    }
}
