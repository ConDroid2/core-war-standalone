using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HuntToExctinction : CardScript
{
    public override void InHandSetUp()
    {
        CardController card = GetComponent<CardController>();
        OnPlay onPlay = new OnPlay(card);
        conditions.Add(onPlay);

        SequenceSystem.Target target = new SequenceSystem.Target(card, zoneFilter: CardSelector.ZoneFilter.All, typeFilter: CardSelector.TypeFilter.Unit);
        HuntAction hunt = new HuntAction();
        target.AddAbility(hunt);

        onPlay.AddAbility(target);
    }

    public class HuntAction : SequenceSystem.TargetedAbility
    {
        public HuntAction()
        {

        }
        public HuntAction(HuntAction template) 
        {
            target = template.target;
        }

        public override void PerformGameAction()
        {
            UnitController unit = target.GetComponent<UnitController>();
            SequenceSystem.DestroyCard destroy = new SequenceSystem.DestroyCard();
            List<CardParent> copies = CardSelector.GetSpecificCard(unit.cardData.name, zoneFilter: CardSelector.ZoneFilter.All);

            foreach(CardParent card in copies)
            {
                destroy.SetTarget(card.gameObject);
                MainSequenceManager.Instance.Add(destroy);
            }

            OnEnd();
        }
    }
}
