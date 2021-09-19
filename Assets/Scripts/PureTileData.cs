using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]

public class PureTileData {

    public int clickCount;
    public TileType tileType;
    public TileType tileBeforeZombifying;
    public int zombieBreakoutStage;
    public int maxZombieBreakoutStage;
    public bool hasCoin;
    public bool readyForBurial;
    public bool hasBurriedInside;
    public bool isGrave;
    public bool isDecoration;
    public bool isProtected;
    public float zombieChance; //based on type of grave
    public int coffins;
    public int posI,posJ;
    public bool IsCollider;
    public bool IsChunkFree;
    public bool IsChunkLeftNeighbour;

    public PureTileData(TileData data)
    {
        clickCount = data.getClickCount();
        tileType = data.getTileType();
        tileBeforeZombifying = data.getTileBeforeZombifying();
        zombieBreakoutStage = data.getMaxZombieBreakoutStage();
        maxZombieBreakoutStage = data.getMaxZombieBreakoutStage();
        hasCoin = data.getHasCoin();
        readyForBurial = data.getReadyForBurial();
        hasBurriedInside = data.getHasBurriedInside();
        isGrave = data.getIsGrave();
        isDecoration = data.getIsDecoration();
        isProtected = data.getIsProtected();
        zombieChance = data.getZombieChance(); //based on type of grave
        coffins = data.getCoffins();
        posI = data.getPosI();
        posJ = data.getPosJ();
        IsCollider = data.IsCollider;
        IsChunkFree = data.IsChunkFree;
        IsChunkLeftNeighbour = data.IsChunkLeftNeighbour;
    }

}
