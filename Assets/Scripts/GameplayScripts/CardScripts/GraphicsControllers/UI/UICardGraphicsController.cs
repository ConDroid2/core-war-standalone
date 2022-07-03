using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Events;
using System;

public class UICardGraphicsController : MonoBehaviour
{
    [Header("Card Pieces")]
    [SerializeField] private RectTransform blueCostBG;
    [SerializeField] private TextMeshProUGUI blueCostTxt;
    [SerializeField] private RectTransform redCostBG;
    [SerializeField] private TextMeshProUGUI redCostTxt;
    [SerializeField] private RectTransform greenCostBG;
    [SerializeField] private TextMeshProUGUI greenCostTxt;
    [SerializeField] private RectTransform blackCostBG;
    [SerializeField] private TextMeshProUGUI blackCostTxt;
    [SerializeField] private RectTransform neutralCostBG;
    [SerializeField] private TextMeshProUGUI neutralCostTxt;
    [SerializeField] private GameObject damageBG;
    [SerializeField] private TextMeshProUGUI damageTxt;
    [SerializeField] private GameObject resilienceBG;
    [SerializeField] private TextMeshProUGUI resilienceTxt;
    [SerializeField] private GameObject influenceBG;
    [SerializeField] private TextMeshProUGUI influenceTxt;
    [SerializeField] private GameObject unitGradient;
    [SerializeField] private GameObject spellGradient;

    [SerializeField] private List<RectTransform> energyCostPositions = new List<RectTransform>();
    [SerializeField] private List<Sprite> CostBars = new List<Sprite>();
    [SerializeField] private Image costBarImage;

    [Header("Sub Controllers")]
    [SerializeField] private UICardElementManager elementManager;
    [SerializeField] private UIBorderManager borderManager;
    [SerializeField] private UICardArtManager artManager;

    //Events
    public Action<Card> OnFilledIn;
    public UnityEvent<Card> OnInspected;

    private void Awake()
    {
        elementManager.SetUp();
    }

    public void FillInInfo(Card cardInfo)
    {
        elementManager.cardName.SetUIText(cardInfo.name);
        elementManager.description.SetUIText(cardInfo.description);
        SetUpSubtypes(cardInfo);
        SetUpKeywords(cardInfo);
        elementManager.UpdatePos();

        SetUpCosts(cardInfo);
        SetUpStrength(cardInfo);
        SetUpResilience(cardInfo);
        SetUpInfluence(cardInfo);

        borderManager.SetUp(cardInfo);
        artManager.SetUp(cardInfo);

        if(cardInfo.type == CardUtilities.Type.Spell)
        {
            spellGradient.SetActive(true);
            unitGradient.SetActive(false);
        }
        else
        {
            spellGradient.SetActive(false);
            unitGradient.SetActive(true);
        }

        OnFilledIn?.Invoke(cardInfo);
        OnInspected?.Invoke(cardInfo);
    }

    public void SetUpSubtypes(Card cardInfo)
    {
        string subtypes = "";

        for (int i = 0; i < cardInfo.subtypes.Count; i++)
        {
            if (cardInfo.subtypes[i] != "All") subtypes += cardInfo.subtypes[i];

            if (i != cardInfo.subtypes.Count - 1)
            {
                subtypes += " ";
            }
        }

        elementManager.subtype.SetUIText(subtypes);
    }

    public void SetUpKeywords(Card cardInfo)
    {
        string keywords = "";

        for (int i = 0; i < cardInfo.keywords.Count; i++)
        {
            keywords += cardInfo.keywords[i];

            if (i != cardInfo.keywords.Count - 1)
            {
                keywords += ", ";
            }
        }

        elementManager.keywords.SetUIText(keywords);
    }

    public void SetUpCosts(Card cardInfo)
    {
        int currentSlot = 0;

        if(cardInfo.cost["Neutral"] > 0)
        {
            neutralCostBG.gameObject.SetActive(true);
            neutralCostTxt.text = cardInfo.cost["Neutral"].ToString();
            neutralCostBG.anchoredPosition = energyCostPositions[currentSlot].anchoredPosition;
            currentSlot++;
        }
        else
        {
            neutralCostBG.gameObject.SetActive(false);
        }

        if (cardInfo.cost["Blue"] > 0)
        {
            blueCostBG.gameObject.SetActive(true);
            blueCostTxt.text = cardInfo.cost["Blue"].ToString();
            blueCostBG.anchoredPosition = energyCostPositions[currentSlot].anchoredPosition;
            currentSlot++;
        }
        else
        {
            blueCostBG.gameObject.SetActive(false);
        }

        if (cardInfo.cost["Red"] > 0)
        {
            redCostBG.gameObject.SetActive(true);
            redCostTxt.text = cardInfo.cost["Red"].ToString();
            redCostBG.anchoredPosition = energyCostPositions[currentSlot].anchoredPosition;
            currentSlot++;
        }
        else
        {
            redCostBG.gameObject.SetActive(false);
        }

        if (cardInfo.cost["Green"] > 0)
        {
            greenCostBG.gameObject.SetActive(true);
            greenCostTxt.text = cardInfo.cost["Green"].ToString();
            greenCostBG.anchoredPosition = energyCostPositions[currentSlot].anchoredPosition;
            currentSlot++;
        }
        else
        {
            greenCostBG.gameObject.SetActive(false);
        }

        if (cardInfo.cost["Black"] > 0)
        {
            blackCostBG.gameObject.SetActive(true);
            blackCostTxt.text = cardInfo.cost["Black"].ToString();
            blackCostBG.anchoredPosition = energyCostPositions[currentSlot].anchoredPosition;
            currentSlot++;
        }
        else
        {
            blackCostBG.gameObject.SetActive(false);
        }

        // Can't just replace the sprite because UI stretches things
        // costBarImage.sprite = CostBars[currentSlot - 1];
    }

    public void SetUpStrength(Card cardInfo)
    {
        if(cardInfo.type == CardUtilities.Type.Character)
        {
            damageBG.SetActive(true);
            damageTxt.text = cardInfo.currentStrength.ToString();
        }
        else
        {
            damageBG.SetActive(false);
        }
    }

    public void SetUpResilience(Card cardInfo)
    {
        if(cardInfo.type == CardUtilities.Type.Character)
        {
            resilienceBG.SetActive(true);
            resilienceTxt.text = cardInfo.currentResilience.ToString();
        }
        else
        {
            resilienceBG.SetActive(false);
        }
    }

    public void SetUpInfluence(Card cardInfo)
    {
        if(cardInfo.type == CardUtilities.Type.Character)
        {
            influenceBG.SetActive(true);
            influenceTxt.text = cardInfo.currentInfluence.ToString();
        }
        else
        {
            influenceBG.SetActive(false);
        }
    }
}
