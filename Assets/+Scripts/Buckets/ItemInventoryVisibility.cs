using NaughtyAttributes;
using UnityEngine;


[ExecuteInEditMode]
public class ItemInventoryVisibility : MonoBehaviour
{
	[SerializeField] private GameObject itemObject;
	[SerializeField] private GameObject inventoryObject;

	private Item _item => itemObject.GetComponent<Item>();
	private void Start() => Init();

	[Button]
	public void ShowInventory() => SetInventoryVisible(true);

	[Button]
	public void HideInventory() => SetInventoryVisible(false);

	private void SetInventoryVisible(bool isVisible)
	{
		inventoryObject.gameObject.SetActive(isVisible);
		itemObject.SetActive(!isVisible);
	}

	private void Init()
	{
		SetEverythingVisible(true);
		_item.Setup();
		SetEverythingVisible(false);
		SetInventoryVisible(true);
	}

	private void SetEverythingVisible(bool isVisible)
	{
		itemObject.SetActive(isVisible);
		inventoryObject.SetActive(isVisible);
	}

	


	
}