using System;
using System.Collections.Generic;
using UnityEngine;


[Serializable]
public class GridInfo
{
	public int Width;
	public int Height;
	public List<Vector2Int> EmptyCells;

	public GridInfo(int width, int height, List<Vector2Int> emptyCells)
	{
		Width = width;
		Height = height;
		EmptyCells = emptyCells;
	}

	public Vector2 GetCenterOffset()
	{
		return new Vector2((float) Width /2+1/2, (float) Height /2+1/2);
	}

	public Vector2 GetReverseCenterOffset()
	{
		return new Vector2( (float) Height / 2 + 1 / 2, (float) Width / 2 + 1 / 2);
	}
}