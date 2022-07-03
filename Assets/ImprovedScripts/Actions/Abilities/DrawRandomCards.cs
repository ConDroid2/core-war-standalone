using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SequenceSystem 
{
    public class DrawRandomCards : Ability
    {
        CardSelector.TypeFilter typeFilter;
        int costFilter;
        CardSelector.CostCompare costCompare;
        StringInput subtypeFilter;
        string keywordFilter;
        int amount;
        bool getUnique;

        List<TargetedAbility> abilitiesTargetingCards = new List<TargetedAbility>();
        public DrawRandomCards(int amount = 0,
            CardSelector.TypeFilter typeFilter = CardSelector.TypeFilter.All,
            int costFilter = -1,
            CardSelector.CostCompare costCompare = CardSelector.CostCompare.EqualTo,
            StringInput subtypeFilter = null,
            string keywordFilter = "",
            bool getUnique = false)
        {
            this.amount = amount;
            this.typeFilter = typeFilter;
            this.costFilter = costFilter;
            this.costCompare = costCompare;
            if(subtypeFilter == null)
            {
                this.subtypeFilter = new StringInput("");
            }
            else
            {
                this.subtypeFilter = subtypeFilter;
            }
            
            this.keywordFilter = keywordFilter;
            this.getUnique = getUnique;
        }

        public DrawRandomCards(DrawRandomCards template)
        {
            amount = template.amount;
            typeFilter = template.typeFilter;
            costFilter = template.costFilter;
            costCompare = template.costCompare;
            subtypeFilter = template.subtypeFilter;
            keywordFilter = template.keywordFilter;
            getUnique = template.getUnique;
        }

        public override void PerformGameAction()
        {
            List<Card> potentialCards = CardSelector.GetCardsFromDeck(typeFilter, costFilter, costCompare, subtypeFilter.Value, keywordFilter);
            PlayerDraw draw = Player.Instance.draw;

            for (int i = 0; i < amount; i++)
            {
                if (potentialCards.Count > 0)
                {
                    int randomIndex = Random.Range(0, potentialCards.Count);

                    Card cardToDraw = potentialCards[randomIndex];
                    potentialCards.RemoveAt(randomIndex);

                    if (getUnique)
                    {
                        potentialCards.RemoveAll((card) => card.name == cardToDraw.name);
                    }

                    draw.SetSpecificCard(cardToDraw);
                    draw.AddAbilities(abilitiesTargetingCards);

                    MainSequenceManager.Instance.AddNext(draw);
                }
            }

            OnEnd();
        }

        public void AddAbility(TargetedAbility ability)
        {
            abilitiesTargetingCards.Add(ability);
        }
    }
}
