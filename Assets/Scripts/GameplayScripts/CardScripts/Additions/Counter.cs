using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Counter
{
    protected CardParent card;
    
    public Counter(CardParent card)
    {
        this.card = card;
    }

    public virtual void AddCounter()
    {
        return;
    }

    public virtual void RemoveCounter()
    {
        return;
    }
}

public class BuffCounter : Counter
{
    int strength;
    int resilience;
    public BuffCounter(CardParent card, int strength, int resilience) : base(card) 
    {
        this.strength = strength;
        this.resilience = resilience;
    }

    public override void AddCounter()
    {
        (card as UnitController).IncreaseMaxResilience(resilience);
        (card as UnitController).IncreaseDamage(strength );
    }

    public override void RemoveCounter()
    {
        (card as UnitController).IncreaseMaxResilience(resilience * -1);
        (card as UnitController).IncreaseDamage(strength * -1);
    }
}
