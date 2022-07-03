using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;

public class TargetMultiple : Target
{
    public List<GameObject> targets = new List<GameObject>();
    private bool doneSelecting = false;
    public int amountOfTargets = 0; // If 0, that means no limit

    public enum TargetMode { ExactAmount, UpTo, Default };
    public TargetMode mode = TargetMode.Default;

    public override IEnumerator ActionCoroutine()
    {
        // MouseManager.Instance.SetSelected(gameObject, this);

        foreach(CardController card in Player.Instance.hand.cards)
        {
            card.SetCanBePayedFor(false);
            card.currentState = CardController.CardState.InHandNoCount;
        }

        List<CardParent> potentialTargets = CardSelector.GetCards(handFilter, zoneFilter, typeFilter, costFilter, costCompare, originatingCard);

        if (potentialTargets.Count > 0)
        {
            targets.Clear();
            doneSelecting = false;
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

            while (doneSelecting == false)
            {
                yield return null;
            }

            foreach (CardParent card in potentialTargets)
            {
                card.potentialTarget.SetSelectable(false);
            }

            MultiUseButton.Instance.BackToDefault();
            // Create a sequence for the abilities to run in and put them in it
            ActionSequencer targetedSequence = new ActionSequencer();

            foreach (GameObject target in targets)
            {
                foreach(TargetedAbility ability in abilities)
                {
                    ability.SetTarget(target);
                    targetedSequence.AddGameAction(ability);

                    // Run the sequence and wait for it to finish
                    yield return StartCoroutine(targetedSequence.RunSequence());
                }   
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

    public override void SetTarget(GameObject newTarget)
    {
        PotentialTarget potentialTarget = newTarget.GetComponent<PotentialTarget>();

        Debug.Log("Calling set target on target multiple");

        if (targets.Contains(newTarget))
        {
            targets.Remove(newTarget);
            potentialTarget.SetSelected(false);
            
            if(mode == TargetMode.ExactAmount && targets.Count != amountOfTargets)
            {
                MultiUseButton.Instance.SetInteractable(false);
            }
        }
        else
        {
            if(amountOfTargets == 0 || ((mode == TargetMode.UpTo || mode == TargetMode.ExactAmount) && targets.Count < amountOfTargets))
            {
                targets.Add(newTarget);
                potentialTarget.SetSelected(true);
            }         

            if (amountOfTargets > 0 && targets.Count == amountOfTargets && mode == TargetMode.ExactAmount)
            {
                MultiUseButton.Instance.SetInteractable(true);
            }
        }
    }

    public void DoneSelecting()
    {
        doneSelecting = true;
    }
}
