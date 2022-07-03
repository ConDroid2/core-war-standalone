using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestinyOfConquest : CardScript
{
    public override void InHandSetUp()
    {
        CardController card = GetComponent<CardController>();
        OnPlay onPlay = new OnPlay(card);
        conditions.Add(onPlay);

        DestinyOfConquestSummon summon = new DestinyOfConquestSummon(card);

        onPlay.AddAbility(summon);
    }

    public class DestinyOfConquestSummon : SequenceSystem.Ability
    {
        CardController card;
        public DestinyOfConquestSummon(CardController card)
        {
            this.card = card;
        }

        public DestinyOfConquestSummon(DestinyOfConquestSummon template)
        {
            card = template.card;
        }

        public override void PerformGameAction()
        {
            int amount;

            if (Player.Instance.spellManager.spellsThisGame.ContainsKey("Destiny of Conquest"))
            {
                amount = Player.Instance.spellManager.spellsThisGame["Destiny of Conquest"];
            }
            else
            {
                amount = 1;
            }

            SequenceSystem.Summon summon = new SequenceSystem.Summon(amount, "Soldier", card.gameObject);

            MainSequenceManager.Instance.Add(summon);

            OnEnd();
        }
    }
}
