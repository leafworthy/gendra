using System;
using UnityEngine;

public class ItemRotation : MonoBehaviour,ItemComponent
{
	public Direction GetDirection() => currentDirection;
	private Item _item => GetComponent<Item>();
	private Direction currentDirection = Direction.Up;
	private Direction _originalDirection;

	public void Setup(ItemData data)
	{
		setRotation();
	}

	private void OnEnable()
	{
		_item.OnDragStart += StartDraggingItem;
		_item.OnRotateCounterClockwise += RotateItemCounterClockwise;
	}

	private void OnDisable()
	{
		_item.OnDragStart -= StartDraggingItem;
		_item.OnRotateCounterClockwise -= RotateItemCounterClockwise;
	}

	public void RotateItemBack()
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
		 if(gridInfo == null) return Vector2.zero;
		if (currentDirection == Direction.Up || currentDirection == Direction.Down)
			return gridInfo.GetCenterOffset() + GetRotationOffset();
		return gridInfo.GetReverseCenterOffset() + GetRotationOffset();
	}

	private void RotateItemCounterClockwise()
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

	public void RotateToDirection(Direction dir)
	{
		currentDirection = dir;
		setRotation();
		_item.GetMovement().SetMouseOffset(GetCenterRotationOffset());
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

	
}