using System;

public static class DeckUtilities
{
    public static int deckSize = 50;
}

[Serializable]
public class DeckJSON
{
    public string deckName;
    public string[] cards;
}
