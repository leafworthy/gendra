using System;
using UnityEngine;

public  class ItemRotator :MonoBehaviour
{
	public static Vector2 GetRotationOffset(Item item)
	{
		var gridInfo = item.GetGridInfo();
		switch (item.GetDirection())
		{
			case Direction.Up:
				return Vector3.zero;
			case Direction.Left:
				return Vector3.left * gridInfo.Height;
			case Direction.Down:
				return new Vector3(-gridInfo.Width, -gridInfo.Height, 0);
			case Direction.Right:
				return new Vector2(0, -gridInfo.Width);
			default:
				return Vector2.zero;
		}

	}

	private static void SetRotationFromDirection(Item item)
	{
		switch (item.GetDirection())
		{
			case Direction.Up:
				item.transform.rotation = Quaternion.Euler(0, 0, 0);
				break;
			case Direction.Left:
				item.transform.rotation = Quaternion.Euler(0, 0, 90);
				break;
			case Direction.Down:
				item.transform.rotation = Quaternion.Euler(0, 0, 180);
				break;
			case Direction.Right:
				item.transform.rotation = Quaternion.Euler(0, 0, 270);
				break;
		}
	}

	public static Vector2 GetCenterRotationOffset(Item item)
	{
		var gridInfo = item.GetGridInfo();
		if (gridInfo == null) return Vector2.zero;
		if (item.GetDirection() is Direction.Up or Direction.Down)
			return gridInfo.GetCenterOffset() + GetRotationOffset(item);
		return gridInfo.GetReverseCenterOffset() + GetRotationOffset(item);
	}

	public static void RotateItemCounterClockwise(Item item)
	{
		item.transform.rotation = Quaternion.Euler(0, 0, item.transform.rotation.eulerAngles.z + 90);
		switch (item.GetDirection())
		{
			case Direction.Up:
				RotateToDirection(item,Direction.Left);
				break;
			case Direction.Left:
				RotateToDirection(item, Direction.Down);
				break;
			case Direction.Down:
				RotateToDirection(item, Direction.Right);
				break;
			case Direction.Right:
				RotateToDirection(item, Direction.Up);
				break;
			default:
				throw new ArgumentOutOfRangeException();
		}
	}

	public static void RotateToDirection(Item item, Direction dir)
	{
		item.SetDirection(dir);
		SetRotationFromDirection(item);
	}
}