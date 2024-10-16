using UnityEngine;

public class ClearItemsButton : MonoBehaviour
{
	private ItemSlotInventory Inventory => GetComponentInParent<ItemSlotInventory>();

	public void ClearItems()
	{
		Inventory.ClearItems();
	}
}