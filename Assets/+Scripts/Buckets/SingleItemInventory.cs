using NaughtyAttributes;
using UnityEngine;

public class SingleItemInventory : MonoBehaviour, IItemContainer
{
	private Item _currentItem;
	private Vector2 _originalItemScale;
	private BoxCollider2D _collider => GetComponent<BoxCollider2D>();
	[SerializeField]private int scaleMultiplier = 2;
	[SerializeField] private Vector2Int size;
	private Vector2Int margin => new(2, 2);
	private SpriteRenderer _spriteRenderer => GetComponentInChildren<SpriteRenderer>();

	[Button]
	public void Refresh()
	{
		_spriteRenderer.size = size+margin;
		_collider.size = size+margin;
	}

	public bool DragIn(Item draggingItem)
	{
		if (_currentItem != null) return false;
		_currentItem = draggingItem;
		_currentItem.transform.SetParent(transform);
		_originalItemScale = _currentItem.transform.localScale;
		_currentItem.transform.localScale = Vector3.one * scaleMultiplier;
		var _itemMovement = _currentItem.GetComponent<ItemMovement>();
		_itemMovement.SetPosition(transform.position);
		_spriteRenderer.color = Color.white;
		_currentItem.ItemContainer = this;
		return true;
	}

	public bool DragOut(Item item)
	{
		_currentItem.transform.SetParent(null);
		_currentItem.transform.localScale = _originalItemScale;
		var _itemMovement = _currentItem.GetComponent<ItemMovement>();
		_itemMovement.SetPosition(transform.position);
		_currentItem.ItemContainer = null;
		_spriteRenderer.color = Color.grey;
		_currentItem = null;
		return true;
	}
}