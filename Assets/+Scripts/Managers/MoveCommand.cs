using UnityEngine;

public interface IInventoryCommand
{
	public void Redo();
	public void Undo();
}
public class MoveCommand : IInventoryCommand
{

	public MoveCommand(Item item, Vector2Int originalPosition, Direction originalDirection, ItemSlotInventory originalInventory)
	{
		Item = item;

		OriginalDirection = originalDirection;
		OriginalPosition = originalPosition;
		OriginalInventory = originalInventory;
		
		NewPosition = item.GetInventoryPosition();
		NewDirection = item.GetDirection();
		NewInventory = item.GetInventory() as ItemSlotInventory;
		
	}

	public void Redo()
	{
		OriginalInventory.DragOut(Item);
		ItemMover.RotateToDirection(Item, NewDirection);
		
		ItemMover.MoveToPosition(Item, NewInventory.GetSlotFromGridPosition(NewPosition).transform.position);

		NewInventory.DragIn(Item);
	}

	public void Undo()
	{
		NewInventory.DragOut(Item);
		ItemMover.RotateToDirection(Item,OriginalDirection);
		ItemMover.MoveToPosition(Item,OriginalInventory.GetSlotFromGridPosition(OriginalPosition).transform.position);
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