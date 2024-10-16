using UnityEngine;

public class AddItemButton : MonoBehaviour
{

	public int numberOfItemsToAdd = 1;
	private ItemSlotInventory inventory => GetComponentInParent<ItemSlotInventory>();

	
	public void AddItem()
	{
		for (var i = 0; i < numberOfItemsToAdd; i++) ItemCreator.AddRandomItemToInventory(inventory);
	}

}