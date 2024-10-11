using NaughtyAttributes;
using UnityEngine;

public class InventoryComponents : MonoBehaviour
{

	public ItemSlotInventory _inventory => GetComponent<ItemSlotInventory>();
	public GridInfo _gridInfo => _inventory.GetGridInfo();

	private void Start()
	{
		_inventory.OnSetupComplete += ResetUI;
		_inventory.OnInventoryChanged += ResetupUI;
	}

	private void ResetupUI()
	{
		var components = GetComponentsInChildren<InventoryComponent>();
		foreach (var component in components)
		{
			component.Setup(this);
		}
	}

	[Button]
	private void ResetUI()
	{
		_inventory.ReSetup();
		var components = GetComponentsInChildren<InventoryComponent>();
		foreach (var component in components)	
		{
			component.Setup(this);
		}
	}

}