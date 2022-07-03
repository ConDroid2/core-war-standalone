using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cull : CardScript
{
    public override void InHandSetUp()
    {
        CardController card = GetComponent<CardController>();
        OnPlay onPlay = new OnPlay(card);
        conditions.Add(onPlay);

        CullAction cull = new CullAction();
        onPlay.AddAbility(cull);
    }

    public class CullAction : SequenceSystem.Ability
    {
        public CullAction() { }
        public CullAction(CullAction template) { }
        public override void PerformGameAction()
        {
            List<CardParent> potentialTargets = CardSelector.GetCards(zoneFilter: CardSelector.ZoneFilter.All, typeFilter: CardSelector.TypeFilter.Unit);

            // Find lowest cost
            int lowestCost = 100;

            foreach(CardParent card in potentialTargets)
            {
                if(card.cardData.GetTotalCost() < lowestCost)
                {
                    lowestCost = card.cardData.GetTotalCost();
                }
            }

            // Remove all cards that are not that low cost
            potentialTargets.RemoveAll((card) =>  card.cardData.GetTotalCost() != lowestCost);

            int randomIndex = Random.Range(0, potentialTargets.Count);

            SequenceSystem.DestroyCard destroy = new SequenceSystem.DestroyCard();
            destroy.SetTarget(potentialTargets[randomIndex].gameObject);
            MainSequenceManager.Instance.Add(destroy);
            OnEnd();    
        }
    }
}
