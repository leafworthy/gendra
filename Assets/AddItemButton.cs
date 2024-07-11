using UnityEngine;

public class AddItemButton : MonoBehaviour
{
	public ItemCreator itemCreator;
	public int numberOfItemsToAdd = 1;

	private void OnEnable()
	{
		itemCreator = GetComponentInParent<ItemCreator>();
	}
	public void AddItem()
	{
		for (var i = 0; i < numberOfItemsToAdd; i++) itemCreator.AddRandomItem();
	}

}