using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SequenceSystem 
{
    public class TargetMultiple : Target
    {
        public List<GameObject> targets = new List<GameObject>();
        private Dictionary<GameObject, LineRenderer> targetLines = new Dictionary<GameObject, LineRenderer>();
        private int amountOfTargets = 0;

        public enum TargetMode { ExactAmount, UpTo, Default };
        private TargetMode mode = TargetMode.Default;
        public TargetMultiple(CardParent card, CardSelector.HandFilter handFilter = CardSelector.HandFilter.None,
        CardSelector.ZoneFilter zoneFilter = CardSelector.ZoneFilter.None,
        CardSelector.TypeFilter typeFilter = CardSelector.TypeFilter.All,
        int costFilter = -1,
        CardSelector.CostCompare costCompare = CardSelector.CostCompare.EqualTo,
        string subtypeFilter = "",
        CardSelector.OriginatingCard originatingCard = CardSelector.OriginatingCard.TargetedAbility) : base(card, handFilter,
            zoneFilter, typeFilter, costFilter, costCompare, originatingCard, subtypeFilter: subtypeFilter)
        {
            
        }

        public TargetMultiple(int photonView, CardSelector.HandFilter handFilter = CardSelector.HandFilter.None,
        CardSelector.ZoneFilter zoneFilter = CardSelector.ZoneFilter.None,
        CardSelector.TypeFilter typeFilter = CardSelector.TypeFilter.All,
        int costFilter = -1,
        CardSelector.CostCompare costCompare = CardSelector.CostCompare.EqualTo,
        CardSelector.OriginatingCard originatingCard = CardSelector.OriginatingCard.TargetedAbility) : base(photonView, handFilter,
            zoneFilter, typeFilter, costFilter, costCompare, originatingCard)
        {

        }

        public TargetMultiple(TargetMultiple template) : base(template)
        {
            amountOfTargets = template.amountOfTargets;
            mode = template.mode;
        }

        public override void PerformGameAction()
        {
            MouseManager.Instance.SetSelected(card.gameObject, this);

            foreach (CardController card in Player.Instance.hand.cards)
            {
                card.SetCanBePayedFor(false);
                card.currentState = CardController.CardState.InHandNoCount;
            }

            potentialTargets = CardSelector.GetCards(handFilter, zoneFilter, typeFilter, costFilter, costCompare, originatingCard);

            if (potentialTargets.Count > 0)
            {
                targets.Clear();
                MultiUseButton.Instance.SetButtonText("Done");
                MultiUseButton.Instance.SetButtonFunction(DoneSelecting);

                if (mode == TargetMode.ExactAmount)
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
                MouseManager.Instance.ClearSelected();
                foreach (CardController card in Player.Instance.hand.cards)
                {
                    Player.Instance.CheckIfCardCanBePlayed(card);
                    card.currentState = CardController.CardState.InHand;
                }
                OnEnd();
            }
        }
        public override void SetTarget(GameObject newTarget)
        {
            PotentialTarget potentialTarget = newTarget.GetComponent<PotentialTarget>();

            if (targets.Contains(newTarget))
            {
                targets.Remove(newTarget);
                potentialTarget.SetSelected(false);
                MouseManager.Instance.linePool.ReturnToPool(targetLines[newTarget]);
                targetLines.Remove(newTarget);

                if (mode == TargetMode.ExactAmount && targets.Count != amountOfTargets)
                {
                    MultiUseButton.Instance.SetInteractable(false);
                }
            }
            else
            {
                if (amountOfTargets == 0 || ((mode == TargetMode.UpTo || mode == TargetMode.ExactAmount) && targets.Count < amountOfTargets))
                {
                    targets.Add(newTarget);
                    potentialTarget.SetSelected(true);
                    targetLines.Add(newTarget, MouseManager.Instance.linePool.Get(newTarget.transform.position));
                }

                if (amountOfTargets > 0 && targets.Count == amountOfTargets && mode == TargetMode.ExactAmount)
                {
                    MultiUseButton.Instance.SetInteractable(true);
                }
            }
        }

        public void DoneSelecting()
        {
            foreach (CardParent card in potentialTargets)
            {
                card.potentialTarget.SetSelectable(false);
            }

            MultiUseButton.Instance.BackToDefault();
            MultiUseButton.Instance.SetInteractable(Player.Instance.myTurn);

            foreach (GameObject target in targets)
            {
                foreach (Ability ability in abilities)
                {
                    if (ability is TargetedAbility)
                    {
                        (ability as TargetedAbility).SetTarget(target);
                        MouseManager.Instance.linePool.ReturnToPool(targetLines[target]);
                    }

                    MainSequenceManager.Instance.Add(ability);        
                }
            }

            foreach (CardController card in Player.Instance.hand.cards)
            {
                Player.Instance.CheckIfCardCanBePlayed(card);
                card.currentState = CardController.CardState.InHand;
            }

            MouseManager.Instance.ClearSelected();
            OnEnd();
        }

        public void SetAmountOfTargets(int amount)
        {
            amountOfTargets = amount;
        }

        public void SetTargetMode(TargetMode mode)
        {
            this.mode = mode;
        }
    }
}
