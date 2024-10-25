using UnityEngine;

public class ItemOutliner : MonoBehaviour
{
	private ItemGridSpace topItemSpace;
	private Item currentlyHighlightedItem;
	private Item currentlyDraggingItem;
	private static readonly int IsHighlighted_AnimatorHash = Animator.StringToHash("IsHighlighted");
	private static readonly int IsHovering = Animator.StringToHash("IsHovering");
	private static readonly int CanDrag = Animator.StringToHash("CanDrag");

	private void Update()
	{
		OutlineHoveredItems();
		OutlineDraggedItems();
	}

	private void OutlineDraggedItems()
	{
		if (currentlyHighlightedItem == null)
		{
			if(currentlyDraggingItem != null) UnOutlineDraggedItem(currentlyDraggingItem);
			return;
		}
		if (currentlyHighlightedItem.IsDragging)
		{
			if (currentlyDraggingItem == currentlyHighlightedItem) return;
			currentlyDraggingItem = currentlyHighlightedItem;
			OutlineDraggedItem(currentlyDraggingItem);
		}
		else
		{
			if (currentlyDraggingItem != null)
			{
				UnOutlineDraggedItem(currentlyDraggingItem);
			}
			currentlyDraggingItem = null;
		}
	}

	private void OutlineHoveredItems()
	{
		var topItemGridSpaces = MouseManager.GetObjectsAtMousePosition<ItemGridSpace>();
		foreach (var space in topItemGridSpaces)
		{
			if (space == null) continue;
			if (space.GetItem() == null) continue;
			if (space.GetItem() == currentlyHighlightedItem) return;
			if (currentlyHighlightedItem != null) UnOutline(currentlyHighlightedItem);
			topItemSpace = space;
			Outline(topItemSpace.GetItem());
			return;
		}

		if (currentlyHighlightedItem != null) UnOutline(currentlyHighlightedItem);
	}

	private void Outline(Item item)
	{
		if (item == null) return;
		if (currentlyHighlightedItem == item) return;
		currentlyHighlightedItem = item;
		var _animator = item.GetComponentInChildren<Animator>(true);
		_animator.SetBool(IsHighlighted_AnimatorHash, true);
	
		var _spriteRenderer = item.GetComponentInChildren<SpriteRenderer>(true);
		
		var canDrag = item.CanDrag(Player.mainPlayer);
		_animator.SetBool(CanDrag, canDrag);
		SortingManager.ChangeLayer(_spriteRenderer, SortingManager.ItemOccupiedLayer);
		SortingManager.SetToFront(_spriteRenderer);
	}

	private void UnOutline(Item item)
	{
		var _animator = item.GetComponentInChildren<Animator>(true);
		_animator.SetBool(IsHighlighted_AnimatorHash, false);
		var canDrag = item.CanDrag(Player.mainPlayer);
		_animator.SetBool(CanDrag, canDrag);
		var _spriteRenderer = item.GetComponentInChildren<SpriteRenderer>(true);
		_spriteRenderer.color = ColorManager.GetColorFromCategoryDark(ColorManager.ItemColor.white);
		currentlyHighlightedItem = null;
	}

	private void OutlineDraggedItem(Item item)
	{
		var _animator = item.GetComponentInChildren<Animator>(true);
		_animator.SetBool(IsHovering, true);
		var _spriteRenderer = item.GetComponentInChildren<SpriteRenderer>(true);
		var color = Color.white;
		color.a = .5f;
		_spriteRenderer.color = color;
		SortingManager.ChangeLayer(_spriteRenderer, SortingManager.ItemHoverLayer);
		SortingManager.SetToFront(_spriteRenderer);
	}

	private void UnOutlineDraggedItem(Item item)
	{
		var _animator = item.GetComponentInChildren<Animator>(true);
		_animator.SetBool(IsHovering, false);
		var _spriteRenderer = item.GetComponentInChildren<SpriteRenderer>(true);
		var color = ColorManager.GetColorFromCategoryDark(ColorManager.ItemColor.white);
		color.a = 1;
		_spriteRenderer.color = color;
	}
}