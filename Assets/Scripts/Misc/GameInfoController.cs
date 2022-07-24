using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameInfoController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI actionSequencerText;
    [SerializeField] private TextMeshProUGUI underworldText;

    private void OnEnable()
    {
        SetUpActionSequenceText();
        SetUpUnderworldInfo();
    }

    private void SetUpActionSequenceText()
    {
        ActionSequencer sequence = MainSequenceManager.Instance.mainSequence;
        string text = "";

        if(sequence.currentAction != null)
        {
            text += "<b><u>" + TypeToString(sequence.currentAction.GetType()) + "</u></b><br><br>";
        }

        foreach(SequenceSystem.GameAction action in sequence.GetSequence())
        {
            text += "-" + TypeToString(action.GetType()) + "<br><br>";
        }

        actionSequencerText.text = text;
    }

    private string TypeToString(System.Type type)
    {
        if (type.ToString().Contains("SequenceSystem."))
        {
            return type.ToString().Split('.')[1];
        }

        return type.ToString();
    }

    private void SetUpUnderworldInfo()
    {
        string text = "";
        text += "<b><u>Mine</u></b><br>";
        foreach(InPlayCardController unit in UnderworldManager.Instance.souls)
        {
            if (unit.isMine)
            {
                text += unit.cardData.name + "<br><br>";
            }       
        }
        text += "<br><b><u>Enemy's</u></b><br>";
        foreach(InPlayCardController unit in UnderworldManager.Instance.souls)
        {
            if (!unit.isMine)
            {
                text += unit.cardData.name + "<br><br>";
            }
        }

        underworldText.text = text;
    }
}
