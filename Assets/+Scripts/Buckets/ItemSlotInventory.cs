using System.Collections.Generic;
using System.Linq;
using NaughtyAttributes;
using UnityEditor;
using UnityEngine;

[ExecuteInEditMode]
public class ItemSlotInventory : MonoBehaviour, IItemContainer
{
	[SerializeField] private GameObject _itemHolder;
	private SlotGrid _slotGrid => GetComponentInChildren<SlotGrid>();
	
	[SerializeField] private GridInfo slotGridInfo;
	private List<Item> _items => GetComponentsInChildren<Item>().ToList();
	public GridInfo GetGridInfo() => slotGridInfo;
	public Grid GetGrid() => _slotGrid;

	private bool _init;


	public void Start()
	{
		if (_slotGrid == null) return;
		if (_init) return;
		_init = true;
		if(_itemHolder == null)_itemHolder = new GameObject();
		SetupGrid();
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
		var itemMovement = item.GetComponent<ItemMovement>();
		var itemRotation = item.GetComponent<ItemRotation>();
		var slot = GetSlotAtWorldPosition(itemRotation.GetBottomLeftPosition());
		if (slot == null)
		{
			Debug.Log("slot null");
			return false;
		}
		
		//item.transform.position = (Vector2) slot.transform.position - itemRotation.GetRotationOffset();
		
		if (itemMovement.CanDrop())
		{
			
			AddItemToInventory(item, slot);
			return true;
		}
		Debug.Log("can't drop");
		return false;
	}

	
	private void AddItemToInventory(Item item, Slot slot)
	{
		item.ItemContainer = this;
		item.transform.position = (Vector2) slot.transform.position - item.GetComponent<ItemRotation>().GetRotationOffset();
		Utils.DrawX(item.transform.position, 0.5f, Color.green);
		Utils.DrawX(slot.transform.position, 0.5f, Color.yellow);
		OccupyHoveredSlots(item, slot);
		item.transform.SetParent(_itemHolder.transform);

		_items.Add(item);
	}

	public bool DragOut(Item item)
	{
		UnoccupyHoveredSlots(item);
		item.transform.SetParent(null);
		_items.Remove(item);
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
			
			if (DragIntoSlot(item, slot))
			{
				Utils.DrawX(slot.transform.position, 0.25f, Color.green);
				return;
			}
			else
			{
				Utils.DrawX(slot.transform.position, 0.5f, Color.blue);
			}
		}

		item.DestroyItem();
	}

	private bool DragIntoSlot(Item item, Slot slot)
	{
		if (!item.GetComponent<ItemMovement>().CanDrop()) return false;
		AddItemToInventory(item, slot);
		return true;
	}

	public GameObject CirclePosition;


	private void SetAllSlotsUnoccupied()
	{
		foreach (var slot in _slotGrid.GetSlots())
		{
			slot.SetUnoccupied();
		}
	}



	private void OccupyHoveredSlots(Item item, IItemContainer inventory)
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
			foreach (var slot in slots)
			{
				hoveredSlots.Add(slot);
			}
		}

		return hoveredSlots;
	}

	private void UnoccupyHoveredSlots(Item item)
	{
		foreach (var slot in GetHoveredSlots(item))
		{
			if (slot.GetInventory() != item.ItemContainer) continue;
			slot.SetUnoccupied();
		}
	}

}