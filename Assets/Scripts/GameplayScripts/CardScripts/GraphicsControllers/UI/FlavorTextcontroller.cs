using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FlavorTextcontroller : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI flavorText;
    [SerializeField] private GameObject textObject;

    public void HandleInspected(Card card)
    {
        if(card.flavorText == "")
        {
            textObject.SetActive(false);
        }
        else
        {
            flavorText.text = card.flavorText;
            textObject.SetActive(true);
        }     
    }
}
