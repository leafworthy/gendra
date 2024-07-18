using System.Collections.Generic;
using System.Linq;
using NaughtyAttributes;
using UnityEngine;

public class ItemSpaceGrid : Grid
{
	private void OnEnable()
	{
		var item = GetComponentInParent<Item>();
		item.OnSetup += Setup;

	}

	public static int howMany;
	private void OnDisable()
	{
		var item = GetComponentInParent<Item>();
		if(item == null) return;
		item.OnSetup -= Setup;
	
	}

	public List<ItemGridSpace> GetItemGridSpaces()
	{
		
		var spaces = new List<ItemGridSpace>();
		var randomColor = Random.ColorHSV();
		
		howMany++;
		Debug.Log(howMany);
		foreach (var space in GetGridObjects())
		{
			
			if (space is ItemGridSpace gridSpace)
			{
				
				Utils.DrawX( gridSpace.transform.position,  0.5f, randomColor);
				spaces.Add(gridSpace);
			}
		}
		Debug.Break();

		return spaces;
	}
	public void Setup (ItemData info)
	{
		MakeGrid(info.spaceGridInfo, Prefabs.I.ItemSpaceGridPrefab);
		foreach (var griddable in GetItemGridSpaces())
		{
			GetItemGridSpaces().Add(griddable);
		}
	}

	public List<Vector2> GetWorldPositionsOfFullSpaces()
	{
		var gridPositions = new List<Vector2>();
		foreach (var space in GetItemGridSpaces())
		{
			if (space.IsEmptySpace) continue;

			gridPositions.Add(space.transform.position);
		}

		return gridPositions;
	}

	public override void MakeGrid(GridInfo info, GameObject prefab)
	{
		base.MakeGrid(info, prefab);
		SetEmptySpaces();
	}

	private void SetEmptySpaces()
	{
		var emptySpaces = GetGridInfo().EmptyCells;
		foreach (var space in GetItemGridSpaces().Where(space => emptySpaces.Contains(space.GetGridPos())))
		{
			space.IsEmptySpace = true;
		}
	}

	public ItemGridSpace GetSpaceByGridPosition(int x, int y)
	{
		return GetItemGridSpaces().FirstOrDefault(space => space.GetGridPos() == new Vector2Int(x, y));
	}

	public List<Vector2Int> GetEmptyGridSpaces()
	{
		var newEmptyCells = new List<Vector2Int>();
		foreach (var spaces in GetItemGridSpaces())
		{
			if (spaces.IsEmptySpace)
				newEmptyCells.Add(spaces.GetGridPos());
		}

		return newEmptyCells;
	}



}