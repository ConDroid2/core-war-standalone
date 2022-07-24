using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnderworldManager : MonoBehaviour
{
    public List<InPlayCardController> souls = new List<InPlayCardController>();
    [SerializeField] private Transform underworldPosition;

    public static UnderworldManager Instance { get; private set; }

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
    }


    public void AddToUnderworld(InPlayCardController card)
    {
        souls.Add(card);

        card.transform.position = underworldPosition.position;

        card.transform.localScale = new Vector3(1.5f, 1.5f, 1f);

        card.GetComponent<CardScript>().InPlayDeath();
    }

    public void ExorciseFromUnderworld(InPlayCardController card)
    {
        if (souls.Contains(card))
        {
            souls.Remove(card);
        }

        if(card.cardData.script != "")
        {
            card.GetComponent<CardScript>().InPlayDeath();
        }

        // Probably delete card here
    }


}
