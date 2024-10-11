using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[ExecuteInEditMode]
public class SlotGrid : Grid
{
	private List<Slot> slots => GetComponentsInChildren<Slot>().ToList();

	public List<Slot> GetSlots() => slots;

	public override void MakeGrid(GridInfo info, GameObject prefab)
	{
		base.MakeGrid(info, prefab);
		SetEmptySpaces();
		Debug.Log("made slot grid" + info.Height + " " + info.Width );
	}

	private void SetEmptySpaces()
	{
		var emptySpaces = GetGridInfo().EmptyCells;
		foreach (var slot in slots.Where(space => emptySpaces.Contains(space.GetGridPos())))
		{
			slot.SetDisabled();
		}
	}

	public Slot GetSlotFromGridPosition(Vector2Int newPosition)
	{   
		var slot = slots.FirstOrDefault(s => s.GetGridPos() == newPosition);
		if (slot == null) Debug.Log("slot null");
		return slot;
	}
}