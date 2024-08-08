using System;
using System.Collections.Generic;
using System.Linq;
using NaughtyAttributes;
using UnityEngine;

[ExecuteInEditMode]
public class ItemSlotInventory : MonoBehaviour, IItemContainer, ItemComponent
{
	[SerializeField] private GameObject _itemHolder;
	private SlotGrid _slotGrid => GetComponentInChildren<SlotGrid>();
	
	[SerializeField] private GridInfo slotGridInfo;
	private List<Item> _items => GetComponentsInChildren<Item>().ToList();
	public GridInfo GetGridInfo() => slotGridInfo;
	public Grid GetGrid() => _slotGrid;

	private bool _init;
	public event Action OnSetupComplete;
	public event Action OnInventoryChanged;

	
	public void Setup(ItemData data)
	{
		if (_slotGrid == null) return;
		if (_init) return;
		_init = true;
		if(_itemHolder == null)_itemHolder = new GameObject();
		SetupGrid();
		OnSetupComplete?.Invoke();
		
	}

	private void SetupGrid()
	{
		_slotGrid.MakeGrid(slotGridInfo, Prefabs.I.SlotPrefab);
		Debug.Log("made grid");

		SetAllSlotsUnoccupied();
	}

	[Button]
	public void ReSetup()
	{
		SetupGrid();
	}

	private Slot GetSlotAtWorldPosition(Vector2 position)
	{
		var hits = MouseManager.GetObjectsAtPosition<Slot>(position);
		return hits.Count <= 0 ? null : hits[0];
	}

	public bool DragIn(Item item)
	{
		var slot = GetSlotAtWorldPosition(item.GetMovement().GetBottomLeftPosition());
		if (slot == null)
		{
			Debug.Log("slot null");
			return false;
		}

		if (item.CanDrop(this))
		{
			AddItemToInventory(item, slot);
			return true;
		}

		Debug.Log("can't drop");
		return false;
	}

	private void AddItemToInventory(Item item, Slot slot)
	{
		item.transform.SetParent(_itemHolder.transform);
		item.SetPositionMinusRotationOffset(slot.transform.position);
		item.SetInventory(this);
		OccupyHoveredSlots(item);

		_items.Add(item);
		OnInventoryChanged?.Invoke();
	}

	public bool DragOut(Item item)
	{
		UnoccupyHoveredSlots(item);
		item.transform.SetParent(null);
		_items.Remove(item);
		OnInventoryChanged?.Invoke();
		return true;
	}

	[Button]
	public void ClearItems()
	{
		foreach (var slot in _slotGrid.GetSlots())
		{
			slot.SetUnoccupied();
		}

		var toDestroy = new List<Item>();
		foreach (var item in _items)
		{
			if(item.gameObject == this.gameObject) continue;
			toDestroy.Add(item);
		}

		foreach (var item in toDestroy)
		{
			item.DestroyItem();
		}
	}

	public void AddItemIntoEmptySlot(Item item)
	{
		foreach (var slot in _slotGrid.GetSlots())
		{
			if (MoveItemToSlotAndDragIn(item, slot)) return;
		}

		item.DestroyItem();
	}



	private void SetAllSlotsUnoccupied()
	{
		foreach (var slot in _slotGrid.GetSlots())
		{
			slot.SetUnoccupied();
		}
	}

	private bool MoveItemToSlotAndDragIn(Item item, Slot slot)
	{
		item.GetMovement().SetPositionMinusRotationOffset(slot.transform.position);
		return DragIn(item);
	}

	private void OccupyHoveredSlots(Item item)
	{
		foreach (var slot in GetHoveredSlots(item))
		{
			slot.SetOccupied(item);
		}
	}

	private List<Slot> GetHoveredSlots(Item item)
	{
		var pointsToTest = item.Grid.GetWorldPositionsOfFullSpaces();
		var hoveredSlots = new List<Slot>();
		foreach (var point in pointsToTest)
		{
			var slots = MouseManager.GetObjectsAtPosition<Slot>(point);
			if (slots.Count <= 0) continue;
			hoveredSlots.Add(slots[0]);
		}

		return hoveredSlots;
	}

	private void UnoccupyHoveredSlots(Item item)
	{
		foreach (var slot in GetHoveredSlots(item))
		{
			if (slot.GetInventory() != item.GetInventory()) continue;
			slot.SetUnoccupied();
		}
	}

}