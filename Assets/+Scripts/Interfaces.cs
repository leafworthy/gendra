	using UnityEngine;



	public interface InventoryComponent
	{
		public void Setup(InventoryComponents components);

	}

	public interface ItemComponent
	{
		void Setup(ItemData data);
	}

	public interface IGriddable
	{
		void SetGridPosition(int x, int y);
		Vector2Int GetGridPosition();
	}

	public interface IItemContainer 
	{
		bool DragIn(Item draggingItem);
		bool DragOut(Item item);
		
	}

