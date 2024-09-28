using UnityEngine;

public class ItemGridSpace : MonoBehaviour, IGriddable
{
	private Vector2Int _spaceGridPos;
	private Item item => GetComponentInParent<Item>();
	public bool IsEmptySpace;
	public Vector2Int GetGridPos() => _spaceGridPos;

	private SpriteRenderer _spriteRenderer => GetComponentInChildren<SpriteRenderer>();

	public void SetGridPosition(int x, int y)
	{
		_spaceGridPos = new Vector2Int(x, y);
	}

	public Vector2Int GetGridPosition() => _spaceGridPos;

	public Item GetItem() => IsEmptySpace ? null : item;

	public void ShowSpaceCantDrop() => _spriteRenderer.color = ColorManager.GetColor(ColorManager.ItemColor.red);

	public void UnHighlightActualSpace() => _spriteRenderer.color = Color.clear;
}