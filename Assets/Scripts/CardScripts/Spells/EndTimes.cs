using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndTimes : CardScript
{
    public override void InHandSetUp()
    {
        OnPlay onPlay = new OnPlay(GetComponent<CardController>());
        conditions.Add(onPlay);

        EndTimesAction action = new EndTimesAction();
        onPlay.AddAbility(action);
    }

    public class EndTimesAction : SequenceSystem.Ability
    {
        public EndTimesAction() { }
        public EndTimesAction(EndTimesAction template) { }
        public override void PerformGameAction()
        {
            while (ProphecyManager.Instance.prophecies.Count > 0)
            {
                Prophecized card = ProphecyManager.Instance.prophecies[0];
                card.Fulfill();
            }

            OnEnd();
        }
    } 
}
