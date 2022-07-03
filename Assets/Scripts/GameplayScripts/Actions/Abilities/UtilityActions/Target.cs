using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Target : Ability
{
    CardParent card;
    public List<TargetedAbility> abilities = new List<TargetedAbility>();
    public GameObject target = null;

    public bool optional = false;

    protected CardSelector.HandFilter handFilter;
    protected CardSelector.ZoneFilter zoneFilter;
    protected CardSelector.TypeFilter typeFilter;
    protected int costFilter = -1;
    protected CardSelector.CostCompare costCompare;
    protected CardSelector.OriginatingCard originatingCard;

    private void Awake()
    {
        card = GetComponent<CardParent>();
    }

    public void Initialize(CardSelector.HandFilter handFilter = CardSelector.HandFilter.None,
        CardSelector.ZoneFilter zoneFilter = CardSelector.ZoneFilter.None,
        CardSelector.TypeFilter typeFilter = CardSelector.TypeFilter.All,
        int costFilter = -1,
        CardSelector.CostCompare costCompare = CardSelector.CostCompare.EqualTo,
        CardSelector.OriginatingCard originatingCard = CardSelector.OriginatingCard.TargetedAbility)
    {
        this.handFilter = handFilter;
        this.zoneFilter = zoneFilter;
        this.typeFilter = typeFilter;
        this.costFilter = costFilter;
        this.costCompare = costCompare;
        this.originatingCard = originatingCard;
    }

    public override IEnumerator ActionCoroutine()
    {
        // Wait for a target to be set
        // MouseManager.Instance.SetSelected(gameObject, this);
        Player.Instance.ChangeMode(Player.Mode.WaitForInput);

        foreach(CardController card in Player.Instance.hand.cards)
        {
            card.SetCanBePayedFor(false);
            card.currentState = CardController.CardState.InHandNoCount;
        }

        List<CardParent> potentialTargets = CardSelector.GetCards(handFilter, zoneFilter, typeFilter, costFilter, costCompare, originatingCard);

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


            while (target == null && !interrupted)
            {
                yield return null;
            }

            foreach (CardParent card in potentialTargets)
            {
                card.potentialTarget.SetSelectable(false);
            }

            if (!interrupted)
            {
                // Create a sequence for the abilities to run in and put them in it
                ActionSequencer targetedSequence = new ActionSequencer();

                foreach (TargetedAbility ability in abilities)
                {
                    ability.SetTarget(target);
                    targetedSequence.AddGameAction(ability);
                }

                // Run the sequence and wait for it to finish
                yield return StartCoroutine(targetedSequence.RunSequence());
            }

            MultiUseButton.Instance.BackToDefault();
            MultiUseButton.Instance.SetInteractable(Player.Instance.myTurn);
        }

        // This is for the case that the potential targets were cards in your hand                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                      
        foreach (CardController card in Player.Instance.hand.cards)
        {
            Player.Instance.CheckIfCardCanBePlayed(card);
            card.currentState = CardController.CardState.InHand;
        }

        Player.Instance.ChangeMode(Player.Mode.Normal);
        MouseManager.Instance.ClearSelected();
        target = null;
        OnEnd();
    }

    public virtual void SetTarget(GameObject newTarget)
    {
        target = newTarget;
    }
}
