using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SequenceSystem 
{
    public class Aura : Ability, NonTemplateAction
    {
        private TargetedAbility addAura;
        private TargetedAbility removeAura;
        private bool isMine;

        private CardSelector.HandFilter handFilter;
        private CardSelector.ZoneFilter zoneFilter;
        private CardSelector.TypeFilter typeFilter;
        private string subtypeFilter;
        private string keywordFilter;
        private string statusFilter;

        private InPlayCardController controller;

        public Aura(
            TargetedAbility addAura,
            TargetedAbility removeAura,
            bool isMine,
            CardSelector.HandFilter handFilter = CardSelector.HandFilter.None,
            CardSelector.ZoneFilter zoneFilter = CardSelector.ZoneFilter.None,
            CardSelector.TypeFilter typeFilter = CardSelector.TypeFilter.All,
            string subtypeFilter = "",
            string keywordFilter = "",
            string statusFilter = "",
            InPlayCardController controller = null)
        {
            this.addAura = addAura;
            this.removeAura = removeAura;
            this.handFilter = handFilter;
            this.zoneFilter = zoneFilter;
            this.typeFilter = typeFilter;
            this.subtypeFilter = subtypeFilter;
            this.keywordFilter = keywordFilter;
            this.statusFilter = statusFilter;
            this.isMine = isMine;
            this.controller = controller;
        }

        public Aura(Aura template)
        {
            addAura = template.addAura;
            removeAura = template.removeAura;
            handFilter = template.handFilter;
            zoneFilter = template.zoneFilter;
            typeFilter = template.typeFilter;
            subtypeFilter = template.subtypeFilter;
            keywordFilter = template.keywordFilter;
            statusFilter = template.statusFilter;
            isMine = template.isMine;
            controller = template.controller;
        }

        public override void PerformGameAction()
        {
            List<CardParent> cards = CardSelector.GetCards(handFilter: handFilter, zoneFilter: zoneFilter, typeFilter: typeFilter, subtypeFilter: subtypeFilter, keywordFilter: keywordFilter, statusFilter: statusFilter);

            foreach (CardParent card in cards)
            {
                addAura.SetTarget(card.gameObject);
                MainSequenceManager.Instance.Add(addAura);
            }

            if (handFilter == CardSelector.HandFilter.EnemyHand || handFilter == CardSelector.HandFilter.All)
            {
                Enemy.Instance.hand.OnCardAdded += ApplyAura;
            }
            if (handFilter == CardSelector.HandFilter.MyHand || handFilter == CardSelector.HandFilter.All)
            {
                Player.Instance.hand.OnCardAdded += ApplyAura;
            }

            if (typeFilter == CardSelector.TypeFilter.Unit && (zoneFilter == CardSelector.ZoneFilter.EnemyZones || zoneFilter == CardSelector.ZoneFilter.All))
            {
                Enemy.Instance.unitManager.OnUnitAdded += ApplyAura;
            }
            else if (typeFilter == CardSelector.TypeFilter.Unit && (zoneFilter == CardSelector.ZoneFilter.MyZones || zoneFilter == CardSelector.ZoneFilter.All))
            {
                Player.Instance.unitManager.OnUnitAdded += ApplyAura;
            }

            if(controller != null)
            {
                controller.OnRemovedFromPlay += RemoveAura;
            }
                
            OnEnd();
        }

        public void ApplyAura(CardParent card)
        {
            if (isMine)
            {
                bool matchesType = true;

                if (typeFilter == CardSelector.TypeFilter.Spell && card.cardData.type != CardUtilities.Type.Spell) matchesType = false;
                else if (typeFilter == CardSelector.TypeFilter.Unit && card.cardData.type != CardUtilities.Type.Character) matchesType = false;
                else if (typeFilter == CardSelector.TypeFilter.Support && card.cardData.type != CardUtilities.Type.Support) matchesType = false;
                if (CheckTarget(card.cardData) && matchesType)
                {
                    Debug.Log("Applying aura to card");
                    addAura.SetTarget(card.gameObject);
                    addAura.PerformGameAction();
                }
            }
        }

        public void RemoveAura()
        {
            if (isMine)
            {
                List<CardParent> cards = CardSelector.GetCards(handFilter: handFilter, zoneFilter: zoneFilter, typeFilter: typeFilter, subtypeFilter: subtypeFilter, keywordFilter: keywordFilter, statusFilter: statusFilter);

                foreach (CardParent card in cards)
                {
                    removeAura.SetTarget(card.gameObject);
                    removeAura.PerformGameAction();
                }

                Enemy.Instance.hand.OnCardAdded -= ApplyAura;
                Player.Instance.hand.OnCardAdded -= ApplyAura;
                Enemy.Instance.unitManager.OnUnitAdded -= ApplyAura;
                Player.Instance.unitManager.OnUnitAdded -= ApplyAura;
            }
        }

        private bool CheckTarget(Card card)
        {
            bool validTarget = false;

            if(subtypeFilter != "" && card.HasSubtype(subtypeFilter))
            {
                validTarget = true;
            }

            if(keywordFilter != "" && card.keywords.Contains(keywordFilter))
            {
                validTarget &= true;
            }

            if(statusFilter != "" && card.activeStatuses.Contains(statusFilter))
            {
                validTarget &= true;
            }

            return validTarget;
        }
    }
}
