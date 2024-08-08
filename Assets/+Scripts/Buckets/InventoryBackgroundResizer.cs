using NaughtyAttributes;
using UnityEngine;

public class InventoryBackgroundResizer : MonoBehaviour, InventoryComponent
{
	[SerializeField] private SpriteRenderer _inventoryBG;
	private InventoryComponents components;
	private BoxCollider2D _inventoryCollider => _inventoryBG.GetComponent<BoxCollider2D>();

	private void ResizeInventoryBG()
	{
		var spriteRendererBounds = _inventoryBG.bounds.size;
		spriteRendererBounds.x = components._gridInfo.Width + 3;
		spriteRendererBounds.y = components._gridInfo.Height + 3;

		_inventoryBG.size = spriteRendererBounds;
		_inventoryBG.transform.localPosition = Vector3.zero;

		_inventoryCollider.size = _inventoryBG.bounds.size;
		_inventoryCollider.offset = Vector2.zero;
		CenterGrid();
	}

	private void CenterGrid()
	{
		components._inventory.GetGrid().transform.localPosition = Vector3.zero - new Vector3(components._gridInfo.Width / 2f, components._gridInfo.Height / 2f, 0);
	}

	public void Setup(InventoryComponents components)
	{
		this.components = components;
		ResizeInventoryBG();
	}
}