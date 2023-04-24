using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Xml.Linq;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

public class UI : MonoBehaviour
{
    // Start is called before the first frame update

    string[] playerIds = new string[] { "Player1UI", "Player2UI", "Player3UI", "Player4UI" };
    VisualElement root;

    void Start()
    {
        //to do: consider creating the entire ui here, as done here. https://www.youtube.com/watch?v=sVEmJ5-dr5E&t=434s
        //This way you could support an indefinite number of players.
        //alternatively, look if using this would work. https://docs.unity3d.com/ScriptReference/UIElements.VisualTreeAsset.CloneTree.html
        root = GetComponent<UIDocument>().rootVisualElement;
        HandlePlayerUI(0);
        HandlePlayerUI(1);
        HandlePlayerUI(2);
        HandlePlayerUI(3);

        HighlightElement(root.Q<VisualElement>(playerIds[0]), Color.white);
    }

    private void HandlePlayerUI(int index)
    {
        GameManager.OnPlayerTurnChanged += PlayerTurnChange;
        VisualElement playerElement = root.Q<VisualElement>(playerIds[index]);
        BindUIProps(playerElement, index);
        RuntimeBindingExtensions.BindProperty(root.Q<TextElement>("PhaseLabel"), GameManager.Instance.currentGamePhase);

        SetUIColor(playerElement, index);
    }

    void HighlightElement(VisualElement element, Color highlightColor)
    {
        element.style.borderLeftColor = Color.white;
        element.style.borderLeftWidth = 12;
        element.style.borderRightColor = Color.white;
        element.style.borderRightWidth = 12;
        element.style.borderTopColor = Color.white;
        element.style.borderTopWidth = 12;
        element.style.borderBottomColor = Color.white;
        element.style.borderBottomWidth = 12;
    }

    private void PlayerTurnChange(PlayerReference player)
    {
        //highlight active player.
        int currentTurnIndex = (int)player;

        for (int i = 0; i < playerIds.Count(); i++)
        {
            VisualElement playerElement = root.Q<VisualElement>(playerIds[i]);
            if(i == currentTurnIndex)
            {
                HighlightElement(playerElement, Color.white); break;
            }
            else
            {
                //to do: make a helper class that has a setBorder and clearBorder function.
                playerElement.style.borderLeftColor = Color.clear;
                playerElement.style.borderLeftWidth = 0;
                playerElement.style.borderRightColor = Color.clear;
                playerElement.style.borderRightWidth = 0;
                playerElement.style.borderTopColor = Color.clear;
                playerElement.style.borderTopWidth = 0;
                playerElement.style.borderBottomColor = Color.clear;
                playerElement.style.borderBottomWidth = 0;
            }
        }
    }

    private void OnDestroy()
    {
        for (int i = 0; i < playerIds.Count(); i++)
        {
            VisualElement playerElement = root.Q<VisualElement>(playerIds[i]);
            UnbindUIProps(playerElement, i);
        }
    }

    void SetUIColor(VisualElement vElement, int index)
    {
        vElement.style.backgroundColor = GameManager.Instance.playerInfo[index].playerColor;
        foreach (VisualElement child in vElement.Children())
        {
            //spooky recursion.
            SetUIColor(child, index);
        }
    }

    void BindUIProps(VisualElement vElement, int index)
    {
        RuntimeBindingExtensions.BindProperty(vElement.Q<Label>("NameLabel"), GameManager.Instance.playerInfo[index].name);
        BindValueLabel(vElement.Q<VisualElement>("MoneyContainer"), GameManager.Instance.playerInfo[index].moneyCount);
        BindValueLabel(vElement.Q<VisualElement>("EnergyContainer"), GameManager.Instance.playerInfo[index].energyCount);
        BindValueLabel(vElement.Q<VisualElement>("OreContainer"), GameManager.Instance.playerInfo[index].oreCount);
        BindValueLabel(vElement.Q<VisualElement>("FoodContainer"), GameManager.Instance.playerInfo[index].foodCount);
    }

    void BindValueLabel(VisualElement vElement, Property<int> property)
    {
        RuntimeBindingExtensions.BindProperty(vElement.Q<Label>("Value"), property);
    }

    void UnbindUIProps(VisualElement vElement, int index)
    {
        RuntimeBindingExtensions.UnbindProperty(vElement.Q<Label>("NameLabel"), GameManager.Instance.playerInfo[index].name);
        UnbindValueProp(vElement.Q<VisualElement>("MoneyContainer"), GameManager.Instance.playerInfo[index].moneyCount);
        UnbindValueProp(vElement.Q<VisualElement>("OreContainer"), GameManager.Instance.playerInfo[index].energyCount);
        BindValueLabel(vElement.Q<VisualElement>("EnergyContainer"), GameManager.Instance.playerInfo[index].oreCount);
        UnbindValueProp(vElement.Q<VisualElement>("FoodContainer"), GameManager.Instance.playerInfo[index].foodCount);
    }

    void UnbindValueProp(VisualElement vElement, Property<int> property)
    {
        RuntimeBindingExtensions.UnbindProperty(vElement.Q<Label>("Value"), property);
    }

}