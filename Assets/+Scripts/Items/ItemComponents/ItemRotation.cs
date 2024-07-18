using System;
using UnityEngine;

public class ItemRotation : MonoBehaviour
{
	private Item _item => GetComponent<Item>();
	private ItemMovement _itemMovement => _item.GetComponent<ItemMovement>();
	
	private Direction currentDirection = Direction.Up;
	private Direction _originalDirection;

	private void OnEnable()
	{
		_itemMovement.OnDragStart += StartDraggingItem;
		_itemMovement.OnPutBack += RotateItemBack;
		setRotation();
	}

	private void OnDisable()
	{
		_itemMovement.OnDragStart -= StartDraggingItem;
		_itemMovement.OnPutBack -= RotateItemBack;
	}

	

	private void RotateItemBack()
	{
		RotateToDirection(_originalDirection);
	}

	private void StartDraggingItem()
	{
		_originalDirection = currentDirection;
	}

	public Vector2 GetRotationOffset()
	{
		var gridInfo = _item.Grid.GetGridInfo();
		Vector2 rotationOffset;
		switch (currentDirection)
		{
			case Direction.Up:
				rotationOffset = Vector3.zero;
				break;
			case Direction.Left:
				rotationOffset = Vector3.left * gridInfo.Height;
				break;
			case Direction.Down:
				rotationOffset = new Vector3(-gridInfo.Width, -gridInfo.Height, 0);
				break;
			case Direction.Right:
				rotationOffset = new Vector2(0, -gridInfo.Width);
				break;
			default:
				rotationOffset = Vector2.zero;
				break;
		}

		return rotationOffset;
	}

	public Vector2 GetCenterRotationOffset()
	{
		var gridInfo = _item.Grid.GetGridInfo();
		if (gridInfo == null) return Vector2.zero;
		Debug.Log("getting center for: " + currentDirection + " " + gridInfo.GetCenterOffset() + " " + GetRotationOffset());
		if (currentDirection == Direction.Up || currentDirection == Direction.Down)
			return gridInfo.GetCenterOffset() + GetRotationOffset();
		return gridInfo.GetReverseCenterOffset() + GetRotationOffset();
	}

	public void RotateItemCounterClockwise()
	{
		_item.transform.rotation = Quaternion.Euler(0, 0, _item.transform.rotation.eulerAngles.z + 90);
		switch (currentDirection)
		{
			case Direction.Up:
				RotateToDirection(Direction.Left);
				break;
			case Direction.Left:
				RotateToDirection(Direction.Down);
				break;
			case Direction.Down:
				RotateToDirection(Direction.Right);
				break;
			case Direction.Right:
				RotateToDirection(Direction.Up);
				break;
			default:
				throw new ArgumentOutOfRangeException();
		}
	}

	private void RotateToDirection(Direction dir)
	{
		currentDirection = dir;
		setRotation();
		_itemMovement.SetMouseOffset(GetCenterRotationOffset());
	}

	private void setRotation()
	{
		switch (currentDirection)
		{
			case Direction.Up:
				_item.transform.rotation = Quaternion.Euler(0, 0, 0);
				break;
			case Direction.Left:
				_item.transform.rotation = Quaternion.Euler(0, 0, 90);
				break;
			case Direction.Down:
				_item.transform.rotation = Quaternion.Euler(0, 0, 180);
				break;
			case Direction.Right:
				_item.transform.rotation = Quaternion.Euler(0, 0, 270);
				break;
		}
	}


	public Vector2 GetBottomLeftPosition()
	{
		var itemSpaceGrid = _item.Grid;
		return currentDirection switch
		       {
			       Direction.Up => itemSpaceGrid.GetSpaceByGridPosition(0, 0).transform.position,
			       Direction.Left => itemSpaceGrid.GetSpaceByGridPosition(0, itemSpaceGrid.GetGridInfo().Height - 1).transform.position,
			       Direction.Down => itemSpaceGrid.GetSpaceByGridPosition(itemSpaceGrid.GetGridInfo().Width - 1,
				       itemSpaceGrid.GetGridInfo().Height - 1).transform.position,
			       Direction.Right => itemSpaceGrid.GetSpaceByGridPosition(itemSpaceGrid.GetGridInfo().Width - 1, 0).transform.position,
			       _ => Vector2.zero
		       };
	}
}