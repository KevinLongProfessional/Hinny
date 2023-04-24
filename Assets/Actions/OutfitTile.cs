using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "OutfitTileScriptableAction", menuName = "ScriptableActions/OutfitTile")]
public class OutfitTile : ScriptableAction
{
    [SerializeField] private TileOutfit outfit;
    public override void PerformAction()
    {
        Tile selectedTile = TileMenu.instance.GetSelectedTile();
        selectedTile.SetOutfit(outfit);
        GameManager.Instance.EndTurn();
    }
}