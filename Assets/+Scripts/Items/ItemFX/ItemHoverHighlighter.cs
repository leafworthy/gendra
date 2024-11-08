using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ItemHoverHighlighter : MonoBehaviour
{
	private List<SlotHighlight> previouslyHoveredSlots = new();
	private List<SlotHighlight> _newHoveredSlots = new List<SlotHighlight>();
	private Item currentHoveredItem;

	private void Start()
	{
		ItemSlotInventory.OnItemDraggedIn += ItemDraggedIn;
		ItemSlotInventory.OnItemDraggedOut += ItemDraggedOut;
	}

	private void ItemDraggedIn(Item newItem, ItemSlotInventory itemSlotInventory)
	{
		if (newItem == null) return;
		HighlightSlots(newItem);
	}
	private void ItemDraggedOut(Item newItem, ItemSlotInventory itemSlotInventory)
	{
		UnhighlightSlots(newItem);
	}

	private static void UnhighlightSlots(Item newItem)
	{ 
		var spotsToHighlight = newItem.GetWorldPositionsOfFullSpaces();
		foreach (var spot in spotsToHighlight)
		{
			var slot = MouseManager.GetObjectsAtPosition<SlotHighlight>(spot).FirstOrDefault();
			if (slot == null) continue;
			slot.SetHovered(null);
		}
	}

	

	private static void HighlightSlots(Item newItem)
	{
		var spotsToHighlight = newItem.GetWorldPositionsOfFullSpaces();
		foreach (var spot in spotsToHighlight)
		{
			var slot = MouseManager.GetObjectsAtPosition<SlotHighlight>(spot).FirstOrDefault();
			if (slot == null) continue;
			slot.SetHovered(newItem);
		}
	}

	private void Update()
	{
		var draggingItem = ItemGrabber.DraggingItem;

		UpdateCurrentlyHoveredSlots(draggingItem);


		foreach (var highlight in previouslyHoveredSlots)
		{

			if (_newHoveredSlots != null)
			{
				if (_newHoveredSlots.Contains(highlight))
				{
					continue;
				}
			} 
			highlight.SetHovered(null);
		}

		if(_newHoveredSlots == null) return;
		previouslyHoveredSlots = _newHoveredSlots.ToList();
	}

	private void UpdateCurrentlyHoveredSlots(Item draggingItem)
	{
		_newHoveredSlots.Clear();
		if (draggingItem == null)
		{
			return;
		}
		var pointsToTest = draggingItem.GetWorldPositionsOfFullSpaces();
		foreach (var point in pointsToTest)
		{
			var slot = MouseManager.GetObjectsAtPosition<SlotHighlight>(point).FirstOrDefault();
			if (slot == null)
			{
				DebugDraw.DrawX(point, 0.5f);
				continue;
			}

			_newHoveredSlots.Add(slot);
			if (previouslyHoveredSlots.Contains(slot)) continue;
			DebugDraw.DrawX( point,  0.5f, Color.green);
			slot.SetHovered(draggingItem);
		}

	}

	

}