using System;
using UnityEngine;

public class ItemRotator : MonoBehaviour
{
	
}
public class ItemRotation : MonoBehaviour,ItemComponent
{
	public Direction GetDirection() => currentDirection;
	private Item _item => GetComponent<Item>();
	private Direction currentDirection = Direction.Up;

	public void Setup(ItemData data)
	{
		setRotationEulerAngles();
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
		if (currentDirection is Direction.Up or Direction.Down)
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

	public void RotateToDirection(Direction dir)
	{
		currentDirection = dir;
		setRotationEulerAngles();
	}

	private void setRotationEulerAngles()
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