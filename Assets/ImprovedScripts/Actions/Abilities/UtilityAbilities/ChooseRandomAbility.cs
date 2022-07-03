using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SequenceSystem 
{
    public class ChooseRandomAbility : Ability
    {
        List<Ability> abilities = new List<Ability>();

        public ChooseRandomAbility() { }

        public ChooseRandomAbility(ChooseRandomAbility template)
        {
            foreach(Ability ability in template.abilities)
            {
                AddAbility(ability);
            }
        }

        public override void PerformGameAction()
        {
            int randomIndex = Random.Range(0, abilities.Count);

            MainSequenceManager.Instance.Add(abilities[randomIndex]);

            OnEnd();
        }

        public void AddAbility(Ability ability)
        {
            abilities.Add(ability);
        }
    }
}
