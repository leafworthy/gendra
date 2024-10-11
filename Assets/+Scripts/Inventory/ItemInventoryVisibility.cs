using NaughtyAttributes;
using UnityEngine;


[ExecuteInEditMode]
public class ItemInventoryVisibility : MonoBehaviour
{


	
	[SerializeField] private GameObject inventoryObject;
	private Rigidbody2D rb => inventoryObject.GetComponent<Rigidbody2D>();

	[SerializeField] private Item _item;
	private void Start() => Init();

	[Button]
	public void ShowInventory() => SetInventoryVisible(true);

	[Button]
	public void HideInventory() => SetInventoryVisible(false);

	private void SetInventoryVisible(bool isVisible)
	{
		inventoryObject.gameObject.SetActive(isVisible);
		rb.isKinematic = !isVisible;
		if (!isVisible) return;
		var target = GetComponentInParent<InventoryBackgroundResizer>();
		if (target == null) return;
		var bgwidth = GetComponentInParent<InventoryBackgroundResizer>().GetBGWidth();
		inventoryObject.transform.position = target.transform.position + new Vector3(bgwidth * .9f, 0, 0);
	}

	private void Init()
	{
		SetEverythingVisible(true);
		_item.Setup();
		SetEverythingVisible(false);
		SetInventoryVisible(false);
	}

	private void SetEverythingVisible(bool isVisible)
	{
		inventoryObject.SetActive(isVisible);
	}

	


	
}