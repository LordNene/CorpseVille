using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CemeteryData
{

    public long Money;
    public int Burried;
    public int CorpsesInQueue;
    public int CorpsesInStock;

    public int StockMax;

    public bool IsHalfZombieSpawn;
    public bool IsKiller;
    public bool IsVirus;

    public int Lvl;
    public long XP;

    public PureTileData[,] PureTileData;
    public AchievementData AchievementData;


    public CemeteryData(TileData[,] tileDataMatrix, AtributesManagerScript atributes, AchievementData achievementData)
    {
        int x = tileDataMatrix.GetLength(0);
        int y = tileDataMatrix.GetLength(1);
        PureTileData = new PureTileData[x,y];

        for (int i = 0; i < x; i++)
        {
            for (int k = 0; k < y; k++)
            {
                PureTileData[i,k] = new PureTileData(tileDataMatrix[i,k]);
            }
        }
        Money = atributes.Money;
        Burried = atributes.Burried;
        CorpsesInQueue = atributes.CorpsesInQueue;
        CorpsesInStock = atributes.CorpsesInStock;

        StockMax = atributes.StockMax;

        Lvl = atributes.Lvl;
        XP = atributes.XP;

        AchievementData = achievementData;
    }


}
