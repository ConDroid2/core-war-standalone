using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Ignite
{
    public AbilityCondition baseAbility;
    public AbilityCondition ignitedAbility;
    CardController card;
    int count;
    bool ignited;

    public static Action OnIgnitedSpellPlayed;

    public Ignite(CardController card, int count, AbilityCondition baseAbility, AbilityCondition ignitedAbility)
    {
        SetUpIgnite(card, count, baseAbility, ignitedAbility);   
    }

    public Ignite(CardController card, int count)
    {
        baseAbility = new OnPlay(card);
        ignitedAbility = new OnPlay(card);
        this.card = card;
        SetUpIgnite(card, count, baseAbility, ignitedAbility);
    }

    public void SetUpIgnite(CardController card, int count, AbilityCondition baseAbility, AbilityCondition ignitedAbility)
    {
        this.card = card;
        if (card.photonView.IsMine)
        {
            this.count = count;
            this.baseAbility = baseAbility;
            this.ignitedAbility = ignitedAbility;
            ignited = false;

            ignitedAbility.Delete();
            HandleIgniteChanged(IgniteManager.Instance.igniteCount);
            IgniteManager.Instance.IgniteCountChange += HandleIgniteChanged;

            card.GetComponent<CardScript>().OnDeathEvent += turnOff;
            card.OnPlay += PlayIgnitedSpell;
        }
    }

    public void turnOff()
    {
        if (card.photonView.IsMine)
        {
            Debug.Log("Turning off ignite");
            IgniteManager.Instance.IgniteCountChange -= HandleIgniteChanged;
            card.GetComponent<CardScript>().OnDeathEvent -= turnOff;
            baseAbility.Delete();
            ignitedAbility.Delete();
        }       
    }

    public void HandleIgniteChanged(int newCount)
    {
        if (card == null) return;
        if(!ignited && newCount >= count)
        {
            baseAbility.Delete();
            ignitedAbility.SetUp();

            ignited = true;

            if(card.selectable != null)
                card.selectable.material = card.ignitedMat;    
        }

        else if(ignited && newCount < count)
        {
            ignitedAbility.Delete();
            baseAbility.SetUp();

            ignited = false;

            Debug.Log("Switching material for: " + card.name);
            card.selectable.material = card.normalMat;    
        }
    }

    public void PlayIgnitedSpell()
    {
        if (ignited)
        {
            OnIgnitedSpellPlayed?.Invoke();
        }    
    }
}
