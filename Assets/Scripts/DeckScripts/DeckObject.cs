using System.Collections.Generic;

public class DeckObject
{
    string deckName;
    List<Card> cards;

    public DeckObject(string name)
    {
        deckName = name;
        cards = new List<Card>();
    }

    public DeckObject(DeckData data) 
    {
        deckName = data.name;
        cards = new List<Card>();

        foreach(string cardName in data.cards)
        {
            if(CardLibrary.Instance.library.ContainsKey(cardName))
                cards.Add(CardLibrary.Instance.library[cardName]);
        }
    }

    /** Card Methods **/
    public int GetCardCount() 
    {
        return cards.Count;
    }

    public Card GetCardAt(int index) 
    {
        if(index < cards.Count)
        {
            return cards[index];
        }

        return null;
    }

    public void AddCard(Card card) 
    {
        cards.Add(card);
    }

    public void RemoveCard(Card card) 
    {
        if (cards.Contains(card))
        {
            cards.Remove(card);
        }
    }

    public void ClearCards() 
    {
        cards.Clear();
    }

    /** Name Methods **/
    public string GetName() 
    {
        return deckName;
    }

    public void SetName(string name) 
    {
        deckName = name;
    }
}
