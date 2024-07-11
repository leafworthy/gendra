
	using UnityEngine;

	public static class SortingManager
	{
		public static string ItemOccupiedLayer = "ItemImages";
		public static string ItemHoverLayer = "ItemHovering";
		private static int highestSortingOrder =1;

		
		public static void ChangeLayer(SpriteRenderer rendererToChange, string newLayer)
		{
			rendererToChange.sortingLayerID = SortingLayer.NameToID(newLayer);
		
		}

		public static void SetToFront(SpriteRenderer rendererToChange)
		{
			highestSortingOrder++;
			rendererToChange.sortingOrder = highestSortingOrder + 1;

		}
	}
