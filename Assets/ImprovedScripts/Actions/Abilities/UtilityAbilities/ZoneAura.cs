using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SequenceSystem 
{
    public class ZoneAura : Ability, NonTemplateAction
    {
        private TargetedAbility addAura;
        private TargetedAbility removeAura;
        private string subtypeFilter;
        private UnitController controller;

        Zone currentlyAffectedZone = null;
        public ZoneAura(TargetedAbility addAura, TargetedAbility removeAura, UnitController controller, string subtypeFilter = "")
        {
            this.addAura = addAura;
            this.removeAura = removeAura;
            this.controller = controller;
            this.subtypeFilter = subtypeFilter;
        }

        public ZoneAura(ZoneAura template)
        {
            addAura = template.addAura;
            removeAura = template.removeAura;
            controller = template.controller;
            subtypeFilter = template.subtypeFilter;
        }

        public override void PerformGameAction()
        {
            controller.unitAdvance.OnActionEnd += HandleChangeZone;
            controller.unitMoveBack.OnActionEnd += HandleChangeZone;
            controller.OnRemovedFromPlay += HandleRemoveFromPlay;

            HandleChangeZone();

            OnEnd();
        }

        public void HandleChangeZone()
        {
            if(currentlyAffectedZone != null)
            {
                currentlyAffectedZone.cards.OnCardRemoved -= RemoveAura;
                currentlyAffectedZone.cards.OnCardAdded -= AddAura;

                foreach(CardParent card in currentlyAffectedZone.cards.cards)
                {
                    RemoveAura(card);
                }
            }

            currentlyAffectedZone = controller.currentZone;

            if (currentlyAffectedZone != null)
            {
                currentlyAffectedZone.cards.OnCardAdded += AddAura;
                currentlyAffectedZone.cards.OnCardRemoved += RemoveAura;

                foreach (CardParent card in currentlyAffectedZone.cards.cards)
                {
                    AddAura(card);
                }
            }
        }

        public void HandleRemoveFromPlay()
        {
            if (currentlyAffectedZone != null)
            {
                foreach (CardParent card in currentlyAffectedZone.cards.cards)
                {
                    RemoveAura(card);
                }

                currentlyAffectedZone.cards.OnCardAdded -= AddAura;
                currentlyAffectedZone.cards.OnCardRemoved -= RemoveAura;
            }

            controller.OnRemovedFromPlay -= HandleRemoveFromPlay;
        }

        private void AddAura(CardParent card)
        {
            if(CheckValidity(card))
            {
                addAura.SetTarget(card.gameObject);
                MainSequenceManager.Instance.Add(addAura);
            }
        }

        private void RemoveAura(CardParent card)
        {
            if (CheckValidity(card))
            {
                removeAura.SetTarget(card.gameObject);
                MainSequenceManager.Instance.Add(removeAura);
            }
        }

        private bool CheckValidity(CardParent card)
        {
            return (card != controller) && card.cardData.HasSubtype(subtypeFilter);
        }
    }
}
