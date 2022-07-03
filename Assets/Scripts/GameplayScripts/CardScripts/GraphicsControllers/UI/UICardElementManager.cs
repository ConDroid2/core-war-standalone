using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UICardElementManager : MonoBehaviour
{
    List<CardInfoElement> elements = new List<CardInfoElement>();

    public CardInfoElement cardName;
    public CardInfoElement subtype;
    public CardInfoElement keywords;
    public CardInfoElement description;

    public RectTransform rectTransform;
    public Vector3 startingPos;

    public void SetUp()
    {
        rectTransform = GetComponent<RectTransform>();
        elements.Add(cardName);
        elements.Add(subtype);
        elements.Add(keywords);
        elements.Add(description);
    }

    public void UpdatePos()
    {
        Vector2 nextPos = new Vector2(0, startingPos.y);
        for (int i = elements.Count - 1; i >= 0; i--)
        {
            if (elements[i] != null && elements[i].HasInfo)
            {
                elements[i].GetComponent<RectTransform>().anchoredPosition = nextPos;
                // Debug.Log("Elements pos is " + elements[i].GetComponent<RectTransform>().anchoredPosition);

                nextPos.y += elements[i].height;
            }
        }
    }
}
