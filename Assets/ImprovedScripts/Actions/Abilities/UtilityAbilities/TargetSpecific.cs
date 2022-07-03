using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SequenceSystem 
{
    public class TargetSpecific : Ability
    {
        public List<Ability> abilities = new List<Ability>();

        string cardName = "";
        CardSelector.HandFilter handFilter;
        CardSelector.ZoneFilter zoneFilter;

        public TargetSpecific(string cardName = "", CardSelector.ZoneFilter zoneFilter = CardSelector.ZoneFilter.None, 
            CardSelector.HandFilter handFilter = CardSelector.HandFilter.None)
        {
            this.cardName = cardName;
            this.handFilter = handFilter;
            this.zoneFilter = zoneFilter;
        }

        public TargetSpecific(TargetSpecific template)
        {
            cardName = template.cardName;
            handFilter = template.handFilter;
            zoneFilter = template.zoneFilter;

            foreach(TargetedAbility ability in template.abilities)
            {
                AddAbility(ability);
            }
        }

        public override void PerformGameAction()
        {
            List<CardParent> cards = CardSelector.GetSpecificCard(cardName, zoneFilter, handFilter);

            foreach (CardParent card in cards)
            {
                foreach (TargetedAbility ability in abilities)
                {
                    ability.SetTarget(card.gameObject);
                    MainSequenceManager.Instance.Add(ability);
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
