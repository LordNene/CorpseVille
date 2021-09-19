using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.EventSystems;

public class ObjectSpawner : MonoBehaviour
{
    //toto som prehodil
    private const float offset = 0.5f;
    private const float gridYOffset = 1f;
    private TileDataManager tileDataManager;
	private GridLayout gridLayout;

	// not used in this version public GameObject coin;
    public GameObject zombie;

    void Start()
    {
        tileDataManager = FindObjectOfType<TileDataManager>();
		gridLayout = FindObjectOfType<GridLayout>();
        //InvokeRepeating("SpawnCoins", 60, 60);

    }

    public GameObject SpawnObject(GameObject obj, int x, int y)
    {
		//x -= offset;
		//y -= offset;
        obj.GetComponent<SpriteRenderer>().sortingOrder = 2;
		Vector3 gridVector = gridLayout.CellToWorld(new Vector3Int(x, y, 0));
		return Instantiate(obj, new Vector3(gridVector.x, gridVector.y + gridYOffset + offset, 0), Quaternion.identity);
    }

    /*
    private void SpawnCoins()
    {
        foreach (var tile in tileDataManager.TileDataMatrix)
        {
            if (tile.getIsGrave())
            {
                SpawnObject(coin, tile.getPosI(), tile.getPosJ());
            }
        }
    }*/

    public void SpawnZombie(int x, int y)
    {
        SpawnObject(zombie, x, y);
    }

}
