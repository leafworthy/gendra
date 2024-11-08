using UnityEngine;

public class InventoryBackgroundResizer : MonoBehaviour
{
	[SerializeField] private SpriteRenderer _inventoryBG;
	private BoxCollider2D _inventoryCollider => _inventoryBG.GetComponent<BoxCollider2D>();
	private ItemSlotInventory _inventory => GetComponent<ItemSlotInventory>();

	public float GetBGWidth() => _inventoryBG.bounds.size.x;

	private void ResizeInventoryBG()
	{
		var spriteRendererBounds = _inventoryBG.bounds.size;
		spriteRendererBounds.x = _inventory.GetGrid().gridInfo.Width + 3;
		spriteRendererBounds.y = _inventory.GetGrid().gridInfo.Height + 3;

		_inventoryBG.size = spriteRendererBounds;
		_inventoryBG.transform.localPosition = Vector3.zero;

		_inventoryCollider.size = _inventoryBG.bounds.size;
		_inventoryCollider.offset = Vector2.zero;
		CenterGrid();
	}

	private void CenterGrid()
	{
		_inventory.GetGrid().transform.localPosition = Vector3.zero - new Vector3(_inventory.GetGrid().gridInfo.Width / 2f,
			_inventory.GetGrid().gridInfo.Height / 2f, 0);
	}

	public void Start() => ResizeInventoryBG();
}