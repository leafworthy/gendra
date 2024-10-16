using UnityEngine;

public class Slot : MonoBehaviour, IGriddable
{
	
	private Vector2Int _spaceGridPos;
	private ItemSlotInventory Container => GetComponentInParent<ItemSlotInventory>();
	public Item GetItem() => _currentItem;
	private Item _currentItem;
	public void SetUnoccupied() => _currentItem = null;

	public void SetOccupied(Item item) => _currentItem = item;
	public bool IsUnoccupied => _currentItem == null;
	public bool IsDisabled { get; private set; }

	public ItemSlotInventory GetInventory() => Container;
	public void SetGridPosition(int x, int y)
	{
		_spaceGridPos = new Vector2Int(x, y);
	}

	public Vector2Int GetGridPosition() =>  _spaceGridPos;
	public void SetEmpty()
	{
		IsDisabled = true;
	}

}