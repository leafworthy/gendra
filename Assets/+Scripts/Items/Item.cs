using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Item : MonoBehaviour
{
	private bool _init;
	private Slot _slot;
	[SerializeField] private int _itemID;
	public int GetID() => _itemID;
	public ItemData GetData() => ItemData.GetItemData(GetID());
	public ColorManager.ItemColor GetColor() => GetData().itemColor;
	public Vector2 GetCenterRotationOffset() => GetRotation().GetCenterRotationOffset();
	public ItemSpaceGrid Grid => GetComponentsInChildren<ItemSpaceGrid>(true)[0];
	public ItemRotation GetRotation() => GetComponentsInChildren<ItemRotation>(true)[0];
	private List<ItemComponent> _itemComponents => GetComponentsInChildren<ItemComponent>().ToList();
	private SpriteRenderer _spriteRenderer => GetComponentInChildren<SpriteRenderer>();
	private void SetSpriteByID(int itemID) => _spriteRenderer.sprite = ItemData.GetSpriteByID(itemID);
	public ItemSlotInventory GetInventory() => _slot?.GetInventory();
	private Vector2Int inventoryPosition;
	public bool IsDragging;
	public Vector2Int GetInventoryPosition() => inventoryPosition;

	public void Setup()
	{
		if (_init) return;
		_init = true;
		_itemComponents.ForEach(component => component.Setup(GetData()));
	}

	public void SetIDAndSetupComponents(int itemID)
	{
		_itemID = itemID;
		SetSpriteByID(itemID);
		Setup();
	}

	public void DestroyItem()
	{
		GetInventory()?.DragOut(this);
		gameObject.DestroySafely();
	}

	public void SetSlot(Slot slot)
	{
		_slot = slot;
		inventoryPosition = _slot.GetGridPos();
	}

	public Slot GetSlot() =>  _slot;

	public void RotateItemCounterClockwise() => GetRotation().RotateItemCounterClockwise();
	public void RotateToDirection(Direction newDirection) => GetRotation().RotateToDirection(newDirection);

	public Vector2 GetBottomLeftPosition()
	{
		var itemSpaceGrid = Grid;
		return GetRotation().GetDirection() switch
		       {
			       Direction.Up => itemSpaceGrid.GetSpaceByGridPosition(0, 0).transform.position,
			       Direction.Left => itemSpaceGrid.GetSpaceByGridPosition(0, itemSpaceGrid.GetGridInfo().Height - 1).transform.position,
			       Direction.Down => itemSpaceGrid.GetSpaceByGridPosition(itemSpaceGrid.GetGridInfo().Width - 1,
				       itemSpaceGrid.GetGridInfo().Height - 1).transform.position,
			       Direction.Right => itemSpaceGrid.GetSpaceByGridPosition(itemSpaceGrid.GetGridInfo().Width - 1, 0).transform.position,
			       _ => Vector2.zero
		       };
	}

	public void SetPositionWithOffset(Vector2 transformPosition)
	{
		transform.position = transformPosition - GetRotation().GetRotationOffset();
	}
	public bool CanDrop(IItemContainer destinationInventory)
	{
		if (destinationInventory == null) return false;
		var pointsToTest = Grid.GetWorldPositionsOfFullSpaces();
		foreach (var point in pointsToTest)
		{
			var slots = MouseManager.GetObjectsAtPosition<Slot>(point);
			if (slots.Count <= 0) return false;

			foreach (var slot in slots)
			{
				if (slot.GetInventory() != destinationInventory) continue;

				if (!slots[0].IsUnoccupied || slots[0].IsDisabled) return false;
			}
		}

		return true;
	}
}