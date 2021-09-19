using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public enum TileType
{
	Grass = 0,
	DigOne = 1,
	DigTwo = 2,
	DigThree = 3,
	DigFour = 4,
	DigFive = 5,
	DigCoffinInside = 6,
	GraveWithoutCross = 7,
	GraveWithoutCrossZombieGrowing,
	GravePlebOne,
	GravePlebTwo,
	GravePlebThree,
	GraveCasioOne,
	GraveCasioTwo,
	GraveCasioThree,
	GraveRolexOne,
	GraveRolexTwo,
	GraveRolexThree,
	PlebGraveGroving1,
	PlebGraveGroving2,
	PlebGraveGroving3,
	PlebGraveGroving4,
	PlebGraveGroving5,
	CasioGraveGroving1,
	CasioGraveGroving2,
	CasioGraveGroving3,
	//CasioGraveGroving4,
	//CasioGraveGroving5,
	RolexGraveGroving1,
	RolexGraveGroving2,
	RolexGraveGroving3,
	//RolexGraveGroving4,
	//RolexGraveGroving5,
	Lamp,
	Road,
	HalfRoadHorizontal,
	HalfRoadVertical,
	HalfRoadCornerT,
	HalfRoadCornerB,
	HalfRoadCornerL,
	HalfRoadCornerR,
	CoffinInQueue1,
	CoffinInQueue2,
	FenceGate,
	FenceHorizontal,
	FenceVertical,
	FenceCornerT,
	FenceCornerB,
	FenceCornerL,
	FenceCornerR,
	BenchHorizontal,
	BenchVertical,
	StatueHorizontal,
	StatueVertical,
	Tree1,
	Tree2,
	ChestTile,
	MainBuilding = 900,
	ChunkPlusSymbol,
	None
}

public class TileData /*: MonoBehaviour*/
{ 
	//any added atributes need to be added also to save system
	private int clickCount;
	private bool hasCoin;  
	private bool readyForBurial; 
	private bool hasBurriedInside; 
	private bool isGrave; 
	private bool isDecoration; 
	private bool isProtected;
	private TileType tileType;
	private TileType tileBeforeZombifying;
	private float zombieChance; //based on type of grave
	private int zombieBreakoutStage;
	private int maxZombieBreakoutStage;

	private int coffins;

	private int posI, posJ;

	//private bool isChunkFree = true;
	//private TileData mainChunkTile = null;

	public bool IsCollider { get; set; }
	public bool IsChunkFree { get; set; }
	public bool IsChunkLeftNeighbour { get; set; }
	public TileData MainChunkTile { get; set; }

	public int Health { get; set; }

	public AtributesManagerScript AttributeManager;
	public TileDictionaryScript TileDictionary;
	public Tilemap MainTileMap;

	public void increaseClickCount() { clickCount++; }
	public void decreaseClickCount() { clickCount--; }
	public void setClickCount(int c) { clickCount = c; }
	public int getClickCount() { return clickCount; }
	public void setHasCoin(bool b) { hasCoin = b; }
	public bool getHasCoin() { return hasCoin; }
	public void setReadyForBurial(bool b) { readyForBurial = b; }
	public bool getReadyForBurial() { return readyForBurial; }
	public void setHasBurriedInside(bool b) { hasBurriedInside = b; }
	public bool getHasBurriedInside() { return hasBurriedInside; }
	public void setZombieChance(float f) { zombieChance = f; }
	public float getZombieChance() { return zombieChance; }
	public void setZombieBreakoutStage (int f) { zombieBreakoutStage = f; }
	public void increaseZombieBreakoutStage() { zombieBreakoutStage++; }
	public void decreaseZombieBreakoutStage() { zombieBreakoutStage--; }
	public int getZombieBreakoutStage() { return zombieBreakoutStage; }
	public void setMaxZombieBreakoutStage(int f) { maxZombieBreakoutStage = f; }
	public int getMaxZombieBreakoutStage() { return maxZombieBreakoutStage; }

	public bool getIsGrave() { return isGrave; }
	public void setIsGrave(bool b) { isGrave = b; }
	public bool getIsDecoration() { return isDecoration; }
	public void setIsDecoration(bool b) { isDecoration = b; }
	public bool getIsProtected() { return isProtected; }
	public void setIsProtected(bool b) { isProtected = b; }

	public void setTileType(TileType t) {
		//if ((int)t < (int)TileType.MainBuilding)
		//{
			tileType = t;
		//}
	}
	public TileType getTileType() { return tileType; }

	public void setTileBeforeZombifying(TileType t) { tileBeforeZombifying = t; }
	public TileType getTileBeforeZombifying() { return tileBeforeZombifying; }

	public int getPosI() { return posI; }
	public int getPosJ() { return -posJ; }

	public void increaseCoffins() { coffins++; }
	public int getCoffins() { return coffins; }

	public TileData(int i, int j)
	{
		clickCount = 0;
		
		hasCoin = false;
		readyForBurial = false;
		hasBurriedInside = false;
		isGrave = false;
		isDecoration = false;
		isProtected = false;
		zombieChance = 120;
		zombieBreakoutStage = 0;
		maxZombieBreakoutStage = 0;
		tileType = TileType.Grass;
		tileBeforeZombifying = TileType.GraveWithoutCross;
		posI = i;
		posJ = j;

		IsChunkFree = true;

		coffins = 0;

		Health = 3;
	}
}
