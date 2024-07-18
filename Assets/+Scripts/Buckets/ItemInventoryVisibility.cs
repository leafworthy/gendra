using NaughtyAttributes;
using UnityEngine;

[ExecuteInEditMode]
public class ItemInventoryVisibility : MonoBehaviour
{
	private const int margin = 3;
	[SerializeField] private GameObject topButtons;
	[SerializeField] private GameObject bottomButtons;
	[SerializeField] private GameObject itemObject;
	[SerializeField] private GameObject inventoryObject;
	[SerializeField] private SpriteRenderer _inventoryBG;
	[SerializeField] private ItemSlotInventory _inventory;
	private BoxCollider2D _inventoryCollider => _inventoryBG.GetComponent<BoxCollider2D>();

	private GridInfo _gridInfo => _inventory.GetGridInfo();
	private void Awake() => Init();

	[Button]
	public void ShowInventory() => SetInventoryVisible(true);

	[Button]
	public void HideInventory() => SetInventoryVisible(false);

	private void SetInventoryVisible(bool isVisible)
	{
		inventoryObject.gameObject.SetActive(isVisible);
		itemObject.SetActive(!isVisible);
		if (!isVisible) return;
		UpdateInventoryGraphics();
	}

	private void Init()
	{
		SetEverythingVisible(true);
		UpdateInventoryGraphics();
		SetEverythingVisible(false);
		SetInventoryVisible(true);
	}

	private void SetEverythingVisible(bool isVisible)
	{
		itemObject.SetActive(isVisible);
		inventoryObject.SetActive(isVisible);
	}

	private void UpdateInventoryGraphics()
	{
		ResizeInventoryBG();
		CenterGrid();
		RepositionButtons();
	}

	private void CenterGrid()
	{
		_inventory.GetGrid().transform.localPosition = Vector3.zero - new Vector3(_gridInfo.Width / 2f, _gridInfo.Height / 2f, 0);
	}

	private void RepositionButtons()
	{
		bottomButtons.transform.localPosition = new Vector3(0, -_gridInfo.Height / 2f, 0);
		topButtons.transform.localPosition = new Vector3(_gridInfo.Width / 2f, _gridInfo.Height / 2f, 0);
	}

	private void ResizeInventoryBG()
	{
		var spriteRendererBounds = _inventoryBG.bounds.size;
		spriteRendererBounds.x = _gridInfo.Width + margin;
		spriteRendererBounds.y = _gridInfo.Height + margin;

		_inventoryBG.size = spriteRendererBounds;
		_inventoryBG.transform.localPosition = Vector3.zero;

		_inventoryCollider.size = _inventoryBG.bounds.size;
		_inventoryCollider.offset = Vector2.zero;
	}

	
}