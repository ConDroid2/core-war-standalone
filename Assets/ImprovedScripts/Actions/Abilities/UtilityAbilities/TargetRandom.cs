using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SequenceSystem 
{
    public class TargetRandom : Ability
    {
        CardParent controller;
        public List<TargetedAbility> abilities = new List<TargetedAbility>();
        protected bool excludeMe = false;
        protected int amount;

        protected CardSelector.HandFilter handFilter;
        protected CardSelector.ZoneFilter zoneFilter;
        protected CardSelector.TypeFilter typeFilter;
        protected int costFilter = -1;
        protected CardSelector.CostCompare costCompare;
        protected CardSelector.OriginatingCard originatingCard;
        protected string subtypeFilter;
        protected string keywordFilter;
        protected string statusFilter;

        public TargetRandom(
            CardParent controller,
            CardSelector.HandFilter handFilter = CardSelector.HandFilter.None,
            CardSelector.ZoneFilter zoneFilter = CardSelector.ZoneFilter.None,
            CardSelector.TypeFilter typeFilter = CardSelector.TypeFilter.All,
            int costFilter = -1,
            CardSelector.CostCompare costCompare = CardSelector.CostCompare.EqualTo,
            CardSelector.OriginatingCard originatingCard = CardSelector.OriginatingCard.Default,
            string subtypeFilter = "",
            string keywordFilter = "",
            string statusFilter = "",
            int amount = 1)
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
            this.amount = amount;
        }

        public TargetRandom(TargetRandom template) : base(template)
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
            amount = template.amount;

            foreach (TargetedAbility ability in template.abilities)
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

            for (int i = 0; i < amount; i++)
            {
                if (cards.Count > 0)
                {
                    int randomIndex = Random.Range(0, cards.Count);

                    foreach (TargetedAbility ability in abilities)
                    {
                        ability.SetTarget(cards[randomIndex].gameObject);
                        MainSequenceManager.Instance.Add(ability);
                    }

                    cards.RemoveAt(randomIndex);
                }
            }

            OnEnd();
        }

        public void AddAbility(TargetedAbility ability)
        {
            abilities.Add(ability);
        }
    }
}
