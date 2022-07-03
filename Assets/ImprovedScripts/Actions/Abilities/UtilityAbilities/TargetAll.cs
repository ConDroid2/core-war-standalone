using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SequenceSystem 
{
    public class TargetAll : Ability
    {
        CardParent controller;
        public List<TargetedAbility> abilities = new List<TargetedAbility>();
        protected bool excludeMe = false;

        protected CardSelector.HandFilter handFilter;
        protected CardSelector.ZoneFilter zoneFilter;
        protected CardSelector.TypeFilter typeFilter;
        protected int costFilter = -1;
        protected CardSelector.CostCompare costCompare;
        protected CardSelector.OriginatingCard originatingCard;
        protected string subtypeFilter;
        protected string keywordFilter;
        protected string statusFilter;

        public TargetAll(CardParent controller,
            CardSelector.HandFilter handFilter = CardSelector.HandFilter.None,
            CardSelector.ZoneFilter zoneFilter = CardSelector.ZoneFilter.None,
            CardSelector.TypeFilter typeFilter = CardSelector.TypeFilter.All,
            int costFilter = -1,
            CardSelector.CostCompare costCompare = CardSelector.CostCompare.EqualTo,
            CardSelector.OriginatingCard originatingCard = CardSelector.OriginatingCard.Default,
            string subtypeFilter = "",
            string keywordFilter = "",
            string statusFilter = "")
        {
            this.controller = controller;
            this.handFilter = handFilter;
            this.zoneFilter = zoneFilter;
            this.typeFilter = typeFilter;
            this.costFilter = costFilter;
            this.costCompare = costCompare;
            this.originatingCard = originatingCard;
            this.subtypeFilter = subtypeFilter;
            this.keywordFilter = keywordFilter;
            this.statusFilter = statusFilter;
        }

        public TargetAll(TargetAll template) : base(template)
        {
            controller = template.controller;
            handFilter = template.handFilter;
            zoneFilter = template.zoneFilter;
            typeFilter = template.typeFilter;
            costFilter = template.costFilter;
            costCompare = template.costCompare;
            originatingCard = template.originatingCard;
            subtypeFilter = template.subtypeFilter;
            keywordFilter = template.keywordFilter;
            statusFilter = template.statusFilter;
            excludeMe = template.excludeMe;

            foreach(TargetedAbility ability in template.abilities)
            {
                AddAbility(ability);
            }
        }

        public override void PerformGameAction()
        {
            List<CardParent> cards = CardSelector.GetCards(handFilter, zoneFilter, typeFilter, costFilter, costCompare, originatingCard, subtypeFilter, keywordFilter, statusFilter);

            if (excludeMe)
            {
                if (cards.Contains(controller))
                {
                    cards.Remove(controller);
                }
            }

            foreach (CardParent card in cards)
            {
                foreach (TargetedAbility ability in abilities)
                {
                    ability.SetTarget(card.gameObject);
                    MainSequenceManager.Instance.AddNext(ability);
                }
            }

            OnEnd();
        }

        public void ExcludeMe()
        {
            excludeMe = true;
        }

        public void AddAbility(TargetedAbility ability)
        {
            abilities.Add(ability);
        }
    }
}
