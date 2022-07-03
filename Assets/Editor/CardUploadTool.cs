using System.IO;
using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

public class CardUploadTool : EditorWindow
{
    private string cardData = "";

    // Fields
    private int fileName = 0;
    private int type = 1;
    private int name = 2;
    private int cost = 3;
    private int damage = 4;
    private int resilience = 5;
    private int keywords = 6;
    private int description = 7;
    private int functionalDescription = 8;

    [MenuItem("Window/Custom/CardUploader")]
    public static void ShowWindow() 
    {
        GetWindow<CardUploadTool>("Card Uploader");
    }
    private void OnGUI() 
    {
        if (GUILayout.Button("Choose File"))
        {
            string path = EditorUtility.OpenFilePanel("Choose Card CSV", "", "");
            cardData = File.ReadAllText(path);
            // Debug.Log(cardData);
        }

        if(cardData != "")
        {
            if(GUILayout.Button("Upload Cards"))
            {
                // Get the current cards and store their file names
                string[] currentCardsGUIDs = AssetDatabase.FindAssets("", new[] { "Assets/Resources/Cards" });
                List<string> currentCardFileNames = new List<string>();
                for (int i = 0; i < currentCardsGUIDs.Length; i++)
                {
                    string path = AssetDatabase.GUIDToAssetPath(currentCardsGUIDs[i]);
                    currentCardFileNames.Add(AssetDatabase.LoadAssetAtPath<CardBaseObject>(path).name);
                }

                string[] rows = cardData.Split('\n');

                int numOfFields = rows[0].Split(',').Length;

                // For each row
                for(int i = 1; i < rows.Length; i++)
                {
                    // Add a comma to the end of the row to make the parsing work
                    rows[i] += ',';
                    string[] fields = new string[numOfFields];

                    // Things to keep track of while parsing
                    bool insideQuote = false;
                    bool fieldHasQuotes = false;
                    int startOfSub = 0;
                    int currentField = 0;

                    for(int j = 0; j < rows[i].Length; j++)
                    {
                        // Check if we're in a quoted section
                        if(rows[i][j] == '"')
                        {
                            if (insideQuote)
                            {
                                insideQuote = false;
                            }
                            else
                            {
                                insideQuote = true;
                                fieldHasQuotes = true;
                            }
                        }

                        // Check if we hit a comma outside any quotes
                        else if (rows[i][j] == ',' && !insideQuote)
                        {
                            if (fieldHasQuotes)
                            {
                                fields[currentField] = rows[i].Substring(startOfSub + 1, j - startOfSub - 2);
                                fieldHasQuotes = false;
                            } 
                            else
                            {
                                fields[currentField] = rows[i].Substring(startOfSub, j - startOfSub);
                            }      
                            startOfSub = j + 1;
                            currentField++;
                        }
                    }

                    CardBaseObject card;

                    // If the card already exists, update it
                    if (currentCardFileNames.Contains(fields[fileName]))
                    {
                        card = AssetDatabase.LoadAssetAtPath<CardBaseObject>("Assets/Resources/Cards/" + fields[fileName] + ".asset");
                        SetUpCardData(card, fields);
                        EditorUtility.SetDirty(card);
                    } 
                    // If it's a new card, create it
                    else
                    {
                        card = ScriptableObject.CreateInstance<CardBaseObject>();
                        SetUpCardData(card, fields);
                        AssetDatabase.CreateAsset(card, "Assets/Resources/Cards/" + fields[fileName] + ".asset");
                    }     
                }

                AssetDatabase.SaveAssets();
            }
        }
    }

    // Put the data from the fields array into the card
    private void SetUpCardData(CardBaseObject card, string[] fields) 
    {
        card.type = CardUtilities.GetType(fields[type]);
        card.cardName = fields[name];
        card.description = fields[description];
        // card.blueCost = int.Parse(fields[cost]);
        card.damage = int.Parse(fields[damage]);
        card.resilience = int.Parse(fields[resilience]);
        foreach(string keyword in fields[keywords].Split(','))
        {
            card.keywords.Add(keyword);
        }
        ParseCosts(card, fields[cost]);
    }

    private List<CardUtilities.Keyword> SetUpCardKeywords(string keywordsString) 
    {
        List<CardUtilities.Keyword> keywords = new List<CardUtilities.Keyword>();

        if (keywordsString != "")
        {
            foreach (string keyword in keywordsString.Split(','))
            {
                string[] wordValues = keyword.Split(' ');
                keywords.Add(CardUtilities.GetKeyword(wordValues));
            }
        }

        return keywords;
    }

    private void ParseCosts(CardBaseObject card, string costString) 
    {
        for(int i = 0; i < costString.Length / 2; i++)
        {
            int beginningIndex = i * 2;
            char color = costString[beginningIndex + 1];

            if(color == 'B')
            {
                card.blueCost = int.Parse(costString[beginningIndex].ToString());
            }
            else if(color == 'R')
            {
                card.redCost = int.Parse(costString[beginningIndex].ToString());
            }
            else if(color == 'G')
            {
                card.greenCost = int.Parse(costString[beginningIndex].ToString());
            }
            else if(color == 'K')
            {
                card.blackCost = int.Parse(costString[beginningIndex].ToString());
            }
            else if(color == 'N')
            {
                card.neutralCost = int.Parse(costString[beginningIndex].ToString());
            }
        }
    }
}
