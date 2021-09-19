using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExpandPopUpScript : MonoBehaviour
{
    public Text TextPlaceholder;
    public Button ConfirmButton;
    public Button CloseButton;

    private TileDataManager tileDataManager;
    private int i;
    private int j;
    AtributesManagerScript AttributeManager;
    UIScript ToolTip;
    ChunkManagerScript chunkManager;

    private bool initiated;
    private bool answered;
    private bool expand;

    void Start()
    {
        ConfirmButton.onClick.AddListener(() =>
        {
            answered = true;
            expand = true;
            Destroy(gameObject);
        });

        CloseButton.onClick.AddListener(() =>
        {
            answered = true;
            expand = false;
            Destroy(gameObject);
        });

        StartCoroutine("Expand");
    }

    public void Init(TileDataManager tileDataManager, int i, int j, AtributesManagerScript attributeManager, UIScript toolTip,
        ChunkManagerScript chunkManager)
    {
        this.tileDataManager = tileDataManager;
        this.i = i;
        this.j = j;
        AttributeManager = attributeManager;
        ToolTip = toolTip;
        this.chunkManager = chunkManager;
        initiated = true;
    }

    IEnumerator Expand()
    {
        if (!initiated) yield break; //ensure we have what we need

        var CurrentTile = tileDataManager.TileDataMatrix[i, j];
        int offset = 4; //because thats where the plus symbols are offseted to
        TileData left, right;
        ChunkData currentChunk = null;

        bool placeRight = tileDataManager.TileDataMatrix[i, j + 10].getTileType() != TileType.ChunkPlusSymbol
                             && tileDataManager.TileDataMatrix[i - offset, j + 10 + offset].getTileType() != TileType.ChunkPlusSymbol;
        bool placeLeft = tileDataManager.TileDataMatrix[i + 10, j].getTileType() != TileType.ChunkPlusSymbol
                            && tileDataManager.TileDataMatrix[i + 10 + offset, j - offset].getTileType() != TileType.ChunkPlusSymbol;

        int price = 0;

        if (CurrentTile.IsChunkLeftNeighbour)
        {
            if (i < j)
            {
                price = (j - offset) * 100;
            }
            else
            {
                price = i * 100;
            }
            TextPlaceholder.text = $"Expand your cemetery for {price} Gold?";
            yield return new WaitUntil(() => answered);

            if (!expand) yield break;

            bool flag = AttributeManager.decreaseMoney(price);
            if (!flag)
            {
                ToolTip.DisplayToolTip("Not enough money.");
                yield break;
            }

            left = tileDataManager.TileDataMatrix[i + 10, j];
            right = tileDataManager.TileDataMatrix[i + offset, j + 10 - offset];
            currentChunk = new ChunkData(i, i + 10, j - offset, j - offset + 10, left, right);

            chunkManager.fillChunkWithGrass(currentChunk);
            CurrentTile.setTileType(TileType.Grass);

            chunkManager.createChunkPlusSymbolTiles(i, i + 10, j - offset, j - offset + 10, left, right, placeLeft, placeRight);
        }
        else
        {
            if (j < i)
            {
                price = (i - offset) * 100;
            }
            else
            {
                price = j * 100;
            }

            TextPlaceholder.text = $"Expand your cemetery for {price} Gold?";
            yield return new WaitUntil(() => answered);

            if (!expand) yield break;

            bool flag = AttributeManager.decreaseMoney(price);
            if (!flag)
            {
                ToolTip.DisplayToolTip("Not enough money.");
                yield break;
            }

            left = tileDataManager.TileDataMatrix[i + 10 - offset, j + offset];
            right = tileDataManager.TileDataMatrix[i, j + 10];
            currentChunk = new ChunkData(i - offset, i - offset + 10, j, j + 10, left, right);

            chunkManager.fillChunkWithGrass(currentChunk);
            CurrentTile.setTileType(TileType.Grass);

            chunkManager.createChunkPlusSymbolTiles(i - offset, i - offset + 10, j, j + 10, left, right, placeLeft, placeRight);
        }
    }
}
