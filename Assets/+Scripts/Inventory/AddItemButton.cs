using UnityEngine;

public class AddItemButton : MonoBehaviour
{
	public ItemCreator itemCreator => GetComponentInParent<ItemCreator>();
	public int numberOfItemsToAdd = 1;

	
	public void AddItem()
	{
		for (var i = 0; i < numberOfItemsToAdd; i++) itemCreator.AddRandomItem();
	}

}