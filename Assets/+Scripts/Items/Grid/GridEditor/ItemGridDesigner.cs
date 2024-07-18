using NaughtyAttributes;
using UnityEditor;
using UnityEngine;

[ExecuteInEditMode]
public class ItemGridDesigner : MonoBehaviour
{
	[SerializeField] private ItemData unsavedItemData;
	[SerializeField] private int dataIndex;
	private Item item => GetComponentInChildren<Item>();

	private static string AssetsDataResourcesItemdataItemID => "Assets/Data/Resources/ItemData/Item_ID_";

	private void OnEnable()
	{
		SetCurrentItemData(dataIndex);
	} 

	private void SetCurrentItemData(int itemDataID)
	{
		dataIndex = itemDataID;

		item.SetIDAndSetupComponents(itemDataID);
		item.SetInventory(null);
		MakeNewUnsavedDataAndCopyCurrentItem();
	}

	private void MakeNewUnsavedDataAndCopyCurrentItem()
	{
		unsavedItemData = ScriptableObject.CreateInstance<ItemData>();
		unsavedItemData.Copy(item.GetData());
	}

	#region Buttons

	[Button("#####CREATE NEW DATA######")]
	public void CreateNewData()
	{
		for (var i = 0; i < 10; i++)
		{
			var newItemData = ScriptableObject.CreateInstance<ItemData>();
			newItemData.SetToDefault();
			newItemData.itemID = ItemData.Count;
			AssetDatabase.CreateAsset(newItemData,
				AssetsDataResourcesItemdataItemID + ItemData.Count.ToString("000") + ".asset");
			AssetDatabase.SaveAssets();
			AssetDatabase.Refresh();
			item.SetIDAndSetupComponents(newItemData.itemID);
			SetCurrentItemData(newItemData.itemID);
		}
	}

	[Button("NEXT------>>")]
	public void NextItemDataButtonOnClick()
	{
		//SaveGridOnClick();
		if (dataIndex == ItemData.Count - 1) dataIndex = 0;
		else dataIndex++;
		SetCurrentItemData(dataIndex);
	}

	[Button("<<------PREV")]
	public void PreviousItemDataButtonOnClick()
	{
		//SaveGridOnClick();
		if (dataIndex == 0) dataIndex = ItemData.Count - 1;
		else dataIndex--;
		SetCurrentItemData(dataIndex);
	}

	[Button("^^^^^^^^")]
	public void AddRow()
	{
	//unsavedItemData.spaceGridInfo.Height++;
		item.SetIDAndSetupComponents(unsavedItemData.itemID);
	}

	[Button(">>>>>>>")]
	public void AddColumn()
	{
		//unsavedItemData.spaceGridInfo.Width++;
		item.SetIDAndSetupComponents(unsavedItemData.itemID);
	}

	[Button("VVVVVVV")]
	public void RemoveRow()
	{
		//unsavedItemData.spaceGridInfo.Height--;
		item.SetIDAndSetupComponents(unsavedItemData.itemID);
	}

	[Button("<<<<<<<")]
	public void RemoveColumn()
	{
		//unsavedItemData.spaceGridInfo.Width--;
		item.SetIDAndSetupComponents(unsavedItemData.itemID);
	}

	[Button("###SAVE###")]
	private void SaveGridOnClick()
	{
		MakeNewUnsavedDataAndCopyCurrentItem();
		unsavedItemData.spaceGridInfo.EmptyCells = item.Grid.GetEmptyGridSpaces();
		var itemData = ItemData.GetItemData(dataIndex);
		if (itemData != null)
		{
			itemData.Copy(unsavedItemData);
			unsavedItemData.Copy(itemData);
			EditorUtility.SetDirty(itemData);
			EditorUtility.SetDirty(unsavedItemData);
		}
		else
		{
			unsavedItemData = ScriptableObject.CreateInstance<ItemData>();
			unsavedItemData.SetToDefault();
			EditorUtility.SetDirty(unsavedItemData);
		}

		AssetDatabase.SaveAssets();
	}

	[Button]
	public void GoToCurrentItem()
	{
		SetCurrentItemData(dataIndex);
	}

	#endregion
}