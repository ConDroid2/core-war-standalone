using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using Photon.Pun;

public abstract class TargetedAction : GameAction
{
    protected Type targetType;
    protected Component target;

    public virtual bool CheckTarget(GameObject targetToCheck) 
    {
        if(targetToCheck.GetComponent(targetType))
        {
            return true;
        }

        return false;
    }

    public virtual void SetTarget(int newTargetID) 
    {
        Debug.Log("Target ID: " + newTargetID + " - Target: " + PhotonView.Find(newTargetID).name);
        target = PhotonView.Find(newTargetID).gameObject.GetComponent(targetType);
    }
}
