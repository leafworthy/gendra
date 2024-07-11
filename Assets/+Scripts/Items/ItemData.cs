using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu]
public class ItemData : ScriptableObject
{
	public int itemID;
	public GridInfo spaceGridInfo;
	public ColorManager.ItemColor itemColor;
	private static List<Sprite> itemSpriteList => Resources.LoadAll<Sprite>(imageFilePath).ToList();
	private const string imageFilePath = "ItemImages";

	
	public void SetToDefault()
	{
		spaceGridInfo = new GridInfo(1, 1, new List<Vector2Int>());
		itemColor = ColorManager.ItemColor.blue;
	}

	public void Copy(ItemData newData)
	{
		spaceGridInfo = new GridInfo( newData.spaceGridInfo.Width, newData.spaceGridInfo.Height, newData.spaceGridInfo.EmptyCells);
		Debug.Log("setting width from " + spaceGridInfo.Width + " to " + newData.spaceGridInfo.Width);
		spaceGridInfo.Width = newData.spaceGridInfo.Width;
		Debug.Log("setting height from " + spaceGridInfo.Height + " to " + newData.spaceGridInfo.Height);
		spaceGridInfo.Height = newData.spaceGridInfo.Height;
		spaceGridInfo.EmptyCells = newData.spaceGridInfo.EmptyCells;
		itemID = newData.itemID;
	}

	private static List<ItemData> _itemDataList => Resources.LoadAll<ItemData>(nameof(ItemData)).ToList();
	public static int Count => _itemDataList.Count;

	public static ItemData GetItemData(int itemID)
	{
		return _itemDataList.FirstOrDefault(itemData => itemData.itemID == itemID);
	}

	public static Sprite GetSpriteByID(int itemDataItemID)
	{
		Debug.Log(itemDataItemID.ToString("00000"));
		return itemSpriteList.FirstOrDefault(sprite => sprite.name == itemDataItemID.ToString("00000"));
	}
}