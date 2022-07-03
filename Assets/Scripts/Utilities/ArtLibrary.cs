using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArtLibrary : MonoBehaviour
{
    public Dictionary<string, Sprite> library = new Dictionary<string, Sprite>();
    public Dictionary<Card.PrimarySchool, Dictionary<CardUtilities.Type, Sprite>> defaultArt = new Dictionary<Card.PrimarySchool, Dictionary<CardUtilities.Type, Sprite>>();
    public static ArtLibrary Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null) Instance = this;

        Sprite[] sprites = Resources.LoadAll<Sprite>("CardArt");

        foreach(Sprite sprite in sprites)
        {
            library.Add(sprite.name, sprite);
        }

        SetUpDefaultArt();
    }

    public void SetUpDefaultArt()
    {
        Dictionary<CardUtilities.Type, Sprite> chaos = new Dictionary<CardUtilities.Type, Sprite>
        {
            {CardUtilities.Type.Character, library["EnergyGolem"]},
            {CardUtilities.Type.Spell, library["Experimentation"] },
            {CardUtilities.Type.Support, library["Experimentation"] }
        };
        Dictionary<CardUtilities.Type, Sprite> order = new Dictionary<CardUtilities.Type, Sprite>
        {
            {CardUtilities.Type.Character, library["ValiantFlagbearer"]},
            {CardUtilities.Type.Spell, library["Blizzard"] },
            {CardUtilities.Type.Support, library["Blizzard"] }
        };
        Dictionary<CardUtilities.Type, Sprite> destruction = new Dictionary<CardUtilities.Type, Sprite>
        {
            {CardUtilities.Type.Character, library["Raven"]},
            {CardUtilities.Type.Spell, library["Annihilate"] },
            {CardUtilities.Type.Support, library["Annihilate"] }
        };
        Dictionary<CardUtilities.Type, Sprite> creation = new Dictionary<CardUtilities.Type, Sprite>
        {
            {CardUtilities.Type.Character, library["SylkanDrone"]},
            {CardUtilities.Type.Spell, library["BlossomingCharm"] },
            {CardUtilities.Type.Support, library["BlossomingCharm"] }
        };
        Dictionary<CardUtilities.Type, Sprite> neutral = new Dictionary<CardUtilities.Type, Sprite>
        {
            {CardUtilities.Type.Character, library["ValiantFlagbearer"]},
            {CardUtilities.Type.Spell, library["Experimentation"] },
            {CardUtilities.Type.Support, library["Experimentation"] }
        };

        defaultArt.Add(Card.PrimarySchool.Chaos, chaos);
        defaultArt.Add(Card.PrimarySchool.Creation, creation);
        defaultArt.Add(Card.PrimarySchool.Destruction, destruction);
        defaultArt.Add(Card.PrimarySchool.Neutral, neutral);
        defaultArt.Add(Card.PrimarySchool.Order, order);
    }


}
