using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class Tile : MonoBehaviour
{
    [SerializeField] private Color _baseColor, _selectedColor, _highlightColor;
    [SerializeField] private MeshRenderer _renderer;
    [SerializeField] private PlayerReference? playerRef = null;
    [SerializeField] private TileOutfit outfit;

    private bool selected = false;

    public void Init(Material material)
    {
        _renderer.material = material;
        _renderer.material.color = _baseColor;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseEnter()
    {
        if (!selected)
        {
            _renderer.material.color = _highlightColor;
        }
    }

    private void OnMouseExit()
    {
        if (!selected)
        {
            _renderer.material.color = _baseColor;
        }
    }


    public void OnClaimTile(Player player)
    {
        _baseColor = player.playerColor;
        _renderer.material.color = _baseColor;
        playerRef = player.playerTurn;
    }

    public void SetOutfit(TileOutfit _outfit)
    {
        if (_outfit.type == OutfitType.none)
        {
            outfit = null;
        }
        else
        {
            GameObject outfitObject = this.transform.GetChild(0).gameObject;
            outfitObject.GetComponent<MeshRenderer>().material = _outfit.material;
            outfit = _outfit;
        }
    }

    private void OnMouseUpAsButton()
    {
        //to do: make it so only one tile can be selected at a time.
        Select();
    }

    public void Select()
    {
        _renderer.material.color = _selectedColor;
        Player currentPlayer = GameManager.Instance.GetActivePlayer();
        TileMenu.instance.SelectTile(this);
        selected = true;
    }

    public void Deselect()
    {
        selected = false;
        _renderer.material.color = _baseColor;
    }

    public PlayerReference? GetOwner()
    {
        return playerRef;
    }

    public void ScoreTile()
    {
        if (playerRef != null)
        {
            Player tileOwner = GameManager.Instance.playerInfo.First(player => player.playerTurn == playerRef);
            if(outfit != null)
            {
                switch(outfit.type)
                {
                    case OutfitType.food:
                        tileOwner.foodCount.Value++;
                        break;
                    case OutfitType.ore:
                        tileOwner.oreCount.Value++;
                        break;
                    case OutfitType.energy:
                        tileOwner.energyCount.Value++;
                        break;
                    default: { break; }
                }
            }
        }
    }
}
