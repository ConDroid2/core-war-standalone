using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class QuoteChooser : MonoBehaviour
{
    [Header("Text boxes")]
    [SerializeField] private List<TextMeshProUGUI> quoteBoxes = new List<TextMeshProUGUI>();
    private List<int> usedIndecies = new List<int>();

    [Header("Quotes")]
    [SerializeField] private List<string> quotes = new List<string>();

    private void Awake()
    {
        foreach(TextMeshProUGUI box in quoteBoxes)
        {
            int index = Random.Range(0, quotes.Count);

            box.text = quotes[index];

            quotes.RemoveAt(index);
        }
    }
}
