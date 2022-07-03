using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class CardSlotManager : MonoBehaviour
{
    [SerializeField] private GameObject cardSlotPrefab;
    [SerializeField] private RectTransform scrollContent;
    private List<Card> cards = new List<Card>();

    private List<GameObject> cardSlots = new List<GameObject>();

    private enum PrimarySchoolFilter { All, Creation, Destruction, Order, Chaos }
    

    // Filtering Criteria
    string nameSearch = "";
    private PrimarySchoolFilter schoolFilter = PrimarySchoolFilter.All;
    private int costFilter = 0;

    private void Awake() 
    {
        foreach(KeyValuePair<string, Card> card in CardLibrary.Instance.library)
        {
            if (card.Value.allowedInDeck)
            {
                cards.Add(card.Value);
            }
        }

        for(int i = 0; i < cards.Count; i++)
        {
            // Instantiate cardSlot, add it to the grid layout object, set it up
            GameObject cardSlotObject = Instantiate(cardSlotPrefab);
            cardSlots.Add(cardSlotObject);
            cardSlotObject.GetComponent<UICardGraphicsController>().FillInInfo(cards[i]);
            cardSlotObject.transform.SetParent(scrollContent, false);
        }
    }

    private void FillInCards()
    {
        int slotIndex = 0;

        foreach(Card card in cards)
        {
            if (FitsCriteria(card))
            {
                cardSlots[slotIndex].SetActive(true);

                cardSlots[slotIndex].GetComponent<UICardGraphicsController>().FillInInfo(card);
                
                slotIndex++;
            }
        }

        for(int i = slotIndex; i < cardSlots.Count; i++)
        {
            cardSlots[i].SetActive(false);
        }
    }

    private bool FitsCriteria(Card card)
    {
        bool fits = true;
        // Check name
        if (!card.name.ToUpper().Contains(nameSearch.ToUpper()) && !card.keywords.Contains(nameSearch) && !card.description.ToUpper().Contains(nameSearch.ToUpper()) && !card.subtypes.Contains(nameSearch))
        {
            fits = false;
        }

        if(schoolFilter != PrimarySchoolFilter.All)
        {
            if(schoolFilter == PrimarySchoolFilter.Chaos) 
            { 
                if(card.primarySchool != Card.PrimarySchool.Chaos) { fits = false; }
            }
            else if(schoolFilter == PrimarySchoolFilter.Creation) 
            { 
                if(card.primarySchool != Card.PrimarySchool.Creation) { fits = false; }
            }
            else if(schoolFilter == PrimarySchoolFilter.Destruction) 
            { 
                if(card.primarySchool != Card.PrimarySchool.Destruction) { fits = false; }
            }
            else if(schoolFilter == PrimarySchoolFilter.Order) 
            { 
                if(card.primarySchool != Card.PrimarySchool.Order) { fits = false; }
            }
        }

        if(costFilter != 0)
        {
            if(card.GetTotalCost() != costFilter)
            {
                fits = false;
            }
        }

        return fits;
    }

    public void SetNameSearch(string newSearch)
    { 
        nameSearch = newSearch;
        FillInCards();
    }

    public void SetSchoolFilter(int filter)
    {
        if (filter == 1)
        {
            schoolFilter = PrimarySchoolFilter.Order;
        }
        else if (filter == 2)
        {
            schoolFilter = PrimarySchoolFilter.Chaos;
        }
        else if (filter == 3)
        {
            schoolFilter = PrimarySchoolFilter.Creation;
        }
        else if (filter == 4)
        {
            schoolFilter = PrimarySchoolFilter.Destruction;
        }
        else
        {
            schoolFilter = PrimarySchoolFilter.All;
        }

        FillInCards();
    }

    public void SetCostFilter(int filter)
    {
        costFilter = filter;
        FillInCards();
    }
}
