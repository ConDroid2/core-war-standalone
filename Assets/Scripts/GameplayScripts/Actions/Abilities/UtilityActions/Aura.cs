using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Aura : Ability
{
    private TargetedAbility addAura;
    private TargetedAbility removeAura;
    private bool isMine;

    private CardSelector.HandFilter handFilter;
    private CardSelector.ZoneFilter zoneFilter;
    private CardSelector.TypeFilter typeFilter;

    public delegate bool CheckIfValidTarget(CardParent card);
    private CheckIfValidTarget checkTarget;

    public void Initialize(
        TargetedAbility addAura,
        TargetedAbility removeAura,
        CheckIfValidTarget checkIfValidTarget,
        bool isMine,
        CardSelector.HandFilter handFilter = CardSelector.HandFilter.None,
        CardSelector.ZoneFilter zoneFilter = CardSelector.ZoneFilter.None,
        CardSelector.TypeFilter typeFilter = CardSelector.TypeFilter.All
        )
    {
        this.addAura = addAura;
        this.removeAura = removeAura;
        checkTarget = checkIfValidTarget;
        this.handFilter = handFilter;
        this.zoneFilter = zoneFilter;
        this.typeFilter = typeFilter;
        this.isMine = isMine;
    }

    public override IEnumerator ActionCoroutine()
    {
        List<CardParent> cards = CardSelector.GetCards(handFilter: handFilter, zoneFilter: zoneFilter, typeFilter: typeFilter);
        cards.RemoveAll((card) => !checkTarget(card));

        ActionSequencer sequence = new ActionSequencer();

        foreach(CardParent card in cards)
        {
            addAura.SetTarget(card.gameObject);
            sequence.AddGameAction(addAura);
            yield return StartCoroutine(sequence.RunSequence());
        }

        if(handFilter == CardSelector.HandFilter.EnemyHand || handFilter == CardSelector.HandFilter.All)
        {      
            Enemy.Instance.hand.OnCardAdded += ApplyAura;
        }
        if(handFilter == CardSelector.HandFilter.MyHand || handFilter == CardSelector.HandFilter.All)
        {
            Player.Instance.hand.OnCardAdded += ApplyAura;
        }

        if(typeFilter == CardSelector.TypeFilter.Unit && (zoneFilter == CardSelector.ZoneFilter.EnemyZones || zoneFilter == CardSelector.ZoneFilter.All))
        {
            Enemy.Instance.unitManager.OnUnitAdded += ApplyAura;
        }
        else if(typeFilter == CardSelector.TypeFilter.Unit && (zoneFilter == CardSelector.ZoneFilter.MyZones || zoneFilter == CardSelector.ZoneFilter.All))
        {
            Player.Instance.unitManager.OnUnitAdded += ApplyAura;
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
            if (checkTarget(card) && matchesType)
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
            List<CardParent> cards = CardSelector.GetCards(handFilter: handFilter, zoneFilter: zoneFilter, typeFilter: typeFilter);
            cards.RemoveAll((card) => !checkTarget(card));

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
}
