using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Additions : MonoBehaviour
{
    List<Counter> counters = new List<Counter>();
    List<Counter> untilTurnEndCounters = new List<Counter>();

    private void Awake()
    {
        Player.Instance.OnEndTurn += HandleTurnEnd;
    }

    public void AddPermanentCounter(Counter counter)
    {
        counters.Add(counter);
        counter.AddCounter();
    }

    public void AddTempCounter(Counter counter)
    {
        untilTurnEndCounters.Add(counter);
        counter.AddCounter();
    }

    public void HandleTurnEnd()
    {
        foreach(Counter counter in untilTurnEndCounters)
        {
            counter.RemoveCounter();
        }

        untilTurnEndCounters.Clear();
    }
}
