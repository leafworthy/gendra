using UnityEngine;

public class InventoryButtonPlacer : MonoBehaviour
{
	public GameObject topButtons;
	public GameObject bottomButtons;

	private ItemSlotInventory _inventory => GetComponent<ItemSlotInventory>();

	private void RepositionButtons()
	{
		bottomButtons.transform.localPosition = new Vector3(0, -_inventory.GetGrid().gridInfo.Height / 2f, 0);
		topButtons.transform.localPosition = new Vector3(_inventory.GetGrid().gridInfo.Width / 2f, _inventory.GetGrid().gridInfo.Height / 2f, 0);
	}

	public void Start() => RepositionButtons();
}