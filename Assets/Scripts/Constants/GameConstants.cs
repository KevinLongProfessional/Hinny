using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public enum PlayerReference
{
    p1Turn,
    p2Turn,
    p3Turn,
    p4Turn,
    roundEnd
}

public static class GamePhase
{
    public const string PropertyClaim = "Property Claim";
    public const string MainPhase = "Main Phase";
    public const string Auction = "Auction";
}
