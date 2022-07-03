using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Photon.Pun;

public class GameAction : MonoBehaviour
{
    protected bool done = false;
    public bool Completed => done;

    protected bool interrupted = false;

    public int priority = 0;

    public bool sendAction = true;

    public virtual void PerformGameAction() 
    {
        sendAction = true;
        OnStart();
        StartCoroutine("ActionCoroutine");
    }

    public virtual IEnumerator ActionCoroutine() { OnEnd();  yield break; }

    public virtual void OnStart() {
        done = false;
        OnActionStart?.Invoke();
    }
    public virtual void OnEnd() {
        done = true;
        interrupted = false;
        OnActionDone?.Invoke();
    }

    public void Interrupt() 
    {
        interrupted = true;
    } 

    public Action OnActionStart;
    public Action OnActionDone;
}
