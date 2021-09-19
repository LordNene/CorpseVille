using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum Directions
{
	Left, Right, Up, Down
}

public class ZombieScript : MonoBehaviour
{

    public Sprite SpriteLeft;
    public Sprite SpriteRight;
    public Sprite SpriteUp;
    public Sprite SpriteDown;

    public float Speed = 1;
    public Image HealthBar;
    public float StartHealth;

    private Vector2 direction;
	private Directions DirectionName;

    private float health;
    private AtributesManagerScript atributesManagerScript;
    private AchievementManager achievementManager;
	private TileDataManager MainTileDataManager;

	private GridLayout gridLayout;
    private WaitForSeconds WaitForSeconds;
    private WaitForSeconds WaitFor3Seconds;
    private bool isThinking = false;
	private bool isDamaging = false;

	private bool waitToHit = false;
	
	public Rigidbody2D RB;
	// Start is called before the first frame update
	void Start()
    {
        health = StartHealth;
        atributesManagerScript = FindObjectOfType<AtributesManagerScript>();
        achievementManager = FindObjectOfType<AchievementManager>();
		MainTileDataManager = FindObjectOfType<TileDataManager>();


		transform.localScale = new Vector3(0.5f, 0.5f, 0);
        ChooseDirection();

		gridLayout = FindObjectOfType<GridLayout>();

        WaitForSeconds = new WaitForSeconds(1);
        WaitFor3Seconds = new WaitForSeconds(3);
        achievementManager.OnZombieSpawn();
    }

    private void OnMouseDown()
    {
        health--;
        HealthBar.fillAmount = health / StartHealth;
        if (health <= 0)
        {
			if (atributesManagerScript.CorpsesInStock < atributesManagerScript.StockMax)
			{
				atributesManagerScript.CorpsesInStock++;
			}
            achievementManager.OnZombieKill();
            Destroy(gameObject);
        }
       
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (!isThinking && collision.gameObject.tag == "Zombie")
        {
            isThinking = true;
            StartCoroutine(ThinkingCoroutine());
        }
		else if (!isThinking /*collision.gameObject.tag != "Tilemap"*/)
		{
			//isDamaging = true;
			if (!isDamaging)
			{
				StartCoroutine(DamagingCoroutine());
			}

			
			//DamageObject();
		}
    }

	//zombie damages object in front of him
	private void DamageObject()
	{
		var x = (float)transform.position.x;
		var y = (float)transform.position.y-0.5f;

		Vector3Int gridVector = gridLayout.WorldToCell(new Vector3(x, y, 0));
		int coordX = System.Math.Abs(gridVector.x);
		int coordY = System.Math.Abs(gridVector.y);

		switch (DirectionName)
		{
			case Directions.Left:
				coordX--;
				break;
			case Directions.Right:
				coordX++;
				break;
			case Directions.Up:
				coordY--;
				break;
			case Directions.Down:
				coordY++;
				break;
		}

		TileData currentTileData = MainTileDataManager.TileDataMatrix[coordX, coordY];

		//Debug.Log("coords: " + coordX + " , " + coordY);
		//Debug.Log(currentTileData.Health);

		currentTileData.Health--;
		int h = currentTileData.Health;

		if (currentTileData.Health == 0)
		{
			//MainTileDataManager.callTileDictionaryFunctionToUpdateTileSprite(currentTileData, TileType.Grass);
			MainTileDataManager.clear(currentTileData.getPosI(), -currentTileData.getPosJ());
			//MainTileDataManager.setColliderTileToNull(currentTileData);
			isDamaging = false;
		}
	}

	public IEnumerator ThinkingCoroutine()
    {
        yield return WaitForSeconds;
        ChooseDirection();
        isThinking = false;
    }

	public IEnumerator DamagingCoroutine()
	{
		isDamaging = true;
		yield return WaitFor3Seconds;
		DamageObject();
		isDamaging = false;
	}

	// Update is called once per frame
	void Update()
    {
        Move();
        CheckDistance();
    }

    private void CheckDistance()
    {
        var x = (int)transform.position.x;
        var y = (int)transform.position.y;

		Vector3Int gridVector = gridLayout.WorldToCell(new Vector3Int(x, y, 0));
		int coordX = System.Math.Abs(gridVector.x);
		int coordY = System.Math.Abs(gridVector.y);

		if (gridVector.x < 0 || gridVector.y > 0 || gridVector.x > 29 || gridVector.y < -29 || MainTileDataManager.TileDataMatrix[coordX, coordY].IsChunkFree)
		{
            achievementManager.OnZombieEscape();
            Destroy(gameObject);
			//Debug.Log("coords xy " + gridVector.x + " , " + gridVector.y + " , " + MainTileDataManager.TileDataMatrix[coordX, coordY].IsChunkFree);
		}
	}

    private void ChooseDirection()
    {
        var rand = UnityEngine.Random.Range(0, 4);
        switch (rand)
        {
            case 0:
                direction = Vector2.left;
                GetComponent<SpriteRenderer>().sprite = SpriteLeft;
                direction = Quaternion.Euler(0, 0, 35.387f) * direction;
				DirectionName = Directions.Left;
                break;
            case 1:
                direction = Vector2.right;
                GetComponent<SpriteRenderer>().sprite = SpriteRight;
                direction = Quaternion.Euler(0, 0, 35.387f) * direction;
				DirectionName = Directions.Right;
				break;
            case 2:
                direction = Vector2.left;
                GetComponent<SpriteRenderer>().sprite = SpriteUp;
                direction = Quaternion.Euler(0, 0, -35.387f) * direction;
				DirectionName = Directions.Up;
				break;
            case 3:
                direction = Vector2.right;
                GetComponent<SpriteRenderer>().sprite = SpriteDown;
                direction = Quaternion.Euler(0, 0, -35.387f) * direction;
				DirectionName = Directions.Down;
				break;
            default:
                break;
        }


    }

    private void Move()
    {
        if (!isThinking && !isDamaging)
        {
            transform.Translate(direction * Speed * Time.deltaTime);
        }
        //RB.MovePosition(transform.position * direction * Speed * Time.deltaTime);
    }
}
