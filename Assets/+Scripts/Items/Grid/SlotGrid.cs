using System.Collections.Generic;
using System.Linq;
using NaughtyAttributes;
using UnityEngine;

[ExecuteInEditMode]
public class SlotGrid : Grid
{
	private List<Slot> slots => GetComponentsInChildren<Slot>().ToList();
	private bool made;
	public List<Slot> GetSlots()
	{
		if(!made) MakeGrid();
		return slots;
	}

	[Button]
	public void ButtonMakeGrid()
	{
		MakeGrid();
	}
	public override void MakeGrid(GridInfo info, GameObject prefab)
	{
		base.MakeGrid(info, prefab);
		SetEmptySpaces();
	}

	private void SetEmptySpaces()
	{
		var emptySpaces = GetGridInfo().EmptyCells;
		foreach (var slot in slots.Where(space => emptySpaces.Contains(space.GetGridPos())))
		{
			slot.SetDisabled();
		}
	}
}