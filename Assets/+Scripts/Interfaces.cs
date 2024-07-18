	using UnityEngine;

	


	public interface ItemComponent
	{
		void Setup(ItemData data);
	}

	public interface IGriddable
	{
		void SetGridPosition(int x, int y);
	}

	public interface IItemContainer 
	{
		bool DragIn(Item draggingItem);
		bool DragOut(Item item);
		
	}

	public interface IDraggable
	{
		void DragTo(Vector2 pos);
		void StartDragging();
		void StopDragging();
		Vector2 GetPosition();
	}
