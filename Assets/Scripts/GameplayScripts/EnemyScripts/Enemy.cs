using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public List<Zone> zones = new List<Zone>();
    [HideInInspector] public CardContainer hand;

    public UnitManager unitManager;

    public int deckSize = 0;


    public static Enemy Instance { get; private set; }
    // Start is called before the first frame update
    void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }

        hand = GetComponent<CardContainer>();

        unitManager = new UnitManager(zones);
    }
}
