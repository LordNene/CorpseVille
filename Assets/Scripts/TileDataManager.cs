using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public class TileDataManager : MonoBehaviour
{
	public AtributesManagerScript AttributeManager;
	public AchievementManager AchievementManager;
	public TileDictionaryScript TileDictionary;
	public Tilemap MainTileMap;
	public Tilemap ColliderTileMap;
	public ChunkManagerScript chunkManager;
	public AdController AdControllerScript;

    public GameObject TreasurePopUpPrefab;
    public GameObject ExpandPopUpPrefab;

	public const int rows = 40, cols = 40;
	private const int baseZombieChance = 150;
	private const int plebZombieChance = 200;
	private const int casioZombieChance = 220;
	private const int rolexZombieChance = 250;
	private bool tmpFixShit = false;

	public TileData[,] TileDataMatrix = new TileData[rows, cols];

	private float _timer;

	public ObjectSpawner zombieSpawner;
	public GameObject MainBuildingMenu;
	public UIScript ToolTip;
	private bool _nonGeneratedYet;

	void Start()
	{
		zombieSpawner = FindObjectOfType<ObjectSpawner>();
		Init();
	}

	void Init()
	{
		_timer = 0f;
		_nonGeneratedYet = false;
		
	}

    public void GenerateNewGameHardFixJebemNato()
    {
		//SetEverythingToFuckingDefault();
		generateTileDataForNewGame();

		chunkManager.createChunkPlusSymbolTiles(0, 10, 0, 10, TileDataMatrix[10, 4], TileDataMatrix[4, 10], true, true);
        Debug.Log("KUBOOOO");
    }

    public void SetEverythingToFuckingDefault()
    {
        //TODO JOJO prosim ta zrob to lebo ma jebne


    }


    public void generateTileDataForNewGame()
	{
		for (int i = 0; i < rows; i++)
		{
			for (int j = 0; j < cols; j++)
			{
				if (i == 7 && j == 0) //one main building tile
				{
					TileDataMatrix[i, j] = new TileData(i, j);
					TileDataMatrix[i, j].setTileType(TileType.MainBuilding);
					TileDataMatrix[i, j].setClickCount((int)TileType.MainBuilding);
					TileDataMatrix[i, j].IsChunkFree = false;
					TileDataMatrix[i, j].IsCollider = true;
					TileDictionary.updateColliderTileSprite(TileType.MainBuilding, new Vector3Int(TileDataMatrix[i, j].getPosI(), TileDataMatrix[i, j].getPosJ(), 0), ColliderTileMap);
				}
				else if (i == 7 && j == 1 || i == 8 && j == 1 || i == 8 && j == 0) //three remaining main building tiles
				{
					TileDataMatrix[i, j] = TileDataMatrix[7, 0];
					TileDictionary.updateColliderTileSprite(TileType.MainBuilding, new Vector3Int(i, -j, 0), ColliderTileMap);
				}
				else
				{
					TileDataMatrix[i, j] = new TileData(i, j);
					TileData CurrentTileData = TileDataMatrix[i, j];
					//if (i > 10 || j > 10) MainTileMap.SetTile(new Vector3Int(i, -j, 0), null);
					
					if (i > 9 || j > 9)
					{
						MainTileMap.SetTile(new Vector3Int(i, -j, 0), null);
					}
					else if (i == 6 && j == 0) continue; //main entrance
					else if (i == 6 && j == 1 || i == 5 && j == 1) continue; //two road blocks near main entrance
					else
					{
						MainTileMap.SetTile(new Vector3Int(i, -j, 0), TileDictionary.GetRandomGrassTile());
					}
					 
					setColliderTileToNull(CurrentTileData);
					CurrentTileData.IsCollider = false;
				}

				//if (i == -10 || i == 11 || j == -10 || j == 11) //fences
				//{
				//	if (TileDataMatrix[i, j].getTileType() != TileType.MainBuilding)
				//	{
				//		TileDataMatrix[i, j].setIsDecoration(true);
				//		TileDataMatrix[i, j].setIsProtected(true);
				//		//TileDataMatrix[i, j].setTileType(TileType.FenceHorizontal);
				//	}
				//}
			}
		}

		for (int i = 0; i < 10; i++)
		{
			for (int j = 0; j < 10; j++)
			{
				TileDataMatrix[i, j].IsChunkFree = false;
			}
		}

		//that one road block before gate
		TileDataMatrix[6, 1].setIsDecoration(true);
		TileDataMatrix[6, 1].setIsProtected(true);
		TileDataMatrix[6, 1].IsChunkFree = false;
		//TileDataMatrix[6, 1].IsCollider = true;
		//TileDictionary.updateColliderTileSprite(TileDataMatrix[6, 1].getTileType(), new Vector3Int(TileDataMatrix[6, 1].getPosI(), TileDataMatrix[6, 1].getPosJ(), 0), ColliderTileMap);

		//gate
		TileDataMatrix[6, 0].setIsDecoration(true);
		TileDataMatrix[6, 0].setIsProtected(true);
		TileDataMatrix[6, 0].IsChunkFree = false;
		//TileDataMatrix[6, 0].IsCollider = true;
		//TileDictionary.updateColliderTileSprite(TileDataMatrix[6, 0].getTileType(), new Vector3Int(TileDataMatrix[6, 0].getPosI(), TileDataMatrix[6, 0].getPosJ(), 0), ColliderTileMap);

		//road tiles for coffins
		TileData RoadTile1 = TileDataMatrix[5, 1];
		TileData RoadTile2 = TileDataMatrix[4, 1];
		TileData RoadTile3 = TileDataMatrix[3, 1];

		RoadTile1.setClickCount((int)TileType.Road);
		RoadTile1.setTileType(TileType.Road);
		RoadTile1.setIsDecoration(true);
		RoadTile1.setIsProtected(true);
		RoadTile1.IsChunkFree = false;
		RoadTile1.IsCollider = true;

		RoadTile2.setClickCount((int)TileType.Road);
		RoadTile2.setTileType(TileType.Road);
		RoadTile2.setIsDecoration(true);
		RoadTile2.setIsProtected(true);
		RoadTile2.IsChunkFree = false;
		RoadTile2.IsCollider = true;

		RoadTile3.setClickCount((int)TileType.Road);
		RoadTile3.setTileType(TileType.Road);
		RoadTile3.setIsDecoration(true);
		RoadTile3.setIsProtected(true);
		RoadTile3.IsChunkFree = false;
		RoadTile3.IsCollider = true;

		TileDictionary.updateTileSprite(RoadTile1.getTileType(), new Vector3Int(RoadTile1.getPosI(), RoadTile1.getPosJ(), 0), MainTileMap);
		TileDictionary.updateTileSprite(RoadTile2.getTileType(), new Vector3Int(RoadTile2.getPosI(), RoadTile2.getPosJ(), 0), MainTileMap);
		TileDictionary.updateTileSprite(RoadTile3.getTileType(), new Vector3Int(RoadTile3.getPosI(), RoadTile3.getPosJ(), 0), MainTileMap);

		//colliders
		TileDictionary.updateColliderTileSprite(RoadTile1.getTileType(), new Vector3Int(RoadTile1.getPosI(), RoadTile1.getPosJ(), 0), ColliderTileMap);
		TileDictionary.updateColliderTileSprite(RoadTile2.getTileType(), new Vector3Int(RoadTile2.getPosI(), RoadTile2.getPosJ(), 0), ColliderTileMap);
		TileDictionary.updateColliderTileSprite(RoadTile3.getTileType(), new Vector3Int(RoadTile3.getPosI(), RoadTile3.getPosJ(), 0), ColliderTileMap);
	}

	void Update()
	{
		//timer for zombie chance and breaking based on tomb and
		_timer += Time.deltaTime;

		if (!_nonGeneratedYet && _timer > 0)
		{
			Debug.Log("are you fakin sirius");
			generateTileDataForNewGame();
			chunkManager.createChunkPlusSymbolTiles(0, 10, 0, 10, TileDataMatrix[10, 4], TileDataMatrix[4, 10], true, true); //initial chunk
			_nonGeneratedYet = true;
		}

		if (_timer > 10 && !tmpFixShit && AttributeManager.IsVirus && AttributeManager.getCorpsesInQueue() < 5)
		{
			addCoffinsToQueue();
			tmpFixShit = true;
		}

		if (_timer > 20)
		{
			_timer = 0f;

			for (int i = 0; i < rows; i++)
			{
				for (int j = 0; j < cols; j++)
				{
					TileData CurrentTileData = TileDataMatrix[i, j];
					//if (CurrentTileData.getHasBurriedInside())
					calculateZombieChance(CurrentTileData);
				}
			}

			if (AttributeManager.getCorpsesInQueue() < 5)
			{
				addCoffinsToQueue();
			}

			tmpFixShit = false;
		}

		if (AttributeManager.IsKiller)
		{
			addCoffinsToQueue();
			addCoffinsToQueue();
			addCoffinsToQueue();
			AttributeManager.IsKiller = false;
		}
	}

	public void rerenderTilesFromDataMatrix()
	{
		for (int i = 0; i < rows; i++)
		{
			for (int j = 0; j < cols; j++)
			{
				/*TileData CurrentTileData = TileDataMatrix[i, j];
				Vector3Int pos = new Vector3Int(CurrentTileData.getPosI(), CurrentTileData.getPosJ(), 0);
				MainTileMap.SetTile(pos, null);*/
				if (i == 7 && j == 0) //one main building tile
				{
					TileDataMatrix[i, j].setTileType(TileType.MainBuilding);
					TileDataMatrix[i, j].setClickCount((int)TileType.MainBuilding);
					TileDataMatrix[i, j].IsChunkFree = false;
					TileDataMatrix[i, j].IsCollider = true;
					TileDictionary.updateColliderTileSprite(TileType.MainBuilding, new Vector3Int(TileDataMatrix[i, j].getPosI(), TileDataMatrix[i, j].getPosJ(), 0), ColliderTileMap);

				}
				else if (i == 7 && j == 1 || i == 8 && j == 1 || i == 8 && j == 0) //three remaining main building tiles
				{
					TileDataMatrix[i, j] = TileDataMatrix[7, 0];
					TileDictionary.updateColliderTileSprite(TileType.MainBuilding, new Vector3Int(i, -j, 0), ColliderTileMap);
				}
				else if (i == 6 && j == 1 || i == 6 && j == 0) // gate and that one road block before gate
				{
					TileDictionary.updateColliderTileSprite(TileDataMatrix[i, j].getTileType(), new Vector3Int(TileDataMatrix[i, j].getPosI(), TileDataMatrix[i, j].getPosJ(), 0), ColliderTileMap);
				}
				else
				{
					TileData CurrentTileData = TileDataMatrix[i, j];
					Vector3Int pos = new Vector3Int(CurrentTileData.getPosI(), CurrentTileData.getPosJ(), 0);
					if (!CurrentTileData.IsChunkFree)
					{
						TileDictionary.updateTileSprite(CurrentTileData.getTileType(), pos, MainTileMap);
						TileDictionary.updateColliderTileSprite(CurrentTileData.getTileType(), pos, ColliderTileMap);
					}
					else
					{
						MainTileMap.SetTile(pos, null);
						setColliderTileToNull(CurrentTileData);
					}
				}
			}
		}
		
	}

	public void calculateZombieChance(TileData CurrentTileData)
	{
		float chance = CurrentTileData.getZombieChance();
		if (CurrentTileData.getHasBurriedInside())
		{
			if (AttributeManager.IsHalfZombieSpawn)
			{
				chance *= 2;
			}
			float diceRoll = Random.Range(0, chance);

			if (diceRoll < 10)
			{
				zombify(CurrentTileData);
				ToolTip.DisplayToolTip("Braaaaaaaaaains!");
				//Debug.Log(CurrentTileData.getZombieBreakoutStage());
			}
		}
	}

	public bool calculateChestChance()
	{
		float diceRoll = Random.Range(0, 200);
		//if (diceRoll < 10) {}
		return diceRoll < 10;
	}

	public void zombify(TileData CurrentTileData)
	{
		if (CurrentTileData.getZombieBreakoutStage() == CurrentTileData.getMaxZombieBreakoutStage())
		{
			//zombie left

			zombieSpawner.SpawnZombie(CurrentTileData.getPosI(), CurrentTileData.getPosJ());

			AttributeManager.decreaseBurried();
			CurrentTileData.setIsGrave(false);
			CurrentTileData.setIsDecoration(false);

			CurrentTileData.setClickCount((int)TileType.DigFive);
			CurrentTileData.setTileType(TileType.DigFive);
			CurrentTileData.setReadyForBurial(true);
			CurrentTileData.setHasBurriedInside(false);
			CurrentTileData.setZombieChance(baseZombieChance);
			//clear(CurrentTileData.getPosI(), CurrentTileData.getPosJ());
			CurrentTileData.setMaxZombieBreakoutStage(0);
			CurrentTileData.setZombieBreakoutStage(0);
		}
		else
		{
			switch (CurrentTileData.getTileType())
			{
				case TileType.GraveWithoutCross:
					CurrentTileData.setTileBeforeZombifying(TileType.GraveWithoutCross);
					CurrentTileData.setTileType(TileType.GraveWithoutCrossZombieGrowing);
					break;

				case TileType.GravePlebOne:
				case TileType.GravePlebTwo:
				case TileType.GravePlebThree:
					CurrentTileData.setTileBeforeZombifying(CurrentTileData.getTileType());
					CurrentTileData.setTileType(TileType.PlebGraveGroving1);
					break;
				case TileType.PlebGraveGroving1:
					CurrentTileData.setTileType(TileType.PlebGraveGroving2);
					break;
				case TileType.PlebGraveGroving2:
					CurrentTileData.setTileType(TileType.PlebGraveGroving3);
					break;
				case TileType.PlebGraveGroving3:
					CurrentTileData.setTileType(TileType.PlebGraveGroving4);
					break;
				case TileType.PlebGraveGroving4:
					CurrentTileData.setTileType(TileType.PlebGraveGroving5);
					break;

				case TileType.GraveCasioOne:
				case TileType.GraveCasioTwo:
				case TileType.GraveCasioThree:
					CurrentTileData.setTileBeforeZombifying(CurrentTileData.getTileType());
					CurrentTileData.setTileType(TileType.CasioGraveGroving1);
					break;
				case TileType.CasioGraveGroving1:
					CurrentTileData.setTileType(TileType.CasioGraveGroving2);
					break;
				case TileType.CasioGraveGroving2:
					CurrentTileData.setTileType(TileType.CasioGraveGroving3);
					break;

				case TileType.GraveRolexOne:
				case TileType.GraveRolexTwo:
				case TileType.GraveRolexThree:
					CurrentTileData.setTileBeforeZombifying(CurrentTileData.getTileType());
					CurrentTileData.setTileType(TileType.RolexGraveGroving1);
					break;
				case TileType.RolexGraveGroving1:
					CurrentTileData.setTileType(TileType.RolexGraveGroving2);
					break;
				case TileType.RolexGraveGroving2:
					CurrentTileData.setTileType(TileType.RolexGraveGroving3);
					break;
			}

			CurrentTileData.increaseZombieBreakoutStage();
		}

		CurrentTileData.setClickCount((int)CurrentTileData.getTileType());
		TileDictionary.updateTileSprite(CurrentTileData.getTileType(), new Vector3Int(CurrentTileData.getPosI(), CurrentTileData.getPosJ(), 0), MainTileMap);
	}

	public void unzombify(TileData CurrentTileData)
	{
		TileType tileBefore = CurrentTileData.getTileBeforeZombifying();

		switch (CurrentTileData.getTileType())
		{
			case TileType.GraveWithoutCrossZombieGrowing:
			case TileType.PlebGraveGroving1:
			case TileType.CasioGraveGroving1:
			case TileType.RolexGraveGroving1:
				CurrentTileData.setTileType(tileBefore);
				break;

			case TileType.PlebGraveGroving2:
				CurrentTileData.setTileType(TileType.PlebGraveGroving1);
				break;
			case TileType.PlebGraveGroving3:
				CurrentTileData.setTileType(TileType.PlebGraveGroving2);
				break;
			case TileType.PlebGraveGroving4:
				CurrentTileData.setTileType(TileType.PlebGraveGroving3);
				break;
			case TileType.PlebGraveGroving5:
				CurrentTileData.setTileType(TileType.PlebGraveGroving4);
				break;

			case TileType.CasioGraveGroving2:
				CurrentTileData.setTileType(TileType.CasioGraveGroving1);
				break;
			case TileType.CasioGraveGroving3:
				CurrentTileData.setTileType(TileType.CasioGraveGroving2);
				break;
				
			case TileType.RolexGraveGroving2:
				CurrentTileData.setTileType(TileType.RolexGraveGroving1);
				break;
			case TileType.RolexGraveGroving3:
				CurrentTileData.setTileType(TileType.RolexGraveGroving2);
				break;
		}

		CurrentTileData.setClickCount((int)CurrentTileData.getTileType());
		CurrentTileData.decreaseZombieBreakoutStage();
		TileDictionary.updateTileSprite(CurrentTileData.getTileType(), new Vector3Int(CurrentTileData.getPosI(), CurrentTileData.getPosJ(), 0), MainTileMap);
	}

	/*public void checkingAndDecreasingMoney(int price)
	{
		
	}*/

	public void clicked(int i, int j)
	{
		if (i < 0 || j < 0 || i > rows || j > cols) return;

		TileData CurrentTile = TileDataMatrix[i, j];
		TileType CurrentTileType = CurrentTile.getTileType();

		if (CurrentTileType == TileType.ChunkPlusSymbol) // click on the plus symbol
		{
            //todo expand experiment
            Instantiate(ExpandPopUpPrefab, ToolTip.transform).GetComponent<ExpandPopUpScript>()
                .Init(this, i, j, AttributeManager, ToolTip, chunkManager);
            return;
        }

		if (CurrentTile.IsChunkFree && MainTileMap.GetTile(new Vector3Int(CurrentTile.getPosI(), CurrentTile.getPosJ(), 0)) == null) // clicking on a tile in a not bought chunk
		{
			ToolTip.DisplayToolTip("There be lions.");
			return;
		}

		if (CurrentTileType == TileType.MainBuilding) //click on main building
		{
			MainBuildingMenu.SetActive(true);
			return;
		}

		if ( (i == 5 || i == 4 || i == 3) && j == 1) //coffin blocks
		{
			if (AttributeManager.CorpsesInStock == AttributeManager.StockMax)
			{
				ToolTip.DisplayToolTip("Not enough room in stock.");
				return;
			}

			if (AttributeManager.CorpsesInQueue < 1)
			{
				ToolTip.DisplayToolTip("Ain't no coffins here amigo.");
				return;
			}

			AttributeManager.CorpsesInStock++;

			decreaseCoffinsInQueue();

			return;
		}

		// [6,0] is the gate block which for some reason is not protected even though I set it to that... ain't no time, hotfix it is
		if (TileDataMatrix[i, j].getIsProtected() || (i == 6 && j == 0)) 
		{
			ToolTip.DisplayToolTip("You can't do that.");
			return;
		}

		switch (AttributeManager.SelectedTool)
		{
			case AtributesManagerScript.ToolID.dig:
				dig(i, j);
				break;
			case AtributesManagerScript.ToolID.corpse:
				bury(i, j);
				break;
			case AtributesManagerScript.ToolID.build:
				build(i, j);
				break;
			case AtributesManagerScript.ToolID.clear:
				clear(i, j);
				break;
		}
	}

	public void dig(int i, int j)
	{
		TileData CurrentTileData = TileDataMatrix[i, j];

		if (CurrentTileData.getTileType() == TileType.ChestTile) //click on a chest
		{
			ToolTip.DisplayToolTip("Use hand for that");
			return;
		}

		if (CurrentTileData.getZombieBreakoutStage() > 0)
		{
			if (AttributeManager.SelectedTool != AtributesManagerScript.ToolID.dig)
			{
				ToolTip.DisplayToolTip("You have to use a shovel!");
				return;
			}
			unzombify(CurrentTileData);
			return;
		}

		if ((CurrentTileData.getClickCount() > (int)TileType.DigCoffinInside) || CurrentTileData.getIsDecoration())
		{
			ToolTip.DisplayToolTip("You need to clear this place if you want to dig again.");
			return;
		}

		if (CurrentTileData.getTileType() == TileType.DigFive && !CurrentTileData.getHasBurriedInside())
		{
			ToolTip.DisplayToolTip("Cannot dig deeper.");
			return;
		}

		else if (CurrentTileData.getTileType() == TileType.DigFour && !CurrentTileData.getHasBurriedInside())
		{
			bool willThereBeAChest = calculateChestChance();
			if (willThereBeAChest)
			{
				CurrentTileData.setClickCount((int)TileType.ChestTile);
			}
			else
			{
				CurrentTileData.setReadyForBurial(true); //later player will be able to bury someone before DigFive, at the cost of increased chance of zombie
				CurrentTileData.increaseClickCount();
			}
			
		}

		else if (CurrentTileData.getTileType() != TileType.DigOne && CurrentTileData.getHasBurriedInside())
		{
			CurrentTileData.decreaseClickCount();
		}

		else if (CurrentTileData.getTileType() == TileType.DigOne && CurrentTileData.getHasBurriedInside())
		{
			CurrentTileData.setClickCount((int)TileType.GraveWithoutCross);
			CurrentTileData.setMaxZombieBreakoutStage(1);
		}

		else
		{
			CurrentTileData.increaseClickCount();
		}

		AttributeManager.EarnXP(); //increase xp

		CurrentTileData.setTileType((TileType)CurrentTileData.getClickCount());
		CurrentTileData.IsCollider = true;
		TileDictionary.updateTileSprite(CurrentTileData.getTileType(), new Vector3Int(CurrentTileData.getPosI(), CurrentTileData.getPosJ(), 0), MainTileMap);
		TileDictionary.updateColliderTileSprite(CurrentTileData.getTileType(), new Vector3Int(CurrentTileData.getPosI(), CurrentTileData.getPosJ(), 0), ColliderTileMap);
	}

	public void bury(int i, int j) //HAND
	{
		TileData CurrentTileData = TileDataMatrix[i, j];

		if (CurrentTileData.getTileType() == TileType.ChestTile) //click on a chest
		{
            var popUpScript = Instantiate(TreasurePopUpPrefab, ToolTip.transform).GetComponent<ChestPopUpScript>();
            popUpScript.AchievementManager = AchievementManager;
            popUpScript.AtributesManager = AttributeManager;

            CurrentTileData.setClickCount((int)TileType.DigFive);
			CurrentTileData.setTileType(TileType.DigFive);
			CurrentTileData.setReadyForBurial(true);
			TileDictionary.updateTileSprite(CurrentTileData.getTileType(), new Vector3Int(CurrentTileData.getPosI(), CurrentTileData.getPosJ(), 0), MainTileMap);
			return;
		}

		if (CurrentTileData.getZombieBreakoutStage() > 0)
		{
			ToolTip.DisplayToolTip("You have to use a shovel!");
			return;
		}

		if (AttributeManager.CorpsesInStock < 1)
		{
			ToolTip.DisplayToolTip("Not enough corpses in stock!");
			return;
		}

		if (CurrentTileData.getReadyForBurial() && !CurrentTileData.getHasBurriedInside())
		{
			CurrentTileData.setHasBurriedInside(true);
			CurrentTileData.setReadyForBurial(false);
			CurrentTileData.setTileType(TileType.DigCoffinInside);
			CurrentTileData.setClickCount((int)TileType.DigCoffinInside);
			CurrentTileData.IsCollider = true;
			TileDictionary.updateColliderTileSprite(CurrentTileData.getTileType(), new Vector3Int(CurrentTileData.getPosI(), CurrentTileData.getPosJ(), 0), ColliderTileMap);
			TileDictionary.updateTileSprite(TileType.DigCoffinInside, new Vector3Int(CurrentTileData.getPosI(), CurrentTileData.getPosJ(), 0), MainTileMap);
			AttributeManager.increaseBurried();
			AttributeManager.CorpsesInStock--;
			AttributeManager.EarnXP(); //increase xp
		}
		else if (!CurrentTileData.getReadyForBurial() && !CurrentTileData.getHasBurriedInside())
		{
			ToolTip.DisplayToolTip("You need to dig " + (5 - CurrentTileData.getClickCount()) + " more times.");
		}
		else
		{
			ToolTip.DisplayToolTip("Oops! Looks like someone is already burried there!");
		}
	}

	public void build(int i, int j) //TODO
	{
		if (AttributeManager.GetItemSelected() == null)
		{
			ToolTip.DisplayToolTip("You need to select something from the menu on the left.");
			return;
		}

		TileData CurrentTileData = TileDataMatrix[i, j];

		if (CurrentTileData.getZombieBreakoutStage() > 0)
		{
			ToolTip.DisplayToolTip("You have to use a shovel!");
			return;
		}

		if (AttributeManager.GetItemSelectedIsGrave())
		{
			if (CurrentTileData.getTileType() != TileType.GraveWithoutCross)
			{
				ToolTip.DisplayToolTip("You can put tombstone only at grave.");
				return;
			}
		}
		else
		{
			if (CurrentTileData.getTileType() != TileType.Grass)
			{
				ToolTip.DisplayToolTip("You can put decorations only at grass.");
				return;
			}

		}

		bool flag = AttributeManager.decreaseMoney(AttributeManager.getItemSelectedCost());
		if (!flag)
		{
			ToolTip.DisplayToolTip("Not enough money.");
			return;
		}

		string selectedTileString = AttributeManager.GetItemSelected().ToString();
		selectedTileString = selectedTileString.Substring(0, selectedTileString.IndexOf(" "));

		TileType SelectedTileType = (TileType)System.Enum.Parse(typeof(TileType), selectedTileString, true);

		CurrentTileData.setClickCount((int)SelectedTileType);
		CurrentTileData.setTileType(SelectedTileType);
		TileDictionary.updateTileSprite(SelectedTileType, new Vector3Int(CurrentTileData.getPosI(), CurrentTileData.getPosJ(), 0), MainTileMap);

		//zombie chance based on tombstone
		if (AttributeManager.GetItemSelectedIsGrave())
		{
			switch (SelectedTileType)
			{
				//TODO: quick chance just to show zombie spawn
				/*case TileType.GravePlebThree:
					CurrentTileData.setZombieChance(20);
					CurrentTileData.setMaxZombieBreakoutStage(5);
					break;*/
				/*case TileType.GraveCasioThree:
				case TileType.GraveRolexThree:
					CurrentTileData.setZombieChance(30);
					CurrentTileData.setMaxZombieBreakoutStage(3);
					break;*/

				case TileType.GravePlebOne:
				case TileType.GravePlebTwo:
				case TileType.GravePlebThree:
					CurrentTileData.setZombieChance(plebZombieChance);
					CurrentTileData.setMaxZombieBreakoutStage(5);
					break;

				case TileType.GraveCasioOne:
				case TileType.GraveCasioTwo:
				case TileType.GraveCasioThree:
					CurrentTileData.setZombieChance(casioZombieChance);
					CurrentTileData.setMaxZombieBreakoutStage(3);
					break;

				case TileType.GraveRolexOne:
				case TileType.GraveRolexTwo:
				case TileType.GraveRolexThree:
					CurrentTileData.setZombieChance(rolexZombieChance);
					CurrentTileData.setMaxZombieBreakoutStage(3);
					break;
			}

			CurrentTileData.setIsGrave(true);
		}
		else
		{
			CurrentTileData.setIsDecoration(true);
		}

		/*if (!CurrentTileData.getIsGrave())
		{*/
			CurrentTileData.IsCollider = true;
			TileDictionary.updateColliderTileSprite(CurrentTileData.getTileType(), new Vector3Int(CurrentTileData.getPosI(), CurrentTileData.getPosJ(), 0), ColliderTileMap);
		/*} else
		{
			CurrentTileData.IsCollider = false;
			setColliderTileToNull(CurrentTileData);
		}*/
		
		
		AttributeManager.EarnXP(); //increase xp
	}

	public void clear(int i, int j)
	{
		TileData CurrentTileData = TileDataMatrix[i, j];

		if (CurrentTileData.getZombieBreakoutStage() > 0)
		{
			ToolTip.DisplayToolTip("You have to use a shovel!");
			return;
		}

		if (CurrentTileData.getClickCount() >= 90)
		{
			ToolTip.DisplayToolTip("Cannot destroy this!");
		}
		else
		{
			CurrentTileData.setClickCount(0);
			CurrentTileData.setTileType(TileType.Grass);
			CurrentTileData.setReadyForBurial(false);
			CurrentTileData.setHasBurriedInside(false);
			MainTileMap.SetTile(new Vector3Int(CurrentTileData.getPosI(), CurrentTileData.getPosJ(), 0), TileDictionary.GetRandomGrassTile());

			if (CurrentTileData.getIsGrave())
			{
				AttributeManager.decreaseBurried();
			}
			CurrentTileData.setIsGrave(false);
			CurrentTileData.setIsDecoration(false);
			CurrentTileData.IsCollider = false;
			setColliderTileToNull(CurrentTileData);
			CurrentTileData.Health = 3;
		}
	}

	public void setColliderTileToNull(TileData CurrentTileData)
	{
		ColliderTileMap.SetTile(new Vector3Int(CurrentTileData.getPosI(), CurrentTileData.getPosJ(), 0), null); //erases tile
	}

	public void addCoffinsToQueue()
	{
		AttributeManager.CorpsesInQueue++;
		setCoffinTileType();
	}

	public void decreaseCoffinsInQueue()
	{
		AttributeManager.CorpsesInQueue--;
		setCoffinTileType();
	}

	public void setCoffinTileType()
	{
		//road tiles for coffins
		TileData FirstCoffinQueueTileData = TileDataMatrix[3, 1];
		TileData SecondCoffinQueueTileData = TileDataMatrix[4, 1];
		TileData ThirdCoffinQueueTileData = TileDataMatrix[5, 1];

		switch (AttributeManager.getCorpsesInQueue())
		{
			case 0:
				FirstCoffinQueueTileData.setTileType(TileType.Road);
				SecondCoffinQueueTileData.setTileType(TileType.Road);
				ThirdCoffinQueueTileData.setTileType(TileType.Road);
				break;
			case 1:
				FirstCoffinQueueTileData.setTileType(TileType.CoffinInQueue1);
				SecondCoffinQueueTileData.setTileType(TileType.Road);
				ThirdCoffinQueueTileData.setTileType(TileType.Road);
				break;
			case 2:
				FirstCoffinQueueTileData.setTileType(TileType.CoffinInQueue2);
				SecondCoffinQueueTileData.setTileType(TileType.Road);
				ThirdCoffinQueueTileData.setTileType(TileType.Road);
				break;
			case 3:
				FirstCoffinQueueTileData.setTileType(TileType.CoffinInQueue2);
				SecondCoffinQueueTileData.setTileType(TileType.CoffinInQueue1);
				ThirdCoffinQueueTileData.setTileType(TileType.Road);
				break;
			case 4:
				FirstCoffinQueueTileData.setTileType(TileType.CoffinInQueue2);
				SecondCoffinQueueTileData.setTileType(TileType.CoffinInQueue2);
				ThirdCoffinQueueTileData.setTileType(TileType.Road);
				break;
			case 5:
				FirstCoffinQueueTileData.setTileType(TileType.CoffinInQueue2);
				SecondCoffinQueueTileData.setTileType(TileType.CoffinInQueue2);
				ThirdCoffinQueueTileData.setTileType(TileType.CoffinInQueue1);
				break;
		}

		TileDictionary.updateTileSprite(FirstCoffinQueueTileData.getTileType(), new Vector3Int(FirstCoffinQueueTileData.getPosI(), FirstCoffinQueueTileData.getPosJ(), 0), MainTileMap);
		TileDictionary.updateTileSprite(SecondCoffinQueueTileData.getTileType(), new Vector3Int(SecondCoffinQueueTileData.getPosI(), SecondCoffinQueueTileData.getPosJ(), 0), MainTileMap);
		TileDictionary.updateTileSprite(ThirdCoffinQueueTileData.getTileType(), new Vector3Int(ThirdCoffinQueueTileData.getPosI(), ThirdCoffinQueueTileData.getPosJ(), 0), MainTileMap);

		TileDictionary.updateColliderTileSprite(FirstCoffinQueueTileData.getTileType(), new Vector3Int(FirstCoffinQueueTileData.getPosI(), FirstCoffinQueueTileData.getPosJ(), 0), ColliderTileMap);
		TileDictionary.updateColliderTileSprite(SecondCoffinQueueTileData.getTileType(), new Vector3Int(SecondCoffinQueueTileData.getPosI(), SecondCoffinQueueTileData.getPosJ(), 0), ColliderTileMap);
		TileDictionary.updateColliderTileSprite(ThirdCoffinQueueTileData.getTileType(), new Vector3Int(ThirdCoffinQueueTileData.getPosI(), ThirdCoffinQueueTileData.getPosJ(), 0), ColliderTileMap);

	}

	// uhh, yep. later will be renamed to "updateTileSprite" when I replace the calls for that function with this one
	// so that we don't have to have TileDataManager AND TileDictionary everywhere (just manager)
	public void callTileDictionaryFunctionToUpdateTileSprite(TileData currentTileData)
	{
		TileDictionary.updateTileSprite(currentTileData.getTileType(), new Vector3Int(currentTileData.getPosI(), currentTileData.getPosJ(), 0), MainTileMap);
	}
}
