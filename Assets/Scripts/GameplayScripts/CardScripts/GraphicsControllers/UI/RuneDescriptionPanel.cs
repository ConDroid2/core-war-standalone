using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RuneDescriptionPanel : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI runeText;
    [SerializeField] private GameObject panel;

    public void FillInRuneInfo(Card card)
    {
        string descriptions = "";

        foreach(string rune in card.keywords)
        {
            if (RuneUtilities.runeDescriptions.ContainsKey(rune))
            {
                descriptions += ("<b>" + rune + "</b> - " + RuneUtilities.runeDescriptions[rune] + "<br><br>");
            }
        }

        if (descriptions == "") panel.SetActive(false);
        else panel.SetActive(true);

        runeText.text = descriptions;
    }
}
