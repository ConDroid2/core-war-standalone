using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SpellManager
{
    public List<string> spellsThisTurn;

    public Dictionary<string, int> spellsThisGame;

    public Action OnSpellCast;

    public SpellManager()
    {
        spellsThisTurn = new List<string>();
        spellsThisGame = new Dictionary<string, int>();

        Player.Instance.OnEndTurn += ResetList;
    }

    public void AddSpellToList(CardController spell)
    {
        spellsThisTurn.Add(spell.cardData.name);
        OnSpellCast?.Invoke();

        if (spellsThisGame.ContainsKey(spell.cardData.name))
        {
            spellsThisGame[spell.cardData.name]++;
        }
        else
        {
            spellsThisGame.Add(spell.cardData.name, 1);
        }
    }

    public void ResetList()
    {
        spellsThisTurn.Clear();
    }
}
