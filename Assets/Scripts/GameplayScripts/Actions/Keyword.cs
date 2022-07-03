using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Keyword : MonoBehaviour
{
    public virtual void RemoveKeyword() 
    {
        CardParent card = GetComponent<CardParent>();
        if (card.cardData.keywords.Contains(GetType().ToString()))
        {
            card.cardData.keywords.Remove(GetType().ToString());
        }
        Destroy(this);
    }
}
