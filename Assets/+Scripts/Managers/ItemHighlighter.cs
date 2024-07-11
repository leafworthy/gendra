using System.Collections.Generic;
using UnityEngine;

public class ItemHighlighter : MonoBehaviour
{
	private List<ItemHighlight> currentlyHighlightedItemsList = new();

	private void Update()
	{
		
		UnHighlightTheOld();
		HighlightTheNew();
	}

	private void HighlightTheNew()
	{
		var highlightableItemsAtMousePos = MouseManager.GetObjectsAtMousePosition<ItemGridSpace>();
		var toAdd = new List<ItemGridSpace>();
		foreach (var highlightable in highlightableItemsAtMousePos)
		{
			var item = highlightable.GetComponentInParent<ItemHighlight>();
			if (currentlyHighlightedItemsList.Contains(item)) continue;
			item.Highlight();
			toAdd.Add(highlightable);
		}

		foreach (var highlightableItemSpace in toAdd) currentlyHighlightedItemsList.Add(highlightableItemSpace.GetComponentInParent<ItemHighlight>());
	}

	private void UnHighlightTheOld()
	{
		var highlightableItemSpacesAtMousePos = MouseManager.GetObjectsAtMousePosition<ItemGridSpace>();
		var highlightableItemsAtMousePos = new List<ItemHighlight>();
		foreach (var itemSpace in highlightableItemSpacesAtMousePos)
		{
			var item = itemSpace.GetComponentInParent<ItemHighlight>();
			if(highlightableItemsAtMousePos.Contains(item)) continue;
			highlightableItemsAtMousePos.Add(item);
		}
		var toRemove = new List<ItemHighlight>();
		foreach (var item in currentlyHighlightedItemsList)
		{
			if (highlightableItemsAtMousePos.Contains(item)) continue;
			if(item == null)
			{
				toRemove.Add(item);
				continue;
			}
			item.UnHighlight();
			toRemove.Add(item);
		}

		foreach (var highlightableBucket in toRemove) currentlyHighlightedItemsList.Remove(highlightableBucket);
	}
}