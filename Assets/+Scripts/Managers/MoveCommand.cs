using UnityEngine;

public class MoveCommand : IInventoryCommand
{

	public MoveCommand(Item item, Vector2Int originalPosition, Direction originalDirection, ItemSlotInventory originalInventory)
	{
		Item = item;

		OriginalDirection = originalDirection;
		OriginalPosition = originalPosition;
		OriginalInventory = originalInventory;
		
		NewPosition = item.GetInventoryPosition();
		NewDirection = item.GetRotation().GetDirection();
		NewInventory = item.GetInventory() as ItemSlotInventory;
		
	}

	public void Redo()
	{
		OriginalInventory.DragOut(Item);
		Item.GetRotation().RotateToDirection(NewDirection, false);
		Item.transform.position = (Vector2) NewInventory.GetSlotFromGridPosition(NewPosition).transform.position-Item.GetRotation().GetRotationOffset();

		NewInventory.DragIn(Item);
	}

	public void Undo()
	{
		NewInventory.DragOut(Item);
		Item.GetRotation().RotateToDirection(OriginalDirection, true);
		Item.transform.position = (Vector2) OriginalInventory.GetSlotFromGridPosition(OriginalPosition).transform.position -
		                          Item.GetRotation().GetRotationOffset();
		
		OriginalInventory.DragIn(Item);
	}

	public Item Item { get; set; }
	public Vector2Int OriginalPosition { get; set; }
	public Vector2Int NewPosition { get; set; }
	public Direction OriginalDirection { get; set; }
	public Direction NewDirection { get; set; }
	
	public ItemSlotInventory OriginalInventory { get; set; }
	public ItemSlotInventory NewInventory { get; set; }
}