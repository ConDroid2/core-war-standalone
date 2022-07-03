using System.IO;
using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
public class CardImageCreator : EditorWindow
{
    private string cardName = "";
    private string path = "";
    private bool textureCreated = false;

    [MenuItem("Window/Custom/CardImageCreator")]
    public static void ShowWindow()
    {
        GetWindow<CardImageCreator>("Card Image Creator");
    }

    private void OnGUI()
    {
        if (GUILayout.Button("Choose File"))
        {
            path = EditorUtility.OpenFilePanel("Choose Image", "", "");
            cardName = "";
            textureCreated = false;
        }

        if(path != "")
        {
            cardName = GUILayout.TextField(cardName);

            if (cardName != "") 
            {
                string newImagePath = "Assets/Resources/CardArt/" + cardName + ".png";

                if (!textureCreated)
                {
                    if (GUILayout.Button("Create Texture"))
                    {
                        Texture2D uploadData = new Texture2D(1, 1);
                        uploadData.LoadImage(File.ReadAllBytes(path));
                        File.WriteAllBytes(newImagePath, uploadData.EncodeToPNG());
                        textureCreated = true;
                    }
                }
                else
                {
                    if (GUILayout.Button("Fix Settings"))
                    {
                        TextureImporter texture = AssetImporter.GetAtPath(newImagePath) as TextureImporter;
                        texture.textureType = TextureImporterType.Sprite;
                        texture.spriteImportMode = SpriteImportMode.Single;
                        texture.spritePixelsPerUnit = 350f;
                        texture.textureFormat = TextureImporterFormat.ARGB32;
                        texture.spritePivot = new Vector2(0.5f, 0.5f);
                        texture.SaveAndReimport();
                        path = "";
                    }
                }
            }
        }
    }
}
