using UnityEngine;

public class ItemHighlighter : MonoBehaviour
{
	private ItemGridSpace topItemSpace;
	private Item currentlyHighlightedItem;
	private Item currentlyDraggingItem;
	private static readonly int IsHighlighted_AnimatorHash = Animator.StringToHash("IsHighlighted");
	private static readonly int IsHovering = Animator.StringToHash("IsHovering");

	private void Update()
	{
		HighlightHoveredItems();
		HighlightDraggedItems();
	}

	private void HighlightDraggedItems()
	{
		if (currentlyHighlightedItem == null) return;
		if (currentlyHighlightedItem.GetMovement().IsDragging())
		{
			if (currentlyDraggingItem == currentlyHighlightedItem) return;
			currentlyDraggingItem = currentlyHighlightedItem;
			HighlightDragging(currentlyDraggingItem);
		}
		else
		{
			if (currentlyDraggingItem != null)
			{
				UnHighlightDragging(currentlyDraggingItem);
			}
			currentlyDraggingItem = null;
		}
	}

	private void HighlightHoveredItems()
	{
		var topItemGridSpaces = MouseManager.GetObjectsAtMousePosition<ItemGridSpace>();
		foreach (var space in topItemGridSpaces)
		{
			if (space == null) continue;
			if (space.GetItem() == null) continue;
			if (space.GetItem() == currentlyHighlightedItem) return;
			if (currentlyHighlightedItem != null) UnHighlight(currentlyHighlightedItem);
			topItemSpace = space;
			Highlight(topItemSpace.GetItem());
			return;
		}

		if (currentlyHighlightedItem != null) UnHighlight(currentlyHighlightedItem);
	}

	private void Highlight(Item item)
	{
		if (item == null) return;
		if (currentlyHighlightedItem == item) return;
		currentlyHighlightedItem = item;
		var _animator = item.GetComponentInChildren<Animator>(true);
		_animator.SetBool(IsHighlighted_AnimatorHash, true);
		var _spriteRenderer = item.GetComponentInChildren<SpriteRenderer>(true);
		_spriteRenderer.color = Color.white;
		SortingManager.ChangeLayer(_spriteRenderer, SortingManager.ItemOccupiedLayer);
		SortingManager.SetToFront(_spriteRenderer);
	}

	private void UnHighlight(Item item)
	{
		var _animator = item.GetComponentInChildren<Animator>(true);
		_animator.SetBool(IsHighlighted_AnimatorHash, false);
		var _spriteRenderer = item.GetComponentInChildren<SpriteRenderer>(true);
		_spriteRenderer.color = ColorManager.GetColorFromCategoryDark(ColorManager.ItemColor.white);
		currentlyHighlightedItem = null;
		currentlyDraggingItem = null;
	}

	private void HighlightDragging(Item item)
	{
		var _animator = item.GetComponentInChildren<Animator>(true);
		_animator.SetBool(IsHovering, true);
		var _spriteRenderer = item.GetComponentInChildren<SpriteRenderer>(true);
		_spriteRenderer.color = Color.white;
		SortingManager.ChangeLayer(_spriteRenderer, SortingManager.ItemHoverLayer);
		SortingManager.SetToFront(_spriteRenderer);
	}

	private void UnHighlightDragging(Item item)
	{
		var _animator = item.GetComponentInChildren<Animator>(true);
		_animator.SetBool(IsHovering, false);
		var _spriteRenderer = item.GetComponentInChildren<SpriteRenderer>(true);
		_spriteRenderer.color = ColorManager.GetColorFromCategoryDark(ColorManager.ItemColor.white);
	}
}