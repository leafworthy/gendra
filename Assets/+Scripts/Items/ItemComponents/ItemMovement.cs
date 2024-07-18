using System;
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
	public event Action OnDragStart;
	public event Action OnDragStop;
	public event Action OnPutBack;
	public event Action<Vector2> OnSetPosition;

	public bool IsDragging() => _isDragging;


	public void SetMouseOffset(Vector2 offset) => mouseOffset = offset;

	private void Update()
	{
		if (_isDragging) _item.transform.position = MouseManager.GetMouseWorldPosition() - mouseOffset;
	}

	public void StartDragging()
	{
		_item.transform.SetParent(null);
		_isDragging = true;
		originalPosition = _item.transform.position;
		originalItemContainer = _item.ItemContainer;
		originalItemContainer?.DragOut(_item);
		mouseOffset = MouseManager.GetMouseWorldPosition() - (Vector2) _item.transform.position;
		OnDragStart?.Invoke();
	}

	public void StopDragging()
	{
		_isDragging = false;
		var inventoriesAtMousePosition = MouseManager.GetObjectsAtMousePosition<IItemContainer>();
		if (inventoriesAtMousePosition.Count <= 0)
		{
			var TableSpaceAtMousePosition = MouseManager.GetObjectsAtMousePosition<TableSpace>();
			if (TableSpaceAtMousePosition.Count > 0)
			{
				OnDragStop?.Invoke();
				Debug.Log("drag stop on table");
				TableSpaceAtMousePosition[0].DragIn(_item);
				return;
			}

			DragItemBack();
		}
		else if (!inventoriesAtMousePosition[0].DragIn(_item))
		{
			DragItemBack();
		}else{
			OnDragStop?.Invoke();
			Debug.Log("drag stop on inventory");
		}
	}

	private void DragItemBack()
	{
		_isDragging = false;
		OnPutBack?.Invoke();
		Debug.Log("put back");
		transform.position = originalPosition;
		if (originalItemContainer != null) originalItemContainer.DragIn(_item);
		OnDragStop?.Invoke();
		Debug.Log("drag stop");
	}

	public void SetPosition(Vector3 newPosition)
	{
		transform.position = (Vector2) newPosition - _item.GetComponent<ItemRotation>().GetCenterRotationOffset();
		//OnSetPosition?.Invoke(newPosition);
	}



	public bool CanDrop()
	{
		var pointsToTest = _item.Grid.GetWorldPositionsOfFullSpaces();	Debug.Break();
		bool isFirst = true;
		Debug.Break();
		foreach (var point in pointsToTest)
		{
			Debug.Break();
			if (isFirst)
			{
				isFirst = false;
				Utils.DrawX(point, 2f, Color.yellow);
			}
			var slots = MouseManager.GetObjectsAtPosition<Slot>(point);

			
			if (slots.Count <= 0)
			{
				Utils.DrawX(point, .5f, Color.red);
				Debug.Log("no slots");
				return false;
			}

			Utils.DrawX(point, .3f, Color.green);
			Debug.Break();

			if (!slots[0].IsUnoccupied || slots[0].IsDisabled)
			{
				Debug.Log("occupied or disabled slot");
				Utils.DrawX(slots[0].transform.position, 0.5f, Color.magenta);
				return false;
			}
		}

		Debug.Break();
		Debug.Log("can drop");
		return true;
	}

	
}