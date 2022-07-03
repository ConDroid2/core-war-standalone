using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CardUIInfoFiller : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI cardName;
    [SerializeField] private TextMeshProUGUI description;
    [SerializeField] private TextMeshProUGUI keywords;
    [SerializeField] private TextMeshProUGUI strengthTxt;
    [SerializeField] private TextMeshProUGUI resilienceTxt;
    [SerializeField] private TextMeshProUGUI subtypes;

    [SerializeField] private GameObject keywordsPanel;
    [SerializeField] private TextMeshProUGUI keywordsPanelText;

    [Header("Cost Stuff")]
    [SerializeField] private List<RectTransform> costSlots = new List<RectTransform>();
    [SerializeField] private GameObject neutralCostBG;
    [SerializeField] private GameObject orderCostBG;
    [SerializeField] private GameObject creationCostBG;
    [SerializeField] private GameObject chaosCostBG;
    [SerializeField] private GameObject destructionCostBG;
    [SerializeField] private TextMeshProUGUI neutralCostText;
    [SerializeField] private TextMeshProUGUI orderCostText;
    [SerializeField] private TextMeshProUGUI creationCostText;
    [SerializeField] private TextMeshProUGUI chaosCostText;
    [SerializeField] private TextMeshProUGUI destructionCostText;

    [Header("Border Stuff")]
    [SerializeField] private Image border;
    [SerializeField] private Sprite neutralBorder;
    [SerializeField] private Sprite orderBorder;
    [SerializeField] private Sprite creationBorder;
    [SerializeField] private Sprite chaosBorder;
    [SerializeField] private Sprite destructionBorder;



    public Dictionary<string, string> keywordDescriptions = new Dictionary<string, string>
    {
        {"Tutor", "When entering a zone, give +1/+1 to any units with less Strength or Resilience" },
        {"Swift", "I can act the same turn I am played" },
        {"Legendary", "When I am in play, all other copies of me become a different card" },
        {"Resistant", "I cannot be targted by abilities" },
        {"Regeneration", "I fully heal at the end of each turn" },
        {"Steady", "I do not get knocked backed when the Core is primed" },
        {"Prophecy", "I may be Prophecized during payment" },
        {"Icetouch", "Whenever I deal damage to a unit, it is Frozen for 1 turn" }
    };


    public void FillInInfo(Card cardInfo)
    {
        cardName.text = cardInfo.name;
        description.text = cardInfo.description;
        strengthTxt.text = cardInfo.currentStrength.ToString();
        resilienceTxt.text = cardInfo.currentResilience.ToString();

        string stringKeywords = "";

        for(int i = 0; i < cardInfo.keywords.Count; i++)
        {
            stringKeywords += cardInfo.keywords[i];

            if(i != cardInfo.keywords.Count - 1)
            {
                stringKeywords += ", ";
            }
        }

        keywords.text = stringKeywords;

        string stringSubtypes = "";

        for(int i = 0; i < cardInfo.subtypes.Count; i++)
        {
            if(cardInfo.subtypes[i] != "All") stringSubtypes += cardInfo.subtypes[i];

            if(i != cardInfo.subtypes.Count - 1)
            {
                stringSubtypes += " ";
            }
        }

        subtypes.text = stringSubtypes;

        SetUpKeywordsPanel(cardInfo);
        SetUpCosts(cardInfo);
        SetUpBorder(cardInfo);
    }

    public void SetUpKeywordsPanel(Card cardData)
    {
        keywordsPanel.SetActive(false);
        if (cardData.keywords.Count > 0)
        {
            keywordsPanel.SetActive(true);
        }

        string keywordDescriptionsText = "";

        foreach(string keyword in cardData.keywords)
        {
            string keywordDescription = "<b>" + keyword + "</b> - ";
            if (keywordDescriptions.ContainsKey(keyword))
            {
                keywordDescription += keywordDescriptions[keyword];
            }

            keywordDescription += "<br><br>";

            keywordDescriptionsText += keywordDescription;
        }

        keywordsPanelText.text = keywordDescriptionsText;
    }

    public void SetUpCosts(Card cardData)
    {
        int currentCostSlot = 0;

        if (cardData.cost["Neutral"] > 0)
        {
            neutralCostBG.SetActive(true);
            neutralCostText.text = cardData.cost["Neutral"].ToString();
            neutralCostBG.GetComponent<RectTransform>().anchoredPosition = costSlots[currentCostSlot].anchoredPosition;
            currentCostSlot++;
        }
        else
        {
            neutralCostBG.SetActive(false);
        }

        if (cardData.cost["Blue"] > 0)
        {
            orderCostBG.SetActive(true);
            orderCostText.text = cardData.cost["Blue"].ToString();
            orderCostBG.GetComponent<RectTransform>().anchoredPosition = costSlots[currentCostSlot].anchoredPosition;
            currentCostSlot++;
        }
        else
        {
            orderCostBG.SetActive(false);
        }

        if (cardData.cost["Red"] > 0)
        {
            chaosCostBG.SetActive(true);
            chaosCostText.text = cardData.cost["Red"].ToString();
            chaosCostBG.GetComponent<RectTransform>().anchoredPosition = costSlots[currentCostSlot].anchoredPosition;
            currentCostSlot++;
        }
        else
        {
            chaosCostBG.SetActive(false);
        }

        if (cardData.cost["Green"] > 0)
        {
            creationCostBG.SetActive(true);
            creationCostText.text = cardData.cost["Green"].ToString();
            creationCostBG.GetComponent<RectTransform>().anchoredPosition = costSlots[currentCostSlot].anchoredPosition;
            currentCostSlot++;
        }
        else
        {
            creationCostBG.SetActive(false);
        }

        if (cardData.cost["Black"] > 0)
        {
            destructionCostBG.SetActive(true);
            destructionCostText.text = cardData.cost["Black"].ToString();
            destructionCostBG.GetComponent<RectTransform>().anchoredPosition = costSlots[currentCostSlot].anchoredPosition;
            currentCostSlot++;
        }
        else
        {
            destructionCostBG.SetActive(false);
        }
    }

    public void SetUpBorder(Card cardData)
    {
        if (cardData.primarySchool == Card.PrimarySchool.Neutral) { border.sprite = neutralBorder; }
        else if (cardData.primarySchool == Card.PrimarySchool.Order) { border.sprite = orderBorder; }
        else if (cardData.primarySchool == Card.PrimarySchool.Creation) { border.sprite = creationBorder; }
        else if (cardData.primarySchool == Card.PrimarySchool.Chaos) { border.sprite = chaosBorder; }
        else if (cardData.primarySchool == Card.PrimarySchool.Destruction) { border.sprite = destructionBorder; }
    }
}
