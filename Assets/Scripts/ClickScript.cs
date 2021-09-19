using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.EventSystems;

public class ClickScript : MonoBehaviour
{
	public TileDictionaryScript TileDictionary;
	public Tilemap MainTileMap;
	public TileDataManager MainTileDataManager;

	private Vector3 _dragingPosition;
	private bool _draging;

	//private bool isZombieTrigger = false;

	/*void OnTriggerEnter2D(Collider2D col)
	{
		isZombieTrigger = true;
	}

	void OnTriggerExit2D(Collider2D col)
	{
		isZombieTrigger = false;
	}*/

	void OnMouseDown()
	{
        //Debug.Log("CLICK");
		_draging = false;
		_dragingPosition = Camera.main.ScreenToViewportPoint(Input.mousePosition);
	}

	void OnMouseDrag()
	{
	    //Debug.Log("CLICKDrag");
        Vector3 dragingPosition2 = Camera.main.ScreenToViewportPoint(Input.mousePosition);
		if (Vector2.Distance(_dragingPosition, dragingPosition2) > 0.02f)
		{
			_draging = true;
		}
	}

	void OnMouseUp()
	{
	    //Debug.Log("CLICKup");

	    
        // Uncomment before build
        
	    if (EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId))
	    {
	        return;
	    }
        
		
	    //if (EventSystem.current.IsPointerOverGameObject()/* || isZombieTrigger*/)
	    /*
		{
			return;
		}
		
	    */
	    
		if (_draging)
		{
			return;
		}

		// get mouse click's position in 2d plane
		Vector3 pz = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		pz.z = 0;

		// convert mouse click's position to Grid position
		GridLayout gridLayout = transform.parent.GetComponentInParent<GridLayout>();
		Vector3Int cellPosition = gridLayout.WorldToCell(pz);

		//MainTileMap = this.GetComponent<Tilemap>();

		// set selectedUnit to clicked location on grid
		Debug.Log("cell position: " + cellPosition);

		MainTileDataManager.clicked(cellPosition.x, -cellPosition.y);
		//Debug.Log("tiletype: " + MainTileDataManager.TileDataMatrix[cellPosition.x + offset, cellPosition.y + offset].getTileType());
		//Debug.Log("tileClickCount: " + MainTileDataManager.TileDataMatrix[cellPosition.x + offset, cellPosition.y + offset].getClickCount());

		//int clicks = MainTileDataManager.TileDataMatrix[cellPosition.x + offset, cellPosition.y + offset].getClickCount();
		//MainTileDataManager.TileDataMatrix[cellPosition.x + offset, cellPosition.y + offset].setTileType((TileType)clicks);

		//TileDictionary.updateTileSprite((TileType)clicks, cellPosition, MainTileMap);

		//Debug.Log("new tiletype: " + MainTileDataManager.TileDataMatrix[cellPosition.x + offset, cellPosition.y + offset].getTileType());
	}
}
