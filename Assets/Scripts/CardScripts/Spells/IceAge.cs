using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceAge : CardScript
{
    public override void InHandSetUp()
    {
        OnPlay onPlay = new OnPlay(GetComponent<CardController>());
        conditions.Add(onPlay);

        IceAgeAction action = new IceAgeAction();
        onPlay.AddAbility(action);
    }

    public class IceAgeAction : SequenceSystem.Ability
    {
        public IceAgeAction() { }
        public IceAgeAction(IceAgeAction template) { }
        public override void PerformGameAction()
        {
            List<CardParent> units = CardSelector.GetCards(zoneFilter: CardSelector.ZoneFilter.All, typeFilter: CardSelector.TypeFilter.Unit);
            units.RemoveAll(card => card.cardData.currentResilience >= MagickManager.Instance.level["Blue"]);
            SequenceSystem.AddStatus addStatus = new SequenceSystem.AddStatus("Frozen", 2);

            foreach (CardParent unit in units)
            {
                addStatus.SetTarget(unit.gameObject);
                MainSequenceManager.Instance.Add(addStatus);
            }

            OnEnd();
        }
    }
}
