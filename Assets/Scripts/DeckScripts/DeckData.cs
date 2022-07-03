
[System.Serializable]
public class DeckData
{
    public string name;
    public string[] cards;

    public DeckData(DeckObject deck) 
    {
        name = deck.GetName();

        cards = new string[deck.GetCardCount()];

        for(int i = 0; i < deck.GetCardCount(); i++)
        {
            cards[i] = deck.GetCardAt(i).fileName;
        }
    }
}
