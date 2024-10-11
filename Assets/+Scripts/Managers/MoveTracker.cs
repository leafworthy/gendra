using System;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;

[Serializable]
public class MoveTracker : MonoBehaviour
{
    public Stack<MoveCommand> UndoCommands = new Stack<MoveCommand>();
    public Stack<MoveCommand> RedoCommands = new Stack<MoveCommand>();
    private MoveCommand newMoveCommand;
    private Vector2Int _originalPosition;
    private Direction _originalDirection;
    private ItemSlotInventory _originalInventory;

    private void Start()
    {
        ItemMover.OnItemStartDragging += OnItemStartDragging;
        ItemMover.OnItemStopDragging += OnItemStopDragging;
    }

    private void OnItemStartDragging(Item item)
    {
        _originalPosition = item.GetInventoryPosition();
        _originalDirection = item.GetRotation().GetDirection();
        _originalInventory = item.GetInventory() as ItemSlotInventory;
        Debug.Log("Start dragging from: " + _originalPosition + " Direction: " + _originalDirection + " Inventory: " + _originalInventory);
    }

    private void OnItemStopDragging(Item item)
    {
        Debug.Log("Stop dragging to: " + item.GetInventoryPosition() + " Direction: " + item.GetRotation().GetDirection() + " Inventory: " + item.GetInventory());
        
        // If the item is not moved, do not add to stack
        if (_originalPosition == item.GetInventoryPosition() && _originalDirection == item.GetRotation().GetDirection() && _originalInventory ==
            (ItemSlotInventory)item.GetInventory())
        {
            Debug.Log("Same spot, no need to add to undo stack");
            return;
        }

        // Add new command to undo stack and clear redo stack
        newMoveCommand = new MoveCommand(item, _originalPosition, _originalDirection, _originalInventory);
        UndoCommands.Push(newMoveCommand);
        RedoCommands.Clear(); // Clear the redo stack when a new action is performed
    }

    [Button]
    public void Undo()
    {
        if (UndoCommands.Count <= 0) return;
        Debug.Log("Undo");
        var command = UndoCommands.Pop();
        command.Undo();
        RedoCommands.Push(command); // After undoing, push it to the redo stack
    }

    [Button]
    public void Redo()
    {
        if (RedoCommands.Count <= 0) return;
        Debug.Log("Redo");
        var command = RedoCommands.Pop();
        command.Redo();
        UndoCommands.Push(command); // After redoing, push it back to the undo stack
    }
}