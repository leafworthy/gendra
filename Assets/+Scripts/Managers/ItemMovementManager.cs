using System.Linq;
using UnityEngine;

public class ItemMovementManager : MonoBehaviour
{
	private Item _item;
	private ItemMovement _itemMovement => _item.GetComponent<ItemMovement>();
	private Vector2 _item_originalPosition;
	private Vector2 _item_mouseOffset;
	private IItemContainer itemOriginalItemContainer;
	private Direction _item_originalDirection;

	private void OnEnable()
	{
		MouseManager.OnPress += OnPress;
		MouseManager.OnRelease += OnRelease;
		MouseManager.OnRightPress += OnRightPress;
	}

	private void OnDisable()
	{
		MouseManager.OnPress -= OnPress;
		MouseManager.OnRelease -= OnRelease;
		MouseManager.OnRightPress -= OnRightPress;
	}

	private void OnRightPress() => RotateItemCounterClockwise();

	private void OnRelease() => StopDraggingItem();

	private void StopDraggingItem()
	{
		if (_item == null) return;
		_itemMovement.StopDragging();
		_item = null;
	}

	private void RotateItemCounterClockwise()
	{
		if (_item == null) return;
		var _itemRotation = _item.GetComponent<ItemRotation>();
		_itemRotation.RotateItemCounterClockwise();
	}

	private void OnPress()
	{
		var draggableItemsAtMousePosition = GetItemGridSpaceAtMousePosition();

		if (draggableItemsAtMousePosition != null) StartDraggingItem(draggableItemsAtMousePosition.GetComponentInParent<Item>());
	}

	private static ItemGridSpace GetItemGridSpaceAtMousePosition()
	{
		var draggableItemsAtMousePosition = MouseManager.GetObjectsAtMousePosition<ItemGridSpace>().Where(x => x.IsEmptySpace == false).ToList();
		return draggableItemsAtMousePosition.Count <= 0 ? null : draggableItemsAtMousePosition[0];
	}

	private void StartDraggingItem(Item newDraggingItem)
	{
		_item = newDraggingItem;
		_itemMovement.StartDragging();
		
	}
}