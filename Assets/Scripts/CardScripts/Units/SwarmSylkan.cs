using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwarmSylkan : CardScript
{
    InPlayCardController card;
    SequenceSystem.BuffInPlay buff;
    SequenceSystem.BuffInPlay removeBuff;
    private List<InPlayCardController> accountedForUnits = new List<InPlayCardController>();

    public override void InPlaySetUp()
    {
        card = GetComponent<InPlayCardController>();
        OnEnterPlay onEnterPlay = new OnEnterPlay(card);
        conditions.Add(onEnterPlay);

        SwarmSylkanAction action = new SwarmSylkanAction(this);
        action.Initalize(this);
        onEnterPlay.AddAbility(action);

        buff = new SequenceSystem.BuffInPlay(1, 1, card as UnitController);
        removeBuff = new SequenceSystem.BuffInPlay(-1, -1, card as UnitController);

        if (card.isMine)
        {
            Player.Instance.zones[0].cards.OnCardAdded += IncreaseStats;
            Player.Instance.zones[1].cards.OnCardAdded += IncreaseStats;
        }
        
    }

    public override void InPlayDeath()
    {
        base.InPlayDeath();

        if (card.isMine)
        {
            Player.Instance.zones[0].cards.OnCardAdded -= IncreaseStats;
            Player.Instance.zones[1].cards.OnCardAdded -= IncreaseStats;

            foreach(InPlayCardController unit in accountedForUnits)
            {
                unit.OnRemovedFromPlayExternal -= DecreaseStats;
            }
        }      
    }

    public void IncreaseStats(CardParent card)
    {
        InPlayCardController unit = card as InPlayCardController;
        if (card.cardData.HasSubtype("Sylkan") && !accountedForUnits.Contains(unit) && card.gameObject != gameObject)
        {
            buff.PerformGameAction();

            accountedForUnits.Add(unit);
            unit.OnRemovedFromPlayExternal += DecreaseStats;
        }    
    }

    public void DecreaseStats(InPlayCardController card)
    {
        if (card.cardData.HasSubtype("Sylkan") && accountedForUnits.Contains(card) && card.gameObject != gameObject)
        {
            removeBuff.PerformGameAction();

            accountedForUnits.Remove(card);
            card.OnRemovedFromPlayExternal -= DecreaseStats;
        }      
    }

    public class SwarmSylkanAction : SequenceSystem.Ability
    {
        SwarmSylkan script;
        public void Initalize(SwarmSylkan script)
        {
            this.script = script;
        }

        public SwarmSylkanAction(SwarmSylkan script)
        {
            this.script = script;
        }

        public SwarmSylkanAction(SwarmSylkanAction template)
        {
            script = template.script;
        }

        public override void PerformGameAction()
        {
            List<CardParent> sylkans = CardSelector.GetCards(zoneFilter: CardSelector.ZoneFilter.MyZones, typeFilter: CardSelector.TypeFilter.Unit, subtypeFilter: "Sylkan");
            foreach (CardParent card in sylkans)
            {
                script.IncreaseStats(card as InPlayCardController);
            }

            OnEnd();
        }
    }
}
