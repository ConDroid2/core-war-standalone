using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InfoHoverController : MonoBehaviour
{
    Camera camera;

    [SerializeField] private TMP_Text textObject;

    int hoveredLinkIndex = -1;
    private void Awake()
    {
        camera = Camera.main;
        Debug.Log("Setting up info hover controller");
    }

    private void LateUpdate()
    {
        int linkIndex = TMP_TextUtilities.FindIntersectingLink(textObject, Input.mousePosition, null);
        Debug.Log(linkIndex);

        if((linkIndex == -1 && hoveredLinkIndex != -1) || linkIndex != hoveredLinkIndex)
        {
            hoveredLinkIndex = -1;
        }

        if(linkIndex != -1 && linkIndex != hoveredLinkIndex)
        {
            hoveredLinkIndex = linkIndex;
            TMP_LinkInfo linkInfo = textObject.textInfo.linkInfo[linkIndex];

            Debug.Log("Hovered link: " + linkInfo.GetLinkID());
        }
    }
}
