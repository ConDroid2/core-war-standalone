using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetSpecific : Ability
{
    public List<Ability> abilities = new List<Ability>();

    string cardName = "";
    CardSelector.HandFilter handFilter;
    CardSelector.ZoneFilter zoneFilter;

    public void Initialize(string cardName = "", CardSelector.ZoneFilter zoneFilter = CardSelector.ZoneFilter.None, CardSelector.HandFilter handFilter = CardSelector.HandFilter.None)
    {
        this.cardName = cardName;
        this.handFilter = handFilter;
        this.zoneFilter = zoneFilter;
    }

    public override IEnumerator ActionCoroutine()
    {
        List<CardParent> cards = CardSelector.GetSpecificCard(cardName, zoneFilter, handFilter);

        ActionSequencer sequence = new ActionSequencer();

        foreach (CardParent card in cards)
        {
            foreach (TargetedAbility ability in abilities)
            {
                ability.SetTarget(card.gameObject);
                sequence.AddGameAction(ability);
            }

            yield return StartCoroutine(sequence.RunSequence());
        }

        OnEnd();
    }
}
