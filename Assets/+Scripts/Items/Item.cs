using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Item : MonoBehaviour
{
	private IItemContainer itemContainer;
	private Vector2 _mouseOffset;
	public int GetID() => _itemID;
	[SerializeField] private int _itemID;
	private bool _init;
	public ItemData GetData() => ItemData.GetItemData(GetID());

	public event Action OnRotateCounterClockwise;
	public event Action OnDragStart;
	public event Action OnDragStop;

	public void StartDragging() => OnDragStart?.Invoke();
	public void StopDragging() => OnDragStop?.Invoke();
	public ItemSpaceGrid Grid => GetComponentsInChildren<ItemSpaceGrid>(true)[0];

	public ItemMovement GetMovement() => GetComponentsInChildren<ItemMovement>(true)[0];

	public ItemRotation GetRotation() => GetComponentsInChildren<ItemRotation>(true)[0];

	private ItemGraphic GetGraphic() => GetComponentsInChildren<ItemGraphic>(true)[0];
	public ColorManager.ItemColor GetColor() => GetGraphic().GetColor();

	private List<ItemComponent> _itemComponents => GetComponentsInChildren<ItemComponent>().ToList();

	

	public void Setup()
	{
		if (_init) return;
		_init = true;
		_itemComponents.ForEach(component => component.Setup(GetData()));
	}

	public void SetIDAndSetupComponents(int itemID)
	{
		_itemID = itemID;
		Setup();
	}

	public void DestroyItem()
	{
		GetInventory()?.DragOut(this);
		gameObject.DestroySafely();
	}

	public IItemContainer GetInventory() => itemContainer;
	public void SetInventory(IItemContainer _itemContainer) => itemContainer = _itemContainer;

	public void SetPositionMinusRotationOffset(Vector3 newPosition) => GetMovement().SetPositionMinusRotationOffset(newPosition);

	public void RotateCounterClockwise() => OnRotateCounterClockwise?.Invoke();


	public bool CanDrop(IItemContainer inventory) => GetMovement().CanDrop(inventory);

	public Slot GetSlot()
	{
		if (itemContainer is ItemSlotInventory inventory)
		{
			return inventory.GetSlotAtWorldPosition(GetMovement().GetBottomLeftPosition());
		}

		return null;
	}
}