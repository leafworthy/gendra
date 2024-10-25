using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Item : MonoBehaviour
{
	private bool _init;
	[SerializeField] private int _itemID;
	public int GetID() => _itemID;
	private void SetSpriteByID(int itemID) => _spriteRenderer.sprite = ItemData.GetSpriteByID(itemID);
	public ItemData GetData() => ItemData.GetItemData(GetID());
	public ColorManager.ItemColor GetColor() => GetData().itemColor;
	
	//ITEM SPACE GRID
	private ItemSpaceGrid Grid => GetComponentsInChildren<ItemSpaceGrid>(true)[0];
	public GridInfo GetGridInfo() => Grid.GetGridInfo();
	public List<Vector2> GetWorldPositionsOfFullSpaces() => Grid.GetWorldPositionsOfFullSpaces();

	public List<Vector2Int> GetEmptyGridSpaces() => Grid.GetEmptyGridSpaces();
	
	//POSITION/DIRECTION
	private Slot _slot;
	public Slot GetSlot() => _slot;
	public void SetSlot(Slot slot) => _slot = slot;
	public ItemSlotInventory GetInventory() => _slot?.GetInventory();
	public bool IsDragging;
	public Vector2Int GetInventoryPosition() => _slot == null ? Vector2Int.zero : _slot.GetGridPosition();
	
	private SpriteRenderer _spriteRenderer => GetComponentInChildren<SpriteRenderer>();
	private Direction _direction;
	public Direction GetDirection() => _direction;
	public void SetDirection(Direction dir) => _direction = dir;
	public void Setup()
	{
		if (_init) return;
		_init = true;
		GetComponentsInChildren<ItemComponent>().ToList().ForEach(component => component.Setup(GetData()));
	}

	
	public void SetSpriteAlpha(float alpha)
	{
		var color = _spriteRenderer.color;
		color.a = alpha;
		_spriteRenderer.color = color;
		Debug.Log("setting alpha: "  + alpha);
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

	

	public Vector2 GetBottomLeftPosition()
	{
		var itemSpaceGrid = Grid;
		return GetDirection() switch
		       {
			       Direction.Up => itemSpaceGrid.GetSpaceByGridPosition(0, 0).transform.position,
			       Direction.Left => itemSpaceGrid.GetSpaceByGridPosition(0, itemSpaceGrid.GetGridInfo().Height - 1).transform.position,
			       Direction.Down => itemSpaceGrid.GetSpaceByGridPosition(itemSpaceGrid.GetGridInfo().Width - 1,
				       itemSpaceGrid.GetGridInfo().Height - 1).transform.position,
			       Direction.Right => itemSpaceGrid.GetSpaceByGridPosition(itemSpaceGrid.GetGridInfo().Width - 1, 0).transform.position,
			       _ => Vector2.zero
		       };
	}

	
	public bool CanDrop(ItemSlotInventory destinationInventory)
	{
		if (destinationInventory == null) return false;
		var pointsToTest = Grid.GetWorldPositionsOfFullSpaces();
		foreach (var point in pointsToTest)
		{
			var slots = MouseManager.GetObjectsAtPosition<Slot>(point);
			if (slots.Count <= 0) return false;

			foreach (var slot in slots)
			{
				if (slot.GetInventory() !=  destinationInventory) continue;

				if (!slots[0].IsUnoccupied || slots[0].IsDisabled) return false;
			}
		}

		return true;
	}

	public bool CanDrag(Player mainPlayer) => mainPlayer.money >= GetData().cost;
}