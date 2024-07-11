using NaughtyAttributes;
using UnityEngine;

public class SingleItemInventory : MonoBehaviour, IItemContainer
{
	private Item _item;
	private int multiplier = 2;
	private BoxCollider2D _collider => GetComponent<BoxCollider2D>();
	public Vector2Int size;
	public Vector2Int margin => new(2, 2);
	private SpriteRenderer _spriteRenderer => GetComponentInChildren<SpriteRenderer>();

	[Button]
	public void Refresh()
	{
		_spriteRenderer.size = size+margin;
		_collider.size = size+margin;
	}



	public bool DragIn(Item draggingItem)
	{
		if (_item != null) return false;
		_item = draggingItem;
		_item.transform.SetParent(transform);
		_item.transform.localScale = Vector3.one * multiplier;
		_item.transform.position =( (Vector2)transform.position -_item.GetRotation().GetCenterRotationOffset() *multiplier);
		_spriteRenderer.color = Color.white;
		_item.SetInventory(this);
		return true;
	}

	public bool DragOut(Item item)
	{
		_item.transform.SetParent(null);
		_item.transform.localScale = Vector3.one;
		_item.transform.position = ( (Vector2)transform.position -_item.GetRotation().GetCenterRotationOffset());
		_item.SetInventory(null);
		_spriteRenderer.color = Color.grey;
		_item = null;
		return true;
	}
}