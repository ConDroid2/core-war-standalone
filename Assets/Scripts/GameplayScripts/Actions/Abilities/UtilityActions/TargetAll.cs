using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetAll : Ability
{
    public List<TargetedAbility> abilities = new List<TargetedAbility>();
    public bool excludeMe = false;

    protected CardSelector.HandFilter handFilter;
    protected CardSelector.ZoneFilter zoneFilter;
    protected CardSelector.TypeFilter typeFilter;
    protected int costFilter = -1;
    protected CardSelector.CostCompare costCompare;
    protected CardSelector.OriginatingCard originatingCard;
    protected string subtypeFilter;
    protected string keywordFilter;
    protected string statusFilter;

    public void Initialize(CardSelector.HandFilter handFilter = CardSelector.HandFilter.None,
        CardSelector.ZoneFilter zoneFilter = CardSelector.ZoneFilter.None,
        CardSelector.TypeFilter typeFilter = CardSelector.TypeFilter.All,
        int costFilter = -1,
        CardSelector.CostCompare costCompare = CardSelector.CostCompare.EqualTo,
        CardSelector.OriginatingCard originatingCard = CardSelector.OriginatingCard.Default,
        string subtypeFilter = "",
        string keywordFilter = "",
        string statusFilter = "")
    {
        this.handFilter = handFilter;
        this.zoneFilter = zoneFilter;
        this.typeFilter = typeFilter;
        this.costFilter = costFilter;
        this.costCompare = costCompare;
        this.originatingCard = originatingCard;
        this.subtypeFilter = subtypeFilter;
        this.keywordFilter = keywordFilter;
        this.statusFilter = statusFilter;
    }

    public override IEnumerator ActionCoroutine()
    {
        List<CardParent> cards = CardSelector.GetCards(handFilter, zoneFilter, typeFilter, costFilter, costCompare, originatingCard, subtypeFilter, keywordFilter, statusFilter);

        if (excludeMe)
        {
            CardParent me = GetComponent<CardParent>();
            if (cards.Contains(me))
            {
                cards.Remove(me);
            }
        }

        ActionSequencer sequence = new ActionSequencer();

        foreach(CardParent card in cards)
        {
            foreach(TargetedAbility ability in abilities)
            {
                ability.SetTarget(card.gameObject);
                sequence.AddGameAction(ability);
            }

            yield return StartCoroutine(sequence.RunSequence());
        }

        OnEnd();
    }
}
