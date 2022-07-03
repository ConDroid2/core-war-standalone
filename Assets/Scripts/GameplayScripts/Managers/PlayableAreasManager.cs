using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayableAreasManager : MonoBehaviour
{
    [SerializeField] private PlayableAreaController spellArea;
    [SerializeField] private PlayableAreaController unitArea;
    [SerializeField] private List<PlayableAreaController> supportAreas;

    private void Awake()
    {
        CardController.OnSelected += HandleCardSelected;
        CardController.OnUnselected += HandleCardUnselected;
    }

    private void OnDestroy()
    {
        CardController.OnSelected -= HandleCardSelected;
        CardController.OnUnselected -= HandleCardUnselected;
    }

    public void HandleCardSelected(CardController card)
    {
        if(card.currentState == CardController.CardState.InHand)
        {
            HandleCardUnselected();
            switch (card.cardData.type)
            {
                case CardUtilities.Type.Character:
                    unitArea.gameObject.SetActive(true);
                    break;
                case CardUtilities.Type.Spell:
                    spellArea.gameObject.SetActive(true);
                    break;
                case CardUtilities.Type.Support:
                    supportAreas[0].gameObject.SetActive(true);
                    supportAreas[1].gameObject.SetActive(true);
                    break;
                default:
                    break;
            }
        }
    }

    public void HandleCardUnselected()
    {
        spellArea.gameObject.SetActive(false);
        unitArea.gameObject.SetActive(false);
        supportAreas[0].gameObject.SetActive(false);
        supportAreas[1].gameObject.SetActive(false);
    }
}
