using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ActionSequencer
{
    // private Queue<SequenceSystem.GameAction> sequence;
    private LinkedList<SequenceSystem.GameAction> list;
    private object[] PipeData;
    
    private bool running = false;
    public bool IsRunning => running;
    public int Count => list.Count;
    public SequenceSystem.GameAction currentAction = null;

    public event Action<Type> OnActionAddedToQueue;
    public ActionSequencer() 
    {
        // sequence = new Queue<GameAction>();
        list = new LinkedList<SequenceSystem.GameAction>();
    }

    public IEnumerator RunSequence() 
    {
        running = true;
        while (list.Count > 0)
        {
            currentAction = list.First.Value;
            list.RemoveFirst();
            if(currentAction != null)
            { 
                currentAction.StartGameAction();
                while (!currentAction.Completed)
                {
                    yield return null;
                }
                
            }
            else
            {
                Debug.Log("Attempted to run an action that was null");
            }
            currentAction = null;
        }
        running = false;
    }

    public void AddGameAction(SequenceSystem.GameAction template) 
    {
        OnActionAddedToQueue?.Invoke(template.GetType());
        if(template is NonTemplateAction)
        {
            list.AddLast(template);
        }
        else
        {
            var type = template.GetType();
            object action = Activator.CreateInstance(type, template);

            list.AddLast(action as SequenceSystem.GameAction);
        }   
    }

    public void AddGameActionFirst(SequenceSystem.GameAction template)
    {
        OnActionAddedToQueue?.Invoke(template.GetType());
        if (template is NonTemplateAction)
        {
            list.AddFirst(template);
        }
        else
        {
            var type = template.GetType();
            object action = Activator.CreateInstance(type, template);

            list.AddFirst(action as SequenceSystem.GameAction);
        }       
    }

    // Allow a list to be added to the sequence
    public void AddGameAction(IEnumerable<SequenceSystem.GameAction> actions) 
    {
        foreach(SequenceSystem.GameAction action in actions)
        {
            AddGameAction(action);
        }
    }

    public void ClearSequencer()
    {
        list.Clear();
    }

    public void SetPipeData(object[] data)
    {
        PipeData = data;
    }

    public object[] GetPipeData()
    {
        return PipeData;
    }

    public LinkedList<SequenceSystem.GameAction> GetSequence()
    {
        return list;
    }
}
