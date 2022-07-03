using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawRandomCards : Ability
{
    CardSelector.TypeFilter typeFilter;
    int costFilter;
    CardSelector.CostCompare costCompare;
    string subtypeFilter;
    string keywordFilter;
    int amount;

    public void Initialize(int amount = 0,
    CardSelector.TypeFilter typeFilter = CardSelector.TypeFilter.All,
    int costFilter = -1,
    CardSelector.CostCompare costCompare = CardSelector.CostCompare.EqualTo,
    string subtypeFilter = "",
    string keywordFilter = "")
    {
        this.amount = amount;
        this.typeFilter = typeFilter;
        this.costFilter = costFilter;
        this.costCompare = costCompare;
        this.subtypeFilter = subtypeFilter;
        this.keywordFilter = keywordFilter;
    }

    public override IEnumerator ActionCoroutine()
    {
        List<Card> potentialCards = CardSelector.GetCardsFromDeck(typeFilter, costFilter, costCompare, subtypeFilter, keywordFilter);
        ActionSequencer sequence = new ActionSequencer();
        PlayerDraw draw = Player.Instance.GetComponent<PlayerDraw>();

        for (int i = 0; i < amount; i++)
        {
            if (potentialCards.Count > 0)
            {
                int randomIndex = Random.Range(0, potentialCards.Count);
                Card spell = potentialCards[randomIndex];

                draw.SetSpecificCard(spell);

                sequence.AddGameAction(draw);

                yield return StartCoroutine(sequence.RunSequence());
            }
        }

        OnEnd();
    }
}
