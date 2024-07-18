using System;
using UnityEngine;

public class Item : MonoBehaviour
{
	public IItemContainer ItemContainer { get; set; }
	[SerializeField] private int _itemID;
	private bool _isSetup;
	public ItemData GetData() => ItemData.GetItemData(_itemID);

	public event Action<ItemData> OnSetup;

	public ItemSpaceGrid Grid => GetComponentsInChildren<ItemSpaceGrid>(true)[0];

	public void Setup(int itemID)
	{
		_itemID = itemID;
		if (_isSetup) return;
		_isSetup = true;
		OnSetup?.Invoke(GetData());
	}

	public void DestroyItem()
	{
		ItemContainer?.DragOut(this);
		gameObject.DestroySafely();
	}

}