
using UnityEngine;

[CreateAssetMenu(fileName="ClaimTileScriptableAction", menuName = "ScriptableActions/ClaimTile")]

public class ClaimTile: ScriptableAction
{

    //to do: add a way to validate whether to show the button in ScriptableAction and then implement that here.

    public override void PerformAction()
    {
        Debug.Log("Tile Claimed");
        Tile selectedTile = TileMenu.instance.GetSelectedTile();
        Player activePlayer = GameManager.Instance.GetActivePlayer();
        if (selectedTile != null && activePlayer != null)
        {
            selectedTile.OnClaimTile(activePlayer);
            selectedTile.Deselect();
            GameManager.Instance.EndTurn();
        }
    }
}