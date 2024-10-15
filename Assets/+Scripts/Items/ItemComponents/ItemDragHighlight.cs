using UnityEngine;

public class ItemDragHighlight : MonoBehaviour
{
	private Item _item => GetComponent<Item>();
	

	private void Update()
	{
		if (_item.IsDragging) DragDropIndicatorHighlight();
		else if (_item.IsDragging) DragDropIndicatorUnHighlight();
	}

	private void DragDropIndicatorHighlight()
	{
		var pointsToTest = _item.Grid.GetWorldPositionsOfFullSpaces();
		foreach (var point in pointsToTest)
		{
			var hits = MouseManager.GetObjectsAtPosition<Slot>(point);
			if (hits.Count <= 0)
			{
				ShowSpacesThatCantDrop();
				return;
			}

			foreach (var hit in hits)
			{
				if (hit.IsUnoccupied && !hit.IsDisabled) continue;
				ShowSpacesThatCantDrop();
				return;
			}
		}

		DragDropIndicatorUnHighlight();
	}

	private void ShowSpacesThatCantDrop()
	{
		var spaces = _item.Grid.GetItemGridSpaces();
		foreach (var space in spaces)
		{
			if (space is {IsEmptySpace: true}) continue;

			var slots = MouseManager.GetObjectsAtPosition<Slot>(space.transform.position);
			if (slots.Count <= 0)
			{
				space.ShowSpaceCantDrop();
				continue;
			}

			if (!slots[0].IsUnoccupied || slots[0].IsDisabled)
			{
				space.ShowSpaceCantDrop();
				continue;
			}

			space.UnHighlightActualSpace();
		}
	}

	private void DragDropIndicatorUnHighlight()
	{
		foreach (var space in _item.Grid.GetItemGridSpaces())
		{
			space.UnHighlightActualSpace();
		}
	}


}