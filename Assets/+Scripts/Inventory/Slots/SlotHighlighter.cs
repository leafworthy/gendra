
using UnityEngine;

public class SlotHighlighter : MonoBehaviour
{
	private SlotHighlight currentHighlightedSlot;

	private void Update()
	{
		HighlightSlotUnderMouse();
	}


	private void HighlightSlotUnderMouse()
	{
		var highlightableSlotsAtMousePosition = MouseManager.GetObjectsAtMousePosition<SlotHighlight>();
		if (highlightableSlotsAtMousePosition.Count <= 0)
		{
			if (currentHighlightedSlot != null) currentHighlightedSlot.SetHighlighted(false);
			return;
		}
		var newSlot = highlightableSlotsAtMousePosition[0];
		if (currentHighlightedSlot == newSlot) return;
		if(currentHighlightedSlot != null)currentHighlightedSlot.SetHighlighted(false);
		currentHighlightedSlot = highlightableSlotsAtMousePosition[0];
		currentHighlightedSlot.SetHighlighted(true);
	}

}