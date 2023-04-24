using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;

[CreateAssetMenu(fileName ="HinnyButton", menuName ="Hinny/Button")]
public class HinnyButton: ScriptableObject
{
    public string displayName;
    public Button buttonElement;
    public ScriptableAction onButtonClick;
    [SerializeField] private bool showOnNewPlot;
    [SerializeField] private bool showOnOwnedPlot;

    public void Init()
    {
        buttonElement = new Button();
        buttonElement.text = displayName;
    }

    public void ButtonClicked()
    {
        if (onButtonClick != null)
        {
            onButtonClick.PerformAction();
        }
        //to do: evaluate if it's oaky for HinnyButton and TileMenu to depend on one another...
        TileMenu.instance.HideMenu();
    }

    public bool Validate()
    {
        Tile selectedTile = TileMenu.instance.GetSelectedTile();

        PlayerReference? owner = selectedTile.GetOwner();
        if (showOnNewPlot && owner == null && GameManager.Instance.currentGamePhase.Value == GamePhase.PropertyClaim)
        {
            return true;
        }

        PlayerReference? activePlayer = GameManager.Instance.GetActivePlayer().playerTurn;

        if (showOnOwnedPlot && owner == activePlayer && GameManager.Instance.currentGamePhase.Value == GamePhase.MainPhase)
        {
            return true;
        }
        return false;
    }

}