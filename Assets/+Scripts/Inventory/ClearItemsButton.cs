using UnityEngine;

public class ClearItemsButton : MonoBehaviour
{
	public ItemSlotInventory Inventory => GetComponentInParent<ItemSlotInventory>();

	public void ClearItems()
	{
		Inventory.ClearItems();
	}
}