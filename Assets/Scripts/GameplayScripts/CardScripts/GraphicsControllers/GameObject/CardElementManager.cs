using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardElementManager : MonoBehaviour
{
    List<CardInfoElement> elements = new List<CardInfoElement>();

    public CardInfoElement cardName;
    public CardInfoElement subtype;
    public CardInfoElement keywords;
    public CardInfoElement description;

    public Vector3 startingPos;

    public void SetUp()
    {
        elements.Add(cardName);
        elements.Add(subtype);
        elements.Add(keywords);
        elements.Add(description);
    }

    public void UpdatePos()
    {
        Vector3 nextPos = new Vector3(transform.position.x, transform.position.y + startingPos.y, transform.position.z);
        for(int i = elements.Count - 1; i >= 0; i--)
        {
            if (elements[i] != null && elements[i].HasInfo)
            {
                elements[i].transform.position = nextPos;

                nextPos.y += elements[i].height;
            }     
        }
    }
}
