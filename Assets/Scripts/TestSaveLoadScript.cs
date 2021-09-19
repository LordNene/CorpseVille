using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestSaveLoadScript : MonoBehaviour
{
    public AtributesManagerScript Atributes;
    public TileDataManager TileData;
    public AchievementManager AchievementManager;
    public BrainScript Brain;

    //public Button Load;

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
    private float time;
    public float SaveTimeAfterSeconds = 60f;

    // Start is called before the first frame update
    void Start()
    {
        //Load.onClick.AddListener(LoadClick);
        StartCoroutine("WaitAndLoad");
        time = -1;
    }

    public void TestClick()
    {

        TileData.rerenderTilesFromDataMatrix();
    }

    IEnumerator WaitAndLoad()
    {
        yield return new WaitForSeconds(0.5f);
        Load();
    }
    /*
    IEnumerator WaitAndNewGame()
    {
        yield return new WaitForSeconds(0.5f);
        GenerateNew();
    }
    */
    public void GenerateNew()
    {
        //Brain.GenerateFirstGrassChunk();
        Atributes.SetAtributesToDefault();
        TileData.GenerateNewGameHardFixJebemNato();
        AchievementManager.Reset();
        //TileData.rerenderTilesFromDataMatrix();
        //Save();
    }

    void OnApplicationQuit()
    {
        Save();
    }

    void Update()
    {
        time += Time.deltaTime;
        if (time > SaveTimeAfterSeconds)
        {
            Save();
            time = 0f;
        }
    }

    public void Save()
    {
        SaveSystem.SaveCemeteryData(TileData.TileDataMatrix,Atributes, AchievementManager.Save());

    }

    public void NewGame()
    {
        GenerateNew();
        //StartCoroutine("WaitAndNewGame");
    }

    public void Load()
    {
        CemeteryData data = SaveSystem.LoadCemeteryData();

        Money = data.Money;
        Burried = data.Burried;
        CorpsesInQueue = data.CorpsesInQueue;
        CorpsesInStock = data.CorpsesInStock;

        StockMax = data.StockMax;

        IsHalfZombieSpawn = data.IsHalfZombieSpawn;
        IsKiller = data.IsKiller;
        IsVirus = data.IsVirus;

        Lvl = data.Lvl;
        XP = data.XP;
        PureTileData = data.PureTileData;

        AchievementManager.Load(data.AchievementData);
        LoadDataToAtributeManager();
        LoadDataToTileDataMatrix();
        TileData.rerenderTilesFromDataMatrix();
    }


    public void LoadDataToAtributeManager()
    {
        Atributes.LoadData(Money, Burried, CorpsesInQueue, CorpsesInStock, StockMax, IsHalfZombieSpawn, IsKiller, IsVirus, Lvl, XP);
        //Atributes.Money = Money;
        //Atributes.Burried = Burried;
        //Atributes.CorpsesInQueue = CorpsesInQueue;
        //Atributes.CorpsesInStock = CorpsesInStock;
        //Atributes.StockMax = StockMax;
        //Atributes.IsHalfZombieSpawn = IsHalfZombieSpawn;
        //Atributes.IsKiller = IsKiller;
        //Atributes.IsVirus = IsVirus;
        //Atributes.Lvl = Lvl;
        //Atributes.XP = XP;
    }


    public void LoadDataToTileDataMatrix()
    {
        int x = TileData.TileDataMatrix.GetLength(0);
        int y = TileData.TileDataMatrix.GetLength(1);

        for (int i = 0; i < x; i++)
        {
            for (int j = 0; j < y; j++)
            {
                TileData.TileDataMatrix[i, j].setClickCount(PureTileData[i, j].clickCount);
                TileData.TileDataMatrix[i, j].setTileType(PureTileData[i, j].tileType);
                TileData.TileDataMatrix[i, j].setTileBeforeZombifying(PureTileData[i, j].tileBeforeZombifying);
                TileData.TileDataMatrix[i, j].setZombieBreakoutStage(PureTileData[i, j].zombieBreakoutStage);
                TileData.TileDataMatrix[i, j].setMaxZombieBreakoutStage(PureTileData[i, j].maxZombieBreakoutStage);
                TileData.TileDataMatrix[i, j].setHasCoin(PureTileData[i, j].hasCoin);
                TileData.TileDataMatrix[i, j].setReadyForBurial(PureTileData[i, j].readyForBurial);
                TileData.TileDataMatrix[i, j].setHasBurriedInside(PureTileData[i, j].hasBurriedInside);
                TileData.TileDataMatrix[i, j].setIsGrave(PureTileData[i, j].isGrave);
                TileData.TileDataMatrix[i, j].setIsDecoration(PureTileData[i, j].isDecoration);
                TileData.TileDataMatrix[i, j].setIsProtected(PureTileData[i, j].isProtected);
                TileData.TileDataMatrix[i, j].setZombieChance(PureTileData[i, j].zombieChance);
				TileData.TileDataMatrix[i, j].IsChunkFree = PureTileData[i, j].IsChunkFree;
                TileData.TileDataMatrix[i, j].IsCollider = PureTileData[i, j].IsCollider;
                TileData.TileDataMatrix[i, j].IsChunkLeftNeighbour = PureTileData[i, j].IsChunkLeftNeighbour;

                //TileData.TileDataMatrix[i, j].cof(PureTileData[i, j].coffins);
                //TileData.TileDataMatrix[i, j].p(PureTileData[i, j].posI);
                //TileData.TileDataMatrix[i, j].setHasCoin(PureTileData[i, j].posJ);

            }
        }

       
    }


}
