using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class MultipleChoiceManager : MonoBehaviour
{
    public delegate void ButtonFunc();

    [SerializeField] private List<Button> choiceButtons;
    [SerializeField] private List<TextMeshProUGUI> choiceTexts;
    
    public void SetChoice(int choice, ButtonFunc choiceFunction, string choiceText)
    {
        choiceTexts[choice].text = choiceText;
        choiceButtons[choice].onClick.RemoveAllListeners();
        choiceButtons[choice].onClick.AddListener(() => choiceFunction());
    }
}
