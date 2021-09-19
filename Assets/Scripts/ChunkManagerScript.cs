using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class ChunkManagerScript : MonoBehaviour
{
	public const int rows = 40, cols = 40;
	public TileDataManager MainTileDataManager;
	public TileDictionaryScript TileDictionary;
	public Tilemap MainTileMap;

	private float _timer;
	private bool _nonGeneratedYet;
	private int offset = 4;

	// Start is called before the first frame update
	void Start()
    {
		_timer = 0f;
		_nonGeneratedYet = false;
		MainTileDataManager = FindObjectOfType<TileDataManager>();
    }

    // Update is called once per frame
    /*void Update()
    {
		_timer += Time.deltaTime;
		if (!_nonGeneratedYet && _timer > 0)
		{
			Debug.Log("creating initial chunk");
			createChunkPlusSymbolTiles(0, 10, 0, 10, MainTileDataManager.TileDataMatrix[10, 4], MainTileDataManager.TileDataMatrix[4, 10], true, true); //initial chunk
			_nonGeneratedYet = true;
		}
	}*/

	public void createChunkPlusSymbolTiles(int startI, int endI, int startJ, int endJ, TileData chunkTileRight, TileData chunkTileLeft, bool placeLeft, bool placeRight)
	{
		TileData leftTileWhereAPlusSymbolWouldHaveBeen = MainTileDataManager.TileDataMatrix[chunkTileRight.getPosI(), -chunkTileRight.getPosJ()];
		TileData rightTileWhereAPlusSymbolWouldHaveBeen = MainTileDataManager.TileDataMatrix[chunkTileLeft.getPosI(), -chunkTileLeft.getPosJ()];

		if (/*startI + 10 < cols && */leftTileWhereAPlusSymbolWouldHaveBeen.IsChunkFree && placeLeft)
		{
			for (int i = startI + 10; i < endI + 10; i++)
			{
				for (int j = startJ; j < endJ; j++)
				{
					MainTileDataManager.TileDataMatrix[i, j].IsChunkLeftNeighbour = true;
					MainTileDataManager.TileDataMatrix[i, j].MainChunkTile = chunkTileRight;
				}
			}

			if (endI + 10 < cols)
			{
				chunkTileRight.setTileType(TileType.ChunkPlusSymbol);
				chunkTileRight.IsChunkFree = false;
				TileDictionary.updateTileSprite(chunkTileRight.getTileType(), new Vector3Int(chunkTileRight.getPosI(), chunkTileRight.getPosJ(), 0), MainTileMap);
			}
		}
		
		if (/*startJ + 10 < rows && */rightTileWhereAPlusSymbolWouldHaveBeen.IsChunkFree && placeRight)
		{
			for (int i = startI; i < endI; i++)
			{
				for (int j = startJ + 10; j < endJ + 10; j++)
				{
					MainTileDataManager.TileDataMatrix[i, j].IsChunkLeftNeighbour = false;
					MainTileDataManager.TileDataMatrix[i, j].MainChunkTile = chunkTileLeft;
				}
			}

			if (endJ + 10 < cols)
			{
				chunkTileLeft.setTileType(TileType.ChunkPlusSymbol);
				chunkTileLeft.IsChunkFree = false;
				TileDictionary.updateTileSprite(TileType.ChunkPlusSymbol, new Vector3Int(chunkTileLeft.getPosI(), chunkTileLeft.getPosJ(), 0), MainTileMap);
			}
		}
	}

	public void fillChunkWithGrass(ChunkData chunk)
	{
		for (int i = chunk.StartIndexI; i < chunk.EndIndexI; i++)
		{
			for (int j = chunk.StartIndexJ; j < chunk.EndIndexJ; j++)
			{
				MainTileMap.SetTile(new Vector3Int(i, -j, 0), TileDictionary.GetRandomGrassTile());
				MainTileDataManager.TileDataMatrix[i, j].IsChunkFree = false;
			}
		}
	}

}
