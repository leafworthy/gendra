using System.Collections.Generic;
using UnityEngine;

public class SlotHighlighter : MonoBehaviour
{
	private List<SlotHighlight> currentlyHighlightedSlots = new();
	private void Update()
	{
		UnHighlightTheOld();
		HighlightTheNew();
	}

	private void HighlightTheNew()
	{
		var highlightableSlotsAtMousePosition = MouseManager.GetObjectsAtMousePosition<SlotHighlight>();
		var toAdd = new List<SlotHighlight>();
		foreach (var highlightable in highlightableSlotsAtMousePosition)
		{
			if (currentlyHighlightedSlots.Contains(highlightable)) continue;
			highlightable.Highlight();
			toAdd.Add(highlightable);
		}

		foreach (var highlightableItemSpace in toAdd) currentlyHighlightedSlots.Add(highlightableItemSpace);
	}

	private void UnHighlightTheOld()
	{
		var highlightableSlotsAtMousePos = MouseManager.GetObjectsAtMousePosition<SlotHighlight>();
		var toRemove = new List<SlotHighlight>();
		foreach (var slot in currentlyHighlightedSlots)
		{
			if (highlightableSlotsAtMousePos.Contains(slot)) continue;
			if (slot == null)
			{
				toRemove.Add(slot);
				continue;
			}

			slot.UnHighlight();
			toRemove.Add(slot);
		}

		foreach (var slot in toRemove) currentlyHighlightedSlots.Remove(slot);
	}
}