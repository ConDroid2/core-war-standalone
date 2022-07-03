using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SequenceSystem 
{
    public class TargetRandomInDeck : Ability
    {
        protected List<InDeckTargetedAbility> abilities = new List<InDeckTargetedAbility>();

        protected CardSelector.TypeFilter typeFilter;
        protected int costFilter = -1;
        protected CardSelector.CostCompare costCompare;
        protected string subtypeFilter;
        protected string keywordFilter;
        protected int amount;
        public TargetRandomInDeck(
            CardSelector.TypeFilter typeFilter = CardSelector.TypeFilter.All,
            int costFilter = -1,
            CardSelector.CostCompare costCompare = CardSelector.CostCompare.EqualTo,
            string subtypeFilter = "",
            string keywordFilter = "",
            int amount = 1)
        {
            this.typeFilter = typeFilter;
            this.costFilter = costFilter;
            this.costCompare = costCompare;
            this.subtypeFilter = subtypeFilter;
            this.keywordFilter = keywordFilter;
            this.amount = amount;
        }

        public TargetRandomInDeck(TargetRandomInDeck template)
        {
            typeFilter = template.typeFilter;
            costFilter = template.costFilter;
            costCompare = template.costCompare;
            subtypeFilter = template.subtypeFilter;
            keywordFilter = template.keywordFilter;
            amount = template.amount;

            foreach(InDeckTargetedAbility ability in template.abilities)
            {
                AddAbility(ability);
            }
        }

        public override void PerformGameAction()
        {
            List<Card> cards = CardSelector.GetCardsFromDeck(typeFilter, costFilter, costCompare, subtypeFilter, keywordFilter);

            for(int i = 0; i < amount; i++)
            {
                if(cards.Count > 0)
                {
                    int randomIndex = Random.Range(0, cards.Count);

                    foreach (InDeckTargetedAbility ability in abilities)
                    {
                        ability.SetTarget(cards[randomIndex]);
                        MainSequenceManager.Instance.Add(ability);
                    }

                    cards.RemoveAt(randomIndex);
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
