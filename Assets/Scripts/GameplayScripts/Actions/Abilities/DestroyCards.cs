using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyCards : Ability
{
    private List<CardParent> cards;
    private CardSelector.ZoneFilter zoneFilter;
    private CardSelector.TypeFilter typeFilter;
    private int costFilter;
    private CardSelector.CostCompare costCompare;

    public override IEnumerator ActionCoroutine()
    {
        cards = CardSelector.GetCards(zoneFilter: zoneFilter, typeFilter: typeFilter, costFilter: costFilter, costCompare: costCompare);

        ActionSequencer sequence = new ActionSequencer();
        foreach (CardParent card in cards)
        {
            sequence.AddGameAction(card.GetComponent<Die>());
        }

        yield return StartCoroutine(sequence.RunSequence());

        OnEnd();
    }

    public void Initialize(
        CardSelector.ZoneFilter zoneFilter = CardSelector.ZoneFilter.None,
        CardSelector.TypeFilter typeFilter = CardSelector.TypeFilter.All,
        int costFilter = -1,
        CardSelector.CostCompare costCompare = CardSelector.CostCompare.EqualTo)
    {
        this.zoneFilter = zoneFilter;
        this.typeFilter = typeFilter;
        this.costFilter = costFilter;
        this.costCompare = costCompare;
    }
}
