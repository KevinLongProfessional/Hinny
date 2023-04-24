using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInfoController : MonoBehaviour
{
    // Start is called before the first frame update
    public Player playerInfo;
    public int playerIndex;
    public bool isMyTurn
    {
        get
        {
            return (playerInfo.playerTurn == GameManager.Instance.currentTurn);
        }
    }

void Start()
    {
        playerInfo.playerTurn = (PlayerReference)playerIndex;
        GameManager.OnPlayerTurnChanged += PlayerTurnChange;
    }

    void OnDestroy()
    {
        GameManager.OnPlayerTurnChanged -= PlayerTurnChange;
    }

    void PlayerTurnChange(PlayerReference turn)
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}