using UnityEngine;

public enum Direction
{
	Up,
	Right,
	Down,
	Left
}

public class ItemMovement : MonoBehaviour
{
	private Vector2 originalPosition;
	private Vector2 mouseOffset;
	private Item _item => GetComponent<Item>();
	private IItemContainer originalItemContainer;
	private IItemContainer currentItemContainer;
	private bool _isDragging;
	private Direction _item_originalDirection;

	public bool IsDragging() => _isDragging;

	private void OnEnable()
	{
		_item.OnDragStart += StartDraggingItem;
		_item.OnDragStop += DragItemStop;
	}
	 
 	private void OnDisable()
	{
		_item.OnDragStart -= StartDraggingItem;
		_item.OnDragStop -= DragItemStop;
	}
	public void SetMouseOffset(Vector2 offset) => mouseOffset = offset;

	private void Update()
	{
		if (_isDragging) _item.transform.position = ( MouseManager.GetMouseWorldPosition() - mouseOffset);
	}

	private void StartDraggingItem()
	{
		_isDragging = true;
		originalPosition = _item.transform.position;
		originalItemContainer = _item.GetInventory();
		originalItemContainer?.DragOut(_item);
		mouseOffset = MouseManager.GetMouseWorldPosition() - (Vector2) _item.transform.position;
	}

	private void DragItemStop()
	{
		_isDragging = false;
		var inventoriesAtMousePosition = MouseManager.GetObjectsAtMousePosition<IItemContainer>();
		if (inventoriesAtMousePosition.Count <= 0)
		{
			Debug.Log("drag back here");
			var TableSpaceAtMousePosition = MouseManager.GetObjectsAtMousePosition<TableSpace>();
			if (TableSpaceAtMousePosition.Count > 0)
			{
				TableSpaceAtMousePosition[0].DragIn(_item);
				Debug.Log("dropped on tablespace");
				return;
			}
			DragItemBack();
		}
		else if (!inventoriesAtMousePosition[0].DragIn(_item))
		{
			Debug.Log("drag back hererhrhr");
			DragItemBack();
		}
	}

	private void DragItemBack()
	{
		_isDragging = false;
		_item.GetRotation().RotateItemBack();
		transform.position = originalPosition;
		if (originalItemContainer != null) originalItemContainer.DragIn(_item);
	}

	public void SetPositionMinusRotationOffset(Vector3 newPosition)
	{
		transform.position = (Vector2) newPosition - _item.GetRotation().GetRotationOffset();
	}

	public Vector2 GetBottomLeftPosition()
	{
		var itemSpaceGrid = _item.Grid;
		return _item.GetRotation().GetDirection() switch
		       {
			       Direction.Up => itemSpaceGrid.GetSpaceByGridPosition(0, 0).transform.position,
			       Direction.Left => itemSpaceGrid.GetSpaceByGridPosition(0, itemSpaceGrid.GetGridInfo().Height - 1).transform.position,
			       Direction.Down => itemSpaceGrid.GetSpaceByGridPosition(itemSpaceGrid.GetGridInfo().Width - 1,
				       itemSpaceGrid.GetGridInfo().Height - 1).transform.position,
			       Direction.Right => itemSpaceGrid.GetSpaceByGridPosition(itemSpaceGrid.GetGridInfo().Width - 1, 0).transform.position,
			       _ => Vector2.zero
		       };
	}

	public bool CanDrop()
	{
		var pointsToTest = _item.Grid.GetWorldPositionsOfFullSpaces();
		foreach (var point in pointsToTest)
		{
			var slots = MouseManager.GetObjectsAtPosition<Slot>(point);

			Debug.DrawLine(point, point + Vector2.one, Color.red, 1f);
			if (slots.Count <= 0)
			{
				Debug.Log("no slots");
				return false;
			}

			if (!slots[0].IsUnoccupied || slots[0].IsDisabled)
			{
				Debug.Log("slot unoccupied" + slots[0].IsUnoccupied + " disabled" + slots[0].IsDisabled);
				return false;
			}
		}

		return true;
	}
}