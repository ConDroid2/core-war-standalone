using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SequenceSystem 
{
    public class AddBuffToken : Ability
    {
        UnitController card;
        int strength;
        int resilience;
        bool temp;

        public AddBuffToken(UnitController card, int strength, int resilience, bool temp)
        {
            this.card = card;
            this.strength = strength;
            this.resilience = resilience;
            this.temp = temp;
        }

        public AddBuffToken(AddBuffToken template)
        {
            card = template.card;
            strength = template.strength;
            resilience = template.resilience;
            temp = template.temp;
        }

        public override void PerformGameAction()
        {
            if (temp)
            {
                card.Additions.AddTempCounter(new BuffCounter(card, strength, resilience));
            }
            else
            {
                card.Additions.AddPermanentCounter(new BuffCounter(card, strength, resilience));
            }

            OnEnd();
        }
    }
}
