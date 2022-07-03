using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SequenceSystem 
{
    public class ConditionalWrapper : Ability
    {
        public delegate bool CheckCondition(IntInput left, IntInput right);
        private List<Ability> abilities = new List<Ability>();
        private List<Ability> elseAbilities = new List<Ability>();

        private IntInput leftInput;
        private IntInput rightInput;
        private string comparisonKey;

        private Dictionary<string, CheckCondition> comparisonDictionary = new Dictionary<string, CheckCondition>
        {
            {"<", (left, right) => left.Value < right.Value },
            {">", (left, right) => left.Value > right.Value },
            {"==", (left, right) => left.Value == right.Value }
        };

        public ConditionalWrapper(IntInput leftInput, string comparisonKey, IntInput rightInput)
        {
            this.leftInput = leftInput;
            this.comparisonKey = comparisonKey;
            this.rightInput = rightInput;
        }

        public ConditionalWrapper(ConditionalWrapper template)
        {
            leftInput = template.leftInput;
            comparisonKey = template.comparisonKey;
            rightInput = template.rightInput;

            foreach(Ability ability in template.abilities)
            {
                AddAbility(ability);
            }

            foreach(Ability ability in template.elseAbilities)
            {
                AddElseAbility(ability);
            }
        }

        public override void PerformGameAction()
        {
            if (comparisonDictionary[comparisonKey](leftInput, rightInput))
            {
                MainSequenceManager.Instance.Add(abilities);
            }
            else
            {
                MainSequenceManager.Instance.Add(elseAbilities);
            }

            OnEnd();
        }

        public void AddAbility(Ability ability)
        {
            abilities.Add(ability);
        }

        public void AddElseAbility(Ability ability)
        {
            elseAbilities.Add(ability);
        }
    }
}
