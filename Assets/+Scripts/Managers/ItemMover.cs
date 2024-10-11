using System;
using System.Linq;
using UnityEngine;

public interface IInventoryCommand
{
	public void Redo();
	public void Undo();
}


public class ItemMover : MonoBehaviour
{
	private Item _item;
	private Vector2 _item_originalPosition;
	private Vector2 _item_mouseOffset;
	private IItemContainer itemOriginalItemContainer;
	private Direction _item_originalDirection;
	public static event Action<Item> OnItemStopDragging;
	public static event Action<Item> OnItemStartDragging;
	public static event Action<Item> OnItemRotateCounterClockwise;

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
		if (_item == null)
		{
			Debug.Log("no item to stop dragging");
			return;
		}
		Debug.Log("stop dragging");
		_item.StopDragging();
		OnItemStopDragging?.Invoke(_item);
		_item = null;
	}

	private void RotateItemCounterClockwise()
	{
		if (_item == null) return;
		_item.RotateCounterClockwise();
		OnItemRotateCounterClockwise?.Invoke(_item);
	}

	private void OnPress()
	{
		Debug.Log("on press here");
		var draggableItemsAtMousePosition = GetItemGridSpaceAtMousePosition();

		if (draggableItemsAtMousePosition != null)
		{
			Debug.Log( "start dragging item" + draggableItemsAtMousePosition.GetItem().GetData().itemID);
			StartDraggingItem(draggableItemsAtMousePosition.GetComponentInParent<Item>());
		}
	}

	private static ItemGridSpace GetItemGridSpaceAtMousePosition()
	{
		var draggableItemsAtMousePosition = MouseManager.GetObjectsAtMousePosition<ItemGridSpace>().Where(x => x.IsEmptySpace == false).ToList();
		return draggableItemsAtMousePosition.Count <= 0 ? null : draggableItemsAtMousePosition[0];
	}

	private void StartDraggingItem(Item newDraggingItem)
	{
		_item = newDraggingItem;
		_item.transform.SetParent(null);
		Debug.Log("start dragging");
		_item.StartDragging();
		OnItemStartDragging?.Invoke(_item);
	}
}