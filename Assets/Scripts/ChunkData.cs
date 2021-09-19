using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChunkData /*: MonoBehaviour*/
{
	//private int startIndexI, endIndexI;
	//private int startIndexJ, endIndexJ;
	private TileData neighbourLeft = null;
	private TileData neighbourRight = null;

	public int StartIndexI { get; set; }
	public int EndIndexI { get; set; }
	public int StartIndexJ { get; set; }
	public int EndIndexJ { get; set; }


	public ChunkData(int startI, int endI, int startJ, int endJ, TileData left, TileData right)
	{
		StartIndexI = startI;
		StartIndexJ = startJ;
		EndIndexI = endI;
		EndIndexJ = endJ;
		neighbourLeft = left;
		neighbourRight = right;
	}
}
