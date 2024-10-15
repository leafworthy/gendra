using System;
using System.Linq;
using UnityEngine;

public class ItemMover : MonoBehaviour
{
	private Item _draggingItem;
	private Vector2Int _originalPosition;
	private Vector2 _mouseDragOffset;
	private ItemSlotInventory _originalItemContainer;
	private Direction _originalDirection;
	public static event Action<Item> OnItemStopDragging;
	public static event Action<Item> OnItemStartDragging;

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

	private void Update()
	{
		if (_draggingItem != null) _draggingItem.transform.position = MouseManager.GetMouseWorldPosition() - _mouseDragOffset;
	}

	private void StopDraggingItem()
	{
		Debug.Log("stop dragging");
		if (_draggingItem == null)
		{
			Debug.Log("no item");
			return;
		}

		var slotsAtMousePosition = MouseManager.GetObjectsAtMousePosition<Slot>();
		if (slotsAtMousePosition.Count <= 0)
		{
			Debug.Log("no slot");
			DragItemBack();
		}
		else
		{
			Debug.Log("slot");
			var slot = slotsAtMousePosition[0];
			if (slot == null)
			{
				DragItemBack();
				return;
			}

			if (!slot.DragIn(_draggingItem)) DragItemBack();
		}

		OnItemStopDragging?.Invoke(_draggingItem);
		_draggingItem.IsDragging = false;
		_draggingItem = null;
	}

	private void DragItemBack()
	{
		_draggingItem.RotateToDirection(_originalDirection);
		if(_originalItemContainer == null) return;
		_draggingItem.SetPositionWithOffset(_originalItemContainer.GetSlotFromGridPosition(_originalPosition).transform.position);
		_originalItemContainer?.DragIn(_draggingItem);
	}

	private void RotateItemCounterClockwise()
	{
		if (_draggingItem == null) return;
		_draggingItem.RotateItemCounterClockwise();
		_mouseDragOffset = _draggingItem.GetCenterRotationOffset();
	}

	private void OnPress()
	{
		var draggableItemsAtMousePosition = GetItemGridSpaceAtMousePosition();
		if (draggableItemsAtMousePosition == null) return;
		StartDraggingItem(draggableItemsAtMousePosition.GetComponentInParent<Item>());
	}

	private static ItemGridSpace GetItemGridSpaceAtMousePosition()
	{
		var draggableItemsAtMousePosition = MouseManager.GetObjectsAtMousePosition<ItemGridSpace>().Where(x => x.IsEmptySpace == false).ToList();
		return draggableItemsAtMousePosition.Count <= 0 ? null : draggableItemsAtMousePosition[0];
	}

	private void StartDraggingItem(Item newDraggingItem)
	{
		_draggingItem = newDraggingItem;
		_draggingItem.IsDragging = true;
		_originalPosition = _draggingItem.GetInventoryPosition();
		_originalDirection = _draggingItem.GetRotation().GetDirection();
		_originalItemContainer = _draggingItem.GetInventory();
		_originalItemContainer?.DragOut(_draggingItem);
		_mouseDragOffset = MouseManager.GetMouseWorldPosition() - (Vector2) _draggingItem.transform.position;
		OnItemStartDragging?.Invoke(_draggingItem);
	}
}