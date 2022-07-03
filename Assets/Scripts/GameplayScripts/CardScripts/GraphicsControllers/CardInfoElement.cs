using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CardInfoElement : MonoBehaviour
{
    public float height;

    public TextMeshPro regularText;
    public TextMeshProUGUI uiText;

    public bool HasInfo => ElementHasInfo();

    public bool ElementHasInfo()
    {
        bool regularInfo = regularText != null && regularText.text != "";
        bool uiInfo = uiText != null && uiText.text != "";

        return regularInfo || uiInfo;
    }

    public void SetRegularText(string text)
    {
        if(regularText != null)
        {
            regularText.text = text;
        }
    }

    public void SetUIText(string text)
    {
        if(uiText != null)
        {
            uiText.text = text;
        }
    }
}
