using UnityEngine;

public class TableSpace:MonoBehaviour, IItemContainer
{
	public bool DragIn(Item draggingItem) => true;

	public bool DragOut(Item item) => true;
}