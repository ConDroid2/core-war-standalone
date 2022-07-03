using System.Collections.Generic;
using UnityEngine;

public class ActionSequenceRunner : MonoBehaviour
{
    ActionSequencer actionSequence = new ActionSequencer();

    public void Add(SequenceSystem.GameAction action)
    {
        actionSequence.AddGameAction(action);

        if (!actionSequence.IsRunning)
        {
            StartCoroutine(actionSequence.RunSequence());
        }
    }

    public void Add(IEnumerable<SequenceSystem.GameAction> actions)
    {
        actionSequence.AddGameAction(actions);

        if (!actionSequence.IsRunning)
        {
            StartCoroutine(actionSequence.RunSequence());
        }
    }
}
