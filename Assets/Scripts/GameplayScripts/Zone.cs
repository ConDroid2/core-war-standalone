using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Zone : MonoBehaviour
{
    public CardContainer cards;
    [HideInInspector] public int cardCapacity = 5;
    [SerializeField] private float cardDist; // The distance between cards
    [SerializeField] private Vector3 cardZPosDif = new Vector3(0f, 0f, 0.01f);

    [HideInInspector] public MeshRenderer renderer;
    [SerializeField] private Material normalMaterial;
    [SerializeField] private Material overloadedMaterial;
    public GameObject discardNotification;

    public int zoneNum = 0;
    public Zone nextZone = null;
    public Zone prevZone = null;

    protected virtual void Awake() 
    {
        cards = GetComponent<CardContainer>();
        renderer = GetComponent<MeshRenderer>();
    }
    /** Functional functions **/
    //AddCard function that will add the card
    // Change name to something like "PlayCardToZone"
    public virtual Vector3 AddCard(InPlayCardController card) 
    {
        // Check if there will be more cards than card cap once this card is added
        if (cards.Count + 1 > cardCapacity)
        {
            renderer.material = overloadedMaterial;
        }

        return cards.AddCard(card);
        
    }

    public virtual void RemoveCard(InPlayCardController card) 
    {
        cards.RemoveCard(card);

        if(cards.Count <= cardCapacity)
        {
            renderer.material = normalMaterial;
        }
    }
}
