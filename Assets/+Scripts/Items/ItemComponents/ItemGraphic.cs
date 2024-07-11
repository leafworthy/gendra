using UnityEngine;

public class ItemGraphic : MonoBehaviour, ItemComponent
{
	private bool _isDragging;
	private Item _item => GetComponent<Item>();
	public ColorManager.ItemColor GetColor() => _item.GetData().itemColor;

	private void OnEnable()
	{
		_item.OnDragStart += StartDragging;
		_item.OnDragStop += StopDragging;
	}

	private void OnDisable()
	{
		_item.OnDragStart -= StartDragging;
		_item.OnDragStop -= StopDragging;
	}

	private void StartDragging()
	{
		_isDragging = true;
		DragDropIndicatorHighlight();
	}

	private void Update()
	{
		if (_isDragging) DragDropIndicatorHighlight();
	}

	private void StopDragging()
	{
		_isDragging = false;
		DragDropIndicatorUnHighlight();
	}

	private void DragDropIndicatorHighlight()
	{
		var pointsToTest = _item.Grid.GetWorldPositionsOfFullSpaces();
		foreach (var point in pointsToTest)
		{
			var hits = MouseManager.GetObjectsAtPosition<Slot>(point);
			if (hits.Count <= 0)
			{
				ShowSpacesThatCantDrop();
				return;
			}

			foreach (var hit in hits)
			{
				if (hit.IsUnoccupied && !hit.IsDisabled) continue;
				ShowSpacesThatCantDrop();
				return;
			}
		}

		DragDropIndicatorUnHighlight();
	}

	private void ShowSpacesThatCantDrop()
	{
		var spaces = _item.Grid.GetItemGridSpaces();
		foreach (var space in spaces)
		{
			if (space is {IsEmptySpace: true}) continue;

			var slots = MouseManager.GetObjectsAtPosition<Slot>(space.transform.position);
			if (slots.Count <= 0)
			{
				space.ShowSpaceCantDrop();
				continue;
			}

			if (!slots[0].IsUnoccupied || slots[0].IsDisabled)
			{
				space.ShowSpaceCantDrop();
				continue;
			}

			space.UnHighlightActualSpace();
		}
	}

	private void DragDropIndicatorUnHighlight()
	{
		foreach (var space in _item.Grid.GetItemGridSpaces())
		{
			space.UnHighlightActualSpace();
		}
	}

	private void DragItemBack()
	{
		_isDragging = false;
		DragDropIndicatorUnHighlight();
	}

	private SpriteRenderer _spriteRenderer => GetComponentInChildren<SpriteRenderer>();
	private void SetSpriteByID(int itemID) => _spriteRenderer.sprite = ItemData.GetSpriteByID(itemID);

	public void Setup(ItemData data) => SetSpriteByID(data.itemID);
}