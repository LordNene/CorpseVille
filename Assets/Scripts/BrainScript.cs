using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEditor;
using UnityEngine.Tilemaps;

public class BrainScript : MonoBehaviour
{

    public TileDictionaryScript TileDictionary;

    public Tilemap MainTileMap;
	public TileDataManager MainTileDataManager;

	private float _timer;
    private bool _nonGeneratedYet;


    void Start()
    {
        Init();   
    }

    void Init()
    {
        _timer = 0f;
        _nonGeneratedYet = false;
    }

    void Update()
    {
        _timer += Time.deltaTime;

        if (!_nonGeneratedYet && _timer > 0)
        {
            Debug.Log("Generating");
            GenerateFirstGrassChunk();
            _nonGeneratedYet = true;
        }
    }


    public void GenerateFirstGrassChunk()
    {
        for (int i = 0; i < 10; i++)
        {
            for (int j = 0; j < 10; j++)
            {
				if (i == 6 && j == 0) continue; //main entrance
				if (i == 6 && j == 1 || i == 5 && j == 1) continue; //two road blocks near main entrance
				if ((i == 5 || i == 4 || i == 3) && j == 1) continue; //coffin blocks
				if (i == 7 && j == 1 || i == 8 && j == 1 || i == 7 && j == 0 || i == 8 && j == 0) continue; //main building
				MainTileMap.SetTile(new Vector3Int(i, -j, 0), TileDictionary.GetRandomGrassTile());
				MainTileDataManager.TileDataMatrix[i, j].IsChunkFree = false;
            }
        }

		//Tile mainBuilding = TileDictionary.MainBuilding;
		//mainBuilding.transform.m01 = 1.29;

		//MainTileMap.SetTile(new Vector3Int(0, 0, 0), mainBuilding);
		/*MainTileMap.SetTile(new Vector3Int(0, 9, 0), null); 
		MainTileMap.SetTile(new Vector3Int(0, 8, 0), null); 
		MainTileMap.SetTile(new Vector3Int(-1, 9, 0), null); 
		MainTileMap.SetTile(new Vector3Int(-1, 8, 0), null); 
		*/

	}




}
