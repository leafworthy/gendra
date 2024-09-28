using UnityEngine;

public class Slot : MonoBehaviour, IGriddable, IItemContainer
{
	public Vector2Int GetGridPos() => _spaceGridPos;
	private Vector2Int _spaceGridPos;
	private ItemSlotInventory Container => GetComponentInParent<ItemSlotInventory>();
	public Item GetItem() => _currentItem;
	private Item _currentItem;
	public void SetUnoccupied() => _currentItem = null;

	public void SetOccupied(Item item) => _currentItem = item;
	public bool IsUnoccupied => _currentItem == null;
	private static Vector3 centerOffset = new(0.5f, 0.5f, 0);
	private bool _isDisabled;
	public bool IsDisabled => _isDisabled;

	public IItemContainer GetInventory() => Container;
	public void SetGridPosition(int x, int y)
	{
		_spaceGridPos = new Vector2Int(x, y);
	}

	public Vector2Int GetGridPosition() =>  _spaceGridPos;

	public bool DragIn(Item draggingItem) => Container.DragIn(draggingItem);

	public bool DragOut(Item item) => Container.DragOut(item);

	public Vector2 GetCenter() => transform.position + centerOffset;

	public void SetDisabled()
	{
		_isDisabled = true;
	}

}