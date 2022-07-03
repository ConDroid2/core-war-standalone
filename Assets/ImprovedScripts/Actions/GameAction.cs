using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace SequenceSystem
{
    public class GameAction
    {
        protected bool done = false;
        protected bool interrupted = false;
        public bool sendAction = true;
        public GameObject gameObject = null;

        public bool Completed => done; 

        public event Action OnActionStart;
        public event Action OnActionEnd;

        public delegate void SetActionPipeData(GameAction action);
        public SetActionPipeData setActionPipeData = null;

        public GameAction() 
        {
     
        }
        public GameAction(GameObject gameObject)
        {
            this.gameObject = gameObject;
        }

        public GameAction(GameAction template)
        {
            // Need things listening to the events
            OnActionStart = template.OnActionStart;
            OnActionEnd = template.OnActionEnd;
            setActionPipeData = template.setActionPipeData;
        }

        public virtual void StartGameAction()
        {
            sendAction = true;
            OnStart();
            PerformGameAction();
        }

        public virtual void PerformGameAction()
        {
            OnEnd();
        }

        public virtual void OnStart()
        {
            done = false;
            OnActionStart?.Invoke();
        }

        public virtual void OnEnd()
        {
            done = true;
            if(setActionPipeData == null)
            {
                setActionPipeData = DefaultSetActionPipeData;
            }
            setActionPipeData(this);
            OnActionEnd?.Invoke();
        }

        public virtual void Interrupt()
        {
            interrupted = true;
        }

        public void DefaultSetActionPipeData(GameAction action)
        {
            return;
        }
    }
}
