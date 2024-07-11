using UnityEngine;

public class ToggleInventoryButton : MonoBehaviour
{
	private ItemInventoryVisibility ItemInventoryVisibility => GetComponentInParent<ItemInventoryVisibility>();



	public void ShowInventory()
	{ 
		ItemInventoryVisibility.ShowInventory();
	}

	public void HideInventory()
	{
		ItemInventoryVisibility.HideInventory();
	}

}