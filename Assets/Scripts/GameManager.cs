using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;
using UnityMvvmToolkit.Core.Interfaces;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public static event Action<PlayerReference> OnPlayerTurnChanged;
    public static event Action<Player[]> OnPlayerInfoChanged;

    int roundCount = 0;
    public Player[] playerInfo = new Player[4];

    public PlayerReference currentTurn;
    public Property<string> currentGamePhase;

    private void Awake()
    {
        Instance = this;
        currentGamePhase = (Property<string>)GamePhase.PropertyClaim;

        //to do: remove this mock info and have it created via menu interface.
        playerInfo[0] = new Player("Green Bonzoid", PlayerReference.p1Turn, Color.green);
        playerInfo[1] = new Player("Purple Humanoid", PlayerReference.p2Turn, Color.magenta);
        playerInfo[2] = new Player("Harambe", PlayerReference.p3Turn, Color.yellow);
        playerInfo[3] = new Player("Xena: Warrior Princess", PlayerReference.p4Turn, Color.red);
    }
    private void Start()
    {
        UpdatePlayerTurn(PlayerReference.p1Turn);
    }

    void UpdatePlayerTurn(PlayerReference newState)
    {
        currentTurn = newState;
        OnPlayerTurnChanged?.Invoke(newState);
    }

    private void Update()
    {
        playerInfo[0].name.Value = UnityEngine.Random.Range(0, 3).ToString();
    }

    public void EndTurn()
    {
        UpdatePlayerTurn(currentTurn.Next());
        if(currentTurn == PlayerReference.roundEnd)
        {
            switch(currentGamePhase.Value)
            {
                case GamePhase.PropertyClaim:
                    currentGamePhase.Value = GamePhase.MainPhase;
                    break;
                case GamePhase.MainPhase:
                    roundCount++;
                    for (int x = 0; x < GridManager.Instance.tiles.Count; x++)
                    {
                        for (int y = 0; y < GridManager.Instance.tiles[x].Count; y++)
                        {
                            Tile tile = GridManager.Instance.tiles[x][y];
                            tile.ScoreTile();
                        }
                    }
                    break;
                case GamePhase.Auction:
                    currentGamePhase.Value = GamePhase.PropertyClaim;
                    break;
                default: break; //to do: log error here!
            }
            UpdatePlayerTurn(currentTurn.Next());
        }
    }

    public Player GetActivePlayer()
    {
        return playerInfo.FirstOrDefault(p => p.playerTurn == currentTurn);
    }

    public void GenerateTileUI()
    {

    }

}

public class Player
{
    public Property<string> name;
    public Property<int> moneyCount;
    public Property<int> energyCount;
    public Property<int> oreCount;
    public Property<int> foodCount;
    public PlayerReference playerTurn;
    public Color playerColor;
    public bool isMyTurn { 
        get
        {
            return GameManager.Instance.currentTurn == playerTurn;
        }
    }

    public Player(string playerName, PlayerReference turn, Color color)
    {
        foodCount = (Property<int>)0;
        energyCount = (Property<int>)0;
        oreCount = (Property<int>)0;
        moneyCount = (Property<int>)0;
        name = (Property<string>)playerName;
        playerTurn = turn;
        playerColor = color;
    }
}