using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class InventorySaveData
{
	public List<ItemSaveData> savedItems = new List<ItemSaveData>();
	private ItemSlotInventory savedInventory;

	public List<ItemSaveData> SavedItems => savedItems;
	public InventorySaveData(ItemSlotInventory inventory)
	{
		savedInventory = inventory;
		SaveInventory();
	}

	private void SaveInventory()
	{
		savedItems.Clear();
		foreach (var item in savedInventory.Items)
		{
			if (item == null) continue;
			var itemSaveData = new ItemSaveData
			{
				ItemID = item.GetID(),
				ItemSlotPosition = item.GetSlot().GetGridPosition(),
				ItemDirection = item.GetDirection()
			};
			Debug.Log("saved item: " + itemSaveData.ItemID);
			savedItems.Add(itemSaveData);
		}
    
	}

}

[System.Serializable]
public class ItemSaveData
{
	public int ItemID;
	public Vector2Int ItemSlotPosition;
	public Direction ItemDirection;
}