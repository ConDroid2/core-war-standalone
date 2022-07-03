using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SequenceSystem 
{
    public class MultipleChoiceWrapper : GameAction
    {
        Ability choiceOne;
        string choiceOneText;
        Ability choiceTwo;
        string choiceTwoText;

        MultipleChoiceManager manager;

        public MultipleChoiceWrapper(Ability choiceOne, string choiceOneText, Ability choiceTwo, string choiceTwoText)
        {
            this.choiceOne = choiceOne;
            this.choiceTwo = choiceTwo;
            this.choiceOneText = choiceOneText;
            this.choiceTwoText = choiceTwoText;
        }

        public MultipleChoiceWrapper(MultipleChoiceWrapper template)
        {
            choiceOne = template.choiceOne;
            choiceTwo = template.choiceTwo;
            choiceOneText = template.choiceOneText;
            choiceTwoText = template.choiceTwoText;

            manager = Resources.FindObjectsOfTypeAll<MultipleChoiceManager>()[0];
        }

        public override void PerformGameAction()
        {
            // GameObject managerObject = GameObject.Find("MultipleChoice");
            manager.SetChoice(0, ChooseOptionOne, choiceOneText);
            manager.SetChoice(1, ChooseOptionTwo, choiceTwoText);

            manager.gameObject.SetActive(true);
        }

        public void ChooseOptionOne()
        {
            MainSequenceManager.Instance.AddNext(choiceOne);
            OnEnd();
            manager.gameObject.SetActive(false);
        }

        public void ChooseOptionTwo()
        {
            MainSequenceManager.Instance.AddNext(choiceTwo);
            OnEnd();
            manager.gameObject.SetActive(false);
        }
    }
}
