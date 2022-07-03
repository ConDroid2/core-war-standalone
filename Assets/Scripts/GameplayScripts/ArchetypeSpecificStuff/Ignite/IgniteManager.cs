using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class IgniteManager : MonoBehaviour
{
    public int igniteCount = 0;
    public bool resetIgnite = true;

    public Action<int> IgniteCountChange;

    public static IgniteManager Instance;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }

        Player.Instance.spellManager.OnSpellCast += IncreaseIgniteCount;
        Player.Instance.OnEndTurn += ResetIgniteCountGraphics;
        Player.Instance.OnStartTurn += ResetIgniteCount;
        gameObject.SetActive(false);
    }

    // Used for when a spell is played
    public void IncreaseIgniteCount()
    {
        igniteCount++;
        IgniteCountChange?.Invoke(igniteCount);
        gameObject.SetActive(true);
    }

    // Used for cards that increase ignite count as an ability
    public void IncreaseIgniteCount(int amount)
    {
        igniteCount += amount;
        IgniteCountChange?.Invoke(igniteCount);
        gameObject.SetActive(true);
    }

    public void ResetIgniteCount()
    {
        if (resetIgnite)
        {
            igniteCount = 0;
            IgniteCountChange?.Invoke(igniteCount);
        }
    }

    public void ResetIgniteCountGraphics()
    {
        if (resetIgnite)
        {
            //igniteCount = 0;
            //IgniteCountChange?.Invoke(igniteCount);
            gameObject.SetActive(false);
        }   
    }
}
