using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameActionQueue
{
    private List<GameAction> queue;
    public int Count => queue.Count;
    
    public GameActionQueue() 
    {
        queue = new List<GameAction>();
    }

    public void Enqueue(GameAction action) 
    {
        if (queue.Count == 0)
        {
            queue.Add(action);
        } 
        else
        {
            for(int i = 0; i < queue.Count; i++)
            {
                if(action.priority >= queue[i].priority)
                {
                    queue.Insert(i, action);
                    return;
                }
            }
        }
    }

    public GameAction Dequeue() 
    {
        GameAction action = queue[0];
        queue.RemoveAt(0);
        return action;
    }
}
