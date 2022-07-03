using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Reflection;
using System.Linq;

public class StatusFactory : MonoBehaviour
{
    public Dictionary<string, Type> stringToStatus;

    public static StatusFactory Instance;

    public void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            InitializeFactory();
        }
    }

    private void InitializeFactory()
    {
        var statuses = Assembly.GetAssembly(typeof(Status)).GetTypes()
            .Where(myType => myType.IsClass && !myType.IsAbstract && myType.IsSubclassOf(typeof(Status)));

        stringToStatus = new Dictionary<string, Type>();

        foreach (var type in statuses)
        {
            stringToStatus.Add(type.Name, type);
        }
    }

    public Status AddStatus(string status, int count, UnitController unit)
    {
        Status addedStatus = null;

        if (stringToStatus.ContainsKey(status))
        {
            if(addedStatus = unit.GetComponent(stringToStatus[status]) as Status)
            {
                if(count < addedStatus.turnCount)
                {
                    count = addedStatus.turnCount;
                }
            }
            else
            {
                addedStatus = unit.gameObject.AddComponent(stringToStatus[status]) as Status;
                unit.cardData.activeStatuses.Add(status);
            }
            
            addedStatus.SetStatusCount(count);
        }

        return addedStatus;
    }

    public void RemoveStatus(string status, UnitController unit)
    {
        Type statusType = stringToStatus[status];
        Status statusObject = unit.GetComponent(statusType) as Status;

        if(statusObject != null)
        {
            statusObject.RemoveStatus();
        }
    }
}
