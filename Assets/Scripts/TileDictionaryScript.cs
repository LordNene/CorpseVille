using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileDictionaryScript : MonoBehaviour
{
    public Tile GrassOne;
    public Tile GrassTwo;
    public Tile GrassThree;
    public Tile GrassFour;
    public Tile GrassFive;

    public Tile DigOne;
    public Tile DigTwo;
    public Tile DigThree;
    public Tile DigFour;
    public Tile DigFive;

	public Tile ChestTile;

	public Tile DigCoffinInside;
    public Tile GraveWithoutCross;
	public Tile GraveWithoutCrossZombieGrowing;

	public Tile GravePlebOne;
    public Tile GravePlebTwo;
    public Tile GravePlebThree;

    public Tile GraveCasioOne;
    public Tile GraveCasioTwo;
    public Tile GraveCasioThree;

    public Tile GraveRolexOne;
    public Tile GraveRolexTwo;
    public Tile GraveRolexThree;

	public Tile PlebGraveGroving1;
	public Tile PlebGraveGroving2;
	public Tile PlebGraveGroving3;
	public Tile PlebGraveGroving4;
	public Tile PlebGraveGroving5;

	public Tile CasioGraveGroving1;
	public Tile CasioGraveGroving2;
	public Tile CasioGraveGroving3;
	//public Tile CasioGraveGroving4;
	//public Tile CasioGraveGroving5;

	public Tile RolexGraveGroving1;
	public Tile RolexGraveGroving2;
	public Tile RolexGraveGroving3;
	//public Tile RolexGraveGroving4;
	//public Tile RolexGraveGroving5;

    public Tile MainBuilding;

    public Tile Road;
    public Tile HalfRoadHorizontal;
    public Tile HalfRoadVertical;
    public Tile HalfRoadCornerT;
    public Tile HalfRoadCornerB;
    public Tile HalfRoadCornerL;
    public Tile HalfRoadCornerR;

	public Tile FenceGate;
	public Tile CoffinInQueue1;
	public Tile CoffinInQueue2;

	public Tile Lamp;

	public Tile FenceHorizontal;
	public Tile FenceVertical;
    public Tile FenceCornerT;
    public Tile FenceCornerB;
    public Tile FenceCornerL;
    public Tile FenceCornerR;

    public Tile BenchHorizontal;
    public Tile BenchVertical;
    public Tile StatueHorizontal;
    public Tile StatueVertical;
    public Tile Tree1;
    public Tile Tree2;

	public Tile Casio1Collider;
	public Tile FenceHorizontalCollider;
	public Tile FenceVerticalCollider;
	public Tile StatueVerticalCollider;
	public Tile Pleb2Collider;
	public Tile basicCollider;

	public Tile ChunkPlusSymbol;

    public Tile[] grassArray;
	/*public Tile[] digArray;
	public Tile[] gravePlebArray;
	public Tile[] graveCasioArray;
	public Tile[] graveRolexArray;*/

	//public const int rows = 20, cols = 20;

	//public TileData[,] TileDataMatrix = new TileData[rows, cols];

	void Start()
    {
        Init();
    }

    public void Init()
    {
        grassArray = new Tile[] {
            GrassOne,
            GrassTwo,
            GrassThree,
            GrassFour,
            GrassFive
		};

		//for random selection - might be useful later
		/*digArray = new Tile[] {
			DigOne,
			DigTwo,
			DigThree,
			DigFour,
			DigFive
		};

		gravePlebArray = new Tile[] {
			GravePlebOne,
			GravePlebTwo,
			GravePlebThree
		};

		graveCasioArray = new Tile[] {
			GraveCasioOne,
			GraveCasioTwo,
			GraveCasioThree
		};

		graveRolexArray = new Tile[] {
			GraveRolexOne,
			GraveRolexTwo,
			GraveRolexThree
		};*/

		/*for (int i = 0; i < rows; i++)
		{
			for (int j = 0; j < cols; j++)
			{
				TileDataMatrix[i, j] = new TileData(i, j);
			}
		}*/
	}

	public Tile GetMainBuildingTile()
	{
		return MainBuilding;
	}

	public Tile GetRandomGrassTile()
    {
        return grassArray[Random.Range(0, grassArray.Length)];
	}

	//for random selection - might be useful later
	/*public Tile GetRandomDigTile()
	{
		return digArray[Random.Range(0, digArray.Length)];
	}

	public Tile GetRandomGravePlebTile()
	{
		return gravePlebArray[Random.Range(0, gravePlebArray.Length)];
	}

	public Tile GetRandomGraveCasioTile()
	{
		return graveCasioArray[Random.Range(0, graveCasioArray.Length)];
	}

	public Tile GetRandomGraveRolexTile()
	{
		return graveRolexArray[Random.Range(0, graveRolexArray.Length)];
	}*/

	/**/


	/*
	 
	 */

	/*
	 
	 */
	public void updateColliderTileSprite(TileType type, Vector3Int cellPosition, Tilemap tilemap)
	{
		switch (type)
		{
			case TileType.MainBuilding:
			case TileType.FenceCornerT:
			case TileType.FenceCornerB:
			case TileType.FenceCornerL:
			case TileType.FenceCornerR:
				//tilemap.SetTile(cellPosition, basicCollider);
				//break;
			//case TileType.GravePlebTwo:
			/*case TileType.GravePlebOne:
			case TileType.GravePlebTwo:
			case TileType.GravePlebThree:
			case TileType.GraveCasioOne:
			case TileType.GraveCasioTwo:
			case TileType.GraveCasioThree:
			case TileType.GraveRolexOne:
			case TileType.GraveRolexTwo:
			case TileType.GraveRolexThree:
			case TileType.PlebGraveGroving1:
			case TileType.PlebGraveGroving2:
			case TileType.PlebGraveGroving3:
			case TileType.PlebGraveGroving4:
			case TileType.PlebGraveGroving5:
			case TileType.CasioGraveGroving1:
			case TileType.CasioGraveGroving2:
			case TileType.CasioGraveGroving3:
			case TileType.RolexGraveGroving1:
			case TileType.RolexGraveGroving2:
			case TileType.RolexGraveGroving3:
				tilemap.SetTile(cellPosition, Pleb2Collider);
				break;*/
			/*case TileType.GraveCasioOne:
				tilemap.SetTile(cellPosition, Casio1Collider);
				break;*/
			case TileType.FenceHorizontal:
				//tilemap.SetTile(cellPosition, FenceHorizontalCollider);
				//break;
			case TileType.FenceVertical:
				//tilemap.SetTile(cellPosition, FenceVerticalCollider);
				//break;
			case TileType.StatueVertical:
			case TileType.StatueHorizontal:
			case TileType.Lamp:
			case TileType.Tree1:
			case TileType.Tree2:
				//tilemap.SetTile(cellPosition, StatueVerticalCollider);
				//break;
				tilemap.SetTile(cellPosition, basicCollider);
				break;
			default:
				tilemap.SetTile(cellPosition, null);
				break;
		}
	}

	public void updateTileSprite(TileType type, Vector3Int cellPosition, Tilemap tilemap)
	{
		switch (type)
		{
			case TileType.Grass:
				tilemap.SetTile(cellPosition, GetRandomGrassTile());
				break;
			case TileType.DigOne:
				tilemap.SetTile(cellPosition, DigOne);
				break;
			case TileType.DigTwo:
				tilemap.SetTile(cellPosition, DigTwo);
				break;
			case TileType.DigThree:
				tilemap.SetTile(cellPosition, DigThree);
				break;
			case TileType.DigFour:
				tilemap.SetTile(cellPosition, DigFour);
				break;
			case TileType.DigFive:
				tilemap.SetTile(cellPosition, DigFive);
				break;
			case TileType.ChestTile:
				tilemap.SetTile(cellPosition, ChestTile);
				break;
			case TileType.DigCoffinInside:
				tilemap.SetTile(cellPosition, DigCoffinInside);
				break;
			case TileType.GraveWithoutCross:
				tilemap.SetTile(cellPosition, GraveWithoutCross);
				break;
			case TileType.GraveWithoutCrossZombieGrowing:
				tilemap.SetTile(cellPosition, GraveWithoutCrossZombieGrowing);
				break;
			case TileType.GravePlebOne:
				tilemap.SetTile(cellPosition, GravePlebOne);
				break;
			case TileType.GravePlebTwo:
				tilemap.SetTile(cellPosition, GravePlebTwo);
				break;
			case TileType.GravePlebThree:
				tilemap.SetTile(cellPosition, GravePlebThree);
				break;
			case TileType.GraveCasioOne:
				tilemap.SetTile(cellPosition, GraveCasioOne);
				break;
			case TileType.GraveCasioTwo:
				tilemap.SetTile(cellPosition, GraveCasioTwo);
				break;
			case TileType.GraveCasioThree:
				tilemap.SetTile(cellPosition, GraveCasioThree);
				break;
			case TileType.GraveRolexOne:
				tilemap.SetTile(cellPosition, GraveRolexOne);
				break;
			case TileType.GraveRolexTwo:
				tilemap.SetTile(cellPosition, GraveRolexTwo);
				break;
			case TileType.GraveRolexThree:
				tilemap.SetTile(cellPosition, GraveRolexThree);
				break;
			case TileType.PlebGraveGroving1:
				tilemap.SetTile(cellPosition, PlebGraveGroving1);
				break;
			case TileType.PlebGraveGroving2:
				tilemap.SetTile(cellPosition, PlebGraveGroving2);
				break;
			case TileType.PlebGraveGroving3:
				tilemap.SetTile(cellPosition, PlebGraveGroving3);
				break;
			case TileType.PlebGraveGroving4:
				tilemap.SetTile(cellPosition, PlebGraveGroving4);
				break;
			case TileType.PlebGraveGroving5:
				tilemap.SetTile(cellPosition, PlebGraveGroving5);
				break;
			case TileType.CasioGraveGroving1:
				tilemap.SetTile(cellPosition, CasioGraveGroving1);
				break;
			case TileType.CasioGraveGroving2:
				tilemap.SetTile(cellPosition, CasioGraveGroving2);
				break;
			case TileType.CasioGraveGroving3:
				tilemap.SetTile(cellPosition, CasioGraveGroving3);
				break;
			/*case TileType.CasioGraveGroving4:
				tilemap.SetTile(cellPosition, CasioGraveGroving4);
				break;
			case TileType.CasioGraveGroving5:
				tilemap.SetTile(cellPosition, CasioGraveGroving5);
				break;*/
			case TileType.RolexGraveGroving1:
				tilemap.SetTile(cellPosition, RolexGraveGroving1);
				break;
			case TileType.RolexGraveGroving2:
				tilemap.SetTile(cellPosition, RolexGraveGroving2);
				break;
			case TileType.RolexGraveGroving3:
				tilemap.SetTile(cellPosition, RolexGraveGroving3);
				break;
			/*case TileType.RolexGraveGroving4:
				tilemap.SetTile(cellPosition, RolexGraveGroving4);
				break;
			case TileType.RolexGraveGroving5:
				tilemap.SetTile(cellPosition, RolexGraveGroving5);
				break;*/
			case TileType.FenceGate:
				tilemap.SetTile(cellPosition, FenceGate);
				break;
			case TileType.Road:
				tilemap.SetTile(cellPosition, Road);
				break;
			case TileType.HalfRoadHorizontal:
				tilemap.SetTile(cellPosition, HalfRoadHorizontal);
				break;
			case TileType.HalfRoadVertical:
				tilemap.SetTile(cellPosition, HalfRoadVertical);
				break;
			case TileType.HalfRoadCornerT:
				tilemap.SetTile(cellPosition, HalfRoadCornerT);
				break;
			case TileType.HalfRoadCornerB:
				tilemap.SetTile(cellPosition, HalfRoadCornerB);
				break;
			case TileType.HalfRoadCornerR:
				tilemap.SetTile(cellPosition, HalfRoadCornerR);
				break;
			case TileType.HalfRoadCornerL:
				tilemap.SetTile(cellPosition, HalfRoadCornerL);
				break;
			case TileType.CoffinInQueue1:
				tilemap.SetTile(cellPosition, CoffinInQueue1);
				break;
			case TileType.CoffinInQueue2:
				tilemap.SetTile(cellPosition, CoffinInQueue2);
				break;
			case TileType.MainBuilding:
				tilemap.SetTile(cellPosition, MainBuilding);
				break;
			case TileType.Lamp:
				tilemap.SetTile(cellPosition, Lamp);
				break;
			case TileType.FenceHorizontal:
				tilemap.SetTile(cellPosition, FenceHorizontal);
				break;
			case TileType.FenceVertical:
				tilemap.SetTile(cellPosition, FenceVertical);
				break;
			case TileType.FenceCornerT:
				tilemap.SetTile(cellPosition, FenceCornerT);
				break;
			case TileType.FenceCornerB:
				tilemap.SetTile(cellPosition, FenceCornerB);
				break;
			case TileType.FenceCornerL:
				tilemap.SetTile(cellPosition, FenceCornerL);
				break;
			case TileType.FenceCornerR:
				tilemap.SetTile(cellPosition, FenceCornerR);
				break;
			case TileType.BenchHorizontal:
				tilemap.SetTile(cellPosition, BenchHorizontal);
				break;
			case TileType.BenchVertical:
				tilemap.SetTile(cellPosition, BenchVertical);
				break;
			case TileType.StatueHorizontal:
				tilemap.SetTile(cellPosition, StatueHorizontal);
				break;
			case TileType.StatueVertical:
				tilemap.SetTile(cellPosition, StatueVertical);
				break;
			case TileType.Tree1:
				tilemap.SetTile(cellPosition, Tree1);
				break;
			case TileType.Tree2:
				tilemap.SetTile(cellPosition, Tree2);
				break;
			case TileType.ChunkPlusSymbol:
				tilemap.SetTile(cellPosition, ChunkPlusSymbol);
				break;
			default:
				break;

		}
	}

}
