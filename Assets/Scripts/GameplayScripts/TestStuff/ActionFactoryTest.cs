using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ActionFactoryTest : MonoBehaviour
{
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space)){
            Child firstAction = new Child(3, "Noice");
            Debug.Log("First Action");
            firstAction.Print();

            var type = firstAction.GetType();

            object secondAction = Activator.CreateInstance(type, firstAction);
            Debug.Log("Second Action");
            (secondAction as Action).Print();
        }
    }

    public class Action
    {
        protected int number;

        public Action() { }
        public Action(int number)
        {
            this.number = number;
        }

        public Action(Action action)
        {
            number = action.number;
        }

        public virtual void Print()
        {
            Debug.Log(number);
        }
    }

    public class Child : Action
    {
        private string word;

        public Child(int number, string word)
        {
            this.number = number;
            this.word = word;
        }

        public Child(Child child)
        {
            number = child.number;
            word = child.word;
        }

        public override void Print()
        {
            Debug.Log(word + " " + number);
        }
    }

    
}
