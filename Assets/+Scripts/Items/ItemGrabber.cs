using System;
using System.Linq;
using UnityEngine;

public class ItemGrabber : MonoBehaviour
{
	public static Item DraggingItem { get; private set; }
	private static Vector2Int _originalPosition;
	private static Vector2 _mouseDragOffset;
	private static ItemSlotInventory _originalItemContainer;
	private static Direction _originalDirection;
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

	private static void OnRightPress() => RotateItemCounterClockwise();

	private static void OnRelease() => StopDraggingItem();

	private void Update()
	{
		if (DraggingItem != null) DraggingItem.transform.position = MouseManager.GetMouseWorldPosition() - _mouseDragOffset;
	}

	private static void StopDraggingItem()
	{
		if (DraggingItem == null) return;

		var slotBeneathItem = MouseManager.GetObjectsAtPosition<Slot>(DraggingItem.GetBottomLeftPosition());

		if (slotBeneathItem.Count <= 0)
			DragItemBack();
		else
		{
			var slot = slotBeneathItem[0];
			if (slot == null)
			{
				DragItemBack();
				return;
			}

			MoveToPosition(DraggingItem, slot.transform.position);
			if (!slot.GetInventory().DragIn(DraggingItem)) DragItemBack();
		}

		OnItemStopDragging?.Invoke(DraggingItem);
		DraggingItem.IsDragging = false;
		DraggingItem.SetSpriteAlpha(1);
		DraggingItem = null;
	}

	private static void DragItemBack()
	{
		if (DraggingItem == null) return;
		ItemRotator.RotateToDirection(DraggingItem, _originalDirection);
		if (_originalItemContainer == null) return;
		MoveToPosition(DraggingItem, _originalItemContainer.GetSlotFromGridPosition(_originalPosition).transform.position);
		_originalItemContainer?.DragIn(DraggingItem);
	}

	private static void RotateItemCounterClockwise()
	{
		if (DraggingItem == null) return;
		ItemRotator.RotateItemCounterClockwise(DraggingItem);
		_mouseDragOffset = ItemRotator.GetCenterRotationOffset(DraggingItem);

		DraggingItem.transform.position = MouseManager.GetMouseWorldPosition() - _mouseDragOffset;
	}

	private void OnPress()
	{
		var draggableItemAtMousePosition = GetItemGridSpaceAtMousePosition();
		if (draggableItemAtMousePosition == null) return;
		var item = draggableItemAtMousePosition.GetComponentInParent<Item>();
		if (item == null) return;
		if (!item.CanDrag(Player.mainPlayer)) return;
		StartDraggingItem(item);
		Player.mainPlayer.SpendMoney(item.GetData().cost);
	}

	private static ItemGridSpace GetItemGridSpaceAtMousePosition()
	{
		var draggableItemsAtMousePosition = MouseManager.GetObjectsAtMousePosition<ItemGridSpace>().Where(x => x.IsEmptySpace == false).ToList();
		return draggableItemsAtMousePosition.Count <= 0 ? null : draggableItemsAtMousePosition[0];
	}

	private static void StartDraggingItem(Item newDraggingItem)
	{
		DraggingItem = newDraggingItem;
		DraggingItem.IsDragging = true;
		DraggingItem.SetSpriteAlpha(.5f);
		_originalPosition = DraggingItem.GetInventoryPosition();
		_originalDirection = DraggingItem.GetDirection();
		_originalItemContainer = DraggingItem.GetInventory();
		_originalItemContainer?.DragOut(DraggingItem);
		_mouseDragOffset = MouseManager.GetMouseWorldPosition() - (Vector2) DraggingItem.transform.position;
		OnItemStartDragging?.Invoke(DraggingItem);
	}

	public static void MoveItemToSlot(Item item, Slot slot) => MoveToPosition(item, slot.transform.position);

	public static void RotateToDirection(Item item, Direction newDirection) => ItemRotator.RotateToDirection(item, newDirection);

	public static void MoveToPosition(Item item, Vector2 transformPosition)
	{
		item.transform.position = transformPosition - ItemRotator.GetRotationOffset(item);
	}
}