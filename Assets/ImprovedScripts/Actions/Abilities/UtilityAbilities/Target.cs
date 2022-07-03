using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SequenceSystem 
{  
    public class Target : Ability
    {
        protected CardParent card;
        public List<Ability> abilities = new List<Ability>();
        public GameObject target = null;
        protected List<CardParent> potentialTargets = new List<CardParent>();

        public bool optional = false;

        protected CardSelector.HandFilter handFilter;
        protected CardSelector.ZoneFilter zoneFilter;
        protected CardSelector.TypeFilter typeFilter;
        protected int costFilter = -1;
        protected CardSelector.CostCompare costCompare;
        protected CardSelector.OriginatingCard originatingCard;
        protected string statusFilter;
        protected string subtypeFilter;

        public Target(CardParent card, CardSelector.HandFilter handFilter = CardSelector.HandFilter.None,
        CardSelector.ZoneFilter zoneFilter = CardSelector.ZoneFilter.None,
        CardSelector.TypeFilter typeFilter = CardSelector.TypeFilter.All,
        int costFilter = -1,
        CardSelector.CostCompare costCompare = CardSelector.CostCompare.EqualTo,
        CardSelector.OriginatingCard originatingCard = CardSelector.OriginatingCard.TargetedAbility,
        string statusFilter = "",
        string subtypeFilter = "")
        {
            this.card = card;
            this.handFilter = handFilter;
            this.zoneFilter = zoneFilter;
            this.typeFilter = typeFilter;
            this.costFilter = costFilter;
            this.costCompare = costCompare;
            this.originatingCard = originatingCard;
            this.statusFilter = statusFilter;
            this.subtypeFilter = subtypeFilter;
        }

        public Target(int photonId, CardSelector.HandFilter handFilter = CardSelector.HandFilter.None,
        CardSelector.ZoneFilter zoneFilter = CardSelector.ZoneFilter.None,
        CardSelector.TypeFilter typeFilter = CardSelector.TypeFilter.All,
        int costFilter = -1,
        CardSelector.CostCompare costCompare = CardSelector.CostCompare.EqualTo,
        CardSelector.OriginatingCard originatingCard = CardSelector.OriginatingCard.TargetedAbility)
        {
            card = Photon.Pun.PhotonView.Find(photonId).GetComponent<CardParent>();
            this.handFilter = handFilter;
            this.zoneFilter = zoneFilter;
            this.typeFilter = typeFilter;
            this.costFilter = costFilter;
            this.costCompare = costCompare;
            this.originatingCard = originatingCard;
        }

        public Target(Target template) : base(template)
        {
            card = template.card;
            handFilter = template.handFilter;
            zoneFilter = template.zoneFilter;
            typeFilter = template.typeFilter;
            costFilter = template.costFilter;
            originatingCard = template.originatingCard;
            optional = template.optional;
            statusFilter = template.statusFilter;
            subtypeFilter = template.subtypeFilter;

            foreach(Ability ability in template.abilities)
            {
                AddAbility(ability);
            }
        }

        public override void PerformGameAction()
        {
            MouseManager.Instance.SetSelected(card.gameObject, this);
            Player.Instance.ChangeMode(Player.Mode.WaitForInput);

            foreach (CardController card in Player.Instance.hand.cards)
            {
                card.SetCanBePayedFor(false);
                card.currentState = CardController.CardState.InHandNoCount;
            }

            potentialTargets = CardSelector.GetCards(handFilter, zoneFilter, typeFilter, costFilter, costCompare, originatingCard, statusFilter: statusFilter, subtypeFilter: subtypeFilter);

            if (potentialTargets.Count > 0)
            {
                if (optional)
                {
                    MultiUseButton.Instance.SetButtonText("Decline");
                    MultiUseButton.Instance.SetButtonFunction(Interrupt);
                }
                else
                {
                    MultiUseButton.Instance.SetInteractable(false);
                }

                foreach (CardParent card in potentialTargets)
                {
                    card.potentialTarget.SetSelectable(true);
                }
            }
            else
            {
                foreach (CardController card in Player.Instance.hand.cards)
                {
                    Player.Instance.CheckIfCardCanBePlayed(card);
                    card.currentState = CardController.CardState.InHand;
                }

                EndAbility();
            }
        }

        public virtual void SetTarget(GameObject target)
        {
            this.target = target;

            foreach (CardParent card in potentialTargets)
            {
                card.potentialTarget.SetSelectable(false);
            }

            foreach (Ability ability in abilities)
            {
                if (ability is TargetedAbility)
                {
                    (ability as TargetedAbility).SetTarget(target);
                }
                MainSequenceManager.Instance.Add(ability);
            }

            MultiUseButton.Instance.BackToDefault();
            MultiUseButton.Instance.SetInteractable(Player.Instance.myTurn);

            foreach (CardController card in Player.Instance.hand.cards)
            {
                Player.Instance.CheckIfCardCanBePlayed(card);
                card.currentState = CardController.CardState.InHand;
            }

            EndAbility();
        }

        public void EndAbility()
        {
            Player.Instance.ChangeMode(Player.Mode.Normal);
            MouseManager.Instance.ClearSelected();
            // target = null;
            OnEnd();
        }

        public override void Interrupt()
        {
            base.Interrupt();

            foreach (CardParent card in potentialTargets)
            {
                card.potentialTarget.SetSelectable(false);
            }

            MultiUseButton.Instance.BackToDefault();
            MultiUseButton.Instance.SetInteractable(Player.Instance.myTurn);

            foreach (CardController card in Player.Instance.hand.cards)
            {
                Player.Instance.CheckIfCardCanBePlayed(card);
                card.currentState = CardController.CardState.InHand;
            }

            EndAbility();
        }

        public void AddAbility(Ability ability)
        {
            abilities.Add(ability);
        }
    }
}
