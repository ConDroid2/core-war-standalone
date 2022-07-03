using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardBorderController : MonoBehaviour
{
    [SerializeField] private SpriteRenderer border;

    [Header("Base Borders")]
    [SerializeField] private Sprite orderBorder;
    [SerializeField] private Sprite creationBorder;
    [SerializeField] private Sprite chaosBorder;
    [SerializeField] private Sprite destructionBorder;
    [SerializeField] private Sprite neutralBorder;

    [Header("Spell Borders")]
    [SerializeField] private Sprite spellOrderBorder;
    [SerializeField] private Sprite spellCreationBorder;
    [SerializeField] private Sprite spellChaosBorder;
    [SerializeField] private Sprite spellDestructionBorder;
    [SerializeField] private Sprite spellNeutralBorder;

    [Header("Gradients")]
    [SerializeField] private SpriteRenderer gradient;
    [SerializeField] private Sprite normalGradient;
    [SerializeField] private Sprite spellGradient;

    public void SetUp(Card data)
    {
        if(data.type == CardUtilities.Type.Spell)
        {
            if (data.primarySchool == Card.PrimarySchool.Neutral) { border.sprite = spellNeutralBorder; }
            else if (data.primarySchool == Card.PrimarySchool.Order) { border.sprite = spellOrderBorder; }
            else if (data.primarySchool == Card.PrimarySchool.Creation) { border.sprite = spellCreationBorder; }
            else if (data.primarySchool == Card.PrimarySchool.Chaos) { border.sprite = spellChaosBorder; }
            else if (data.primarySchool == Card.PrimarySchool.Destruction) { border.sprite = spellDestructionBorder; }

            if(gradient != null)
                gradient.sprite = spellGradient;
        }
        else
        {
            if (data.primarySchool == Card.PrimarySchool.Neutral) { border.sprite = neutralBorder; }
            else if (data.primarySchool == Card.PrimarySchool.Order) { border.sprite = orderBorder; }
            else if (data.primarySchool == Card.PrimarySchool.Creation) { border.sprite = creationBorder; }
            else if (data.primarySchool == Card.PrimarySchool.Chaos) { border.sprite = chaosBorder; }
            else if (data.primarySchool == Card.PrimarySchool.Destruction) { border.sprite = destructionBorder; }

            if(gradient != null)
                gradient.sprite = normalGradient;
        }
    }


}
