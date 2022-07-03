using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SequenceSystem 
{
    public class TargetAllInDeck : Ability
    {
        protected List<InDeckTargetedAbility> abilities = new List<InDeckTargetedAbility>();

        protected CardSelector.TypeFilter typeFilter;
        protected int costFilter = -1;
        protected CardSelector.CostCompare costCompare;
        protected string subtypeFilter;
        protected string keywordFilter;

        public TargetAllInDeck(
            CardSelector.TypeFilter typeFilter = CardSelector.TypeFilter.All,
            int costFilter = -1,
            CardSelector.CostCompare costCompare = CardSelector.CostCompare.EqualTo,
            string subtypeFilter = "",
            string keywordFilter = "")
        {
            this.typeFilter = typeFilter;
            this.costFilter = costFilter;
            this.costCompare = costCompare;
            this.subtypeFilter = subtypeFilter;
            this.keywordFilter = keywordFilter;
        }

        public TargetAllInDeck(TargetAllInDeck template)
        {
            typeFilter = template.typeFilter;
            costFilter = template.costFilter;
            costCompare = template.costCompare;
            subtypeFilter = template.subtypeFilter;
            keywordFilter = template.keywordFilter;

            foreach (InDeckTargetedAbility ability in template.abilities)
            {
                AddAbility(ability);
            }
        }

        public override void PerformGameAction()
        {
            List<Card> cards = CardSelector.GetCardsFromDeck(typeFilter, costFilter, costCompare, subtypeFilter, keywordFilter);

            foreach(Card card in cards)
            {
                foreach(InDeckTargetedAbility ability in abilities)
                {
                    ability.SetTarget(card);
                    MainSequenceManager.Instance.Add(ability);
                }
            }

            OnEnd();
        }

        public void AddAbility(InDeckTargetedAbility ability)
        {
            abilities.Add(ability);
        }
    }
}
