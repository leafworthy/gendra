	using System.Collections.Generic;
	using UnityEngine;

	




	public interface IGriddable
	{
		void SetGridPosition(int x, int y);
	}

	public interface IItemContainer 
	{
		bool DragIn(Item draggingItem);
		bool DragOut(Item item);
		
	}

