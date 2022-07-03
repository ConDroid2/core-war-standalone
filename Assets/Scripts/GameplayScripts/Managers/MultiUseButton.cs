using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MultiUseButton : MonoBehaviour
{
    public static MultiUseButton Instance { get; private set; }

    public ButtonTimer buttonTimer;
    [SerializeField] private Button button;
    [SerializeField] private Text buttonText;
    [SerializeField] private Image buttonImage;
    public delegate void ButtonFunc();

    private ButtonFunc defaultFunc;
    private string defaultText;

    // Button potential colors
    public Color defaultYellow;
    public Color green;
    public Color blue;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    public void SetButtonFunction(ButtonFunc func)
    {
        button.onClick.RemoveAllListeners();
        button.onClick.AddListener(() => func());
    }

    public void SetDefaultFunction(ButtonFunc func)
    {
        defaultFunc = func;
    }

    public void SetButtonText(string text)
    {
        buttonText.text = text;
    }

    public void SetDefaultText(string text)
    {
        defaultText = text;
    }

    public void BackToDefault()
    {
        button.onClick.RemoveAllListeners();
        buttonText.text = defaultText;
        button.onClick.AddListener(() => defaultFunc());
    }

    public void BackToEndTurn()
    {
        SetButtonFunction(Player.Instance.EndTurn);
        SetButtonText("End Turn");
        defaultText = "End Turn";
        defaultFunc = Player.Instance.EndTurn;
    }

    public void SetInteractable(bool interactable)
    {
        button.interactable = interactable;
    }

    public void SetColorGreen()
    {
        buttonImage.color = green;
    }

    public void SetColorYellow()
    {
        buttonImage.color = defaultYellow;
    }

    public void SetColorBlue()
    {
        buttonImage.color = blue;
    }

    public void ChangeToNextTurn()
    {
        SetDefaultText("Next Turn");
        SetDefaultFunction(() =>
        {
            Player.Instance.EndTurn();
            Player.Instance.StartTurn();
            Instance.BackToEndTurn();


            // Reset everything back to normal
            Player.Instance.turnEndActions.Add(Player.Instance.endTurn);

            List<CardParent> enemyUnits = CardSelector.GetCards(zoneFilter: CardSelector.ZoneFilter.MyZones, typeFilter: CardSelector.TypeFilter.Unit);

            foreach (CardParent enemy in enemyUnits)
            {
                Status[] statuses = enemy.GetComponents<Status>();

                foreach (Status status in statuses)
                {
                    Player.Instance.OnStartTurn -= status.DecreaseCount;
                    Player.Instance.OnStartTurn += status.DecreaseCount;
                }
            }
        });
        Instance.BackToDefault();
    }

    public void ChangeToLose()
    {
        SetDefaultText("Lose");
        SetDefaultFunction(() => 
        {
            MatchManager.Instance.YouLose(true);
        });
        BackToDefault();
    }
}
