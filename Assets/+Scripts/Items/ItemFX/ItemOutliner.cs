using UnityEngine;

public class ItemOutliner : MonoBehaviour
{
	private ItemGridSpace topItemSpace;
	private Item currentlyOutlinedItem;
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
		if (currentlyOutlinedItem == null)
		{
			if(currentlyDraggingItem != null) UnOutlineDraggedItem(currentlyDraggingItem);
			return;
		}
		if (currentlyOutlinedItem.IsDragging)
		{
			if (currentlyDraggingItem == currentlyOutlinedItem) return;
			currentlyDraggingItem = currentlyOutlinedItem;
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
			if (space.GetItem() == currentlyOutlinedItem) return;
			if (currentlyOutlinedItem != null) UnOutline(currentlyOutlinedItem);
			topItemSpace = space;
			Outline(topItemSpace.GetItem());
			return;
		}

		if (currentlyOutlinedItem != null) UnOutline(currentlyOutlinedItem);
	}

	private void Outline(Item item)
	{
		if (item == null) return;
		if (currentlyOutlinedItem == item) return;
		currentlyOutlinedItem = item;
		var _animator = item.GetComponentInChildren<Animator>(true);
		_animator.SetBool(IsHighlighted_AnimatorHash, true);
	
		var _spriteRenderer = item.GetComponentInChildren<SpriteRenderer>(true);
		
		var canDrag = item.CanDrag(Player.mainPlayer);
		_animator.SetBool(CanDrag, canDrag);
		ItemSortingManager.ChangeLayer(_spriteRenderer, ItemSortingManager.ItemOccupiedLayer);
		ItemSortingManager.SetToFront(_spriteRenderer);
	}

	private void UnOutline(Item item)
	{
		var _animator = item.GetComponentInChildren<Animator>(true);
		_animator.SetBool(IsHighlighted_AnimatorHash, false);
		var canDrag = item.CanDrag(Player.mainPlayer);
		_animator.SetBool(CanDrag, canDrag);
		var _spriteRenderer = item.GetComponentInChildren<SpriteRenderer>(true);
		_spriteRenderer.color = ColorManager.GetColorFromCategoryDark(ColorManager.ItemColor.white);
		currentlyOutlinedItem = null;
	}

	private void OutlineDraggedItem(Item item)
	{
		var _animator = item.GetComponentInChildren<Animator>(true);
		_animator.SetBool(IsHovering, true);
		var _spriteRenderer = item.GetComponentInChildren<SpriteRenderer>(true);
		var color = Color.white;
		color.a = .5f;
		_spriteRenderer.color = color;
		ItemSortingManager.ChangeLayer(_spriteRenderer, ItemSortingManager.ItemHoverLayer);
		ItemSortingManager.SetToFront(_spriteRenderer);
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