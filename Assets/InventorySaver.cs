using System.Collections;
using System.IO;
using UnityEngine;

public class InventorySaver : MonoBehaviour
{
    // Start is called before the first frame update
    private ItemSlotInventory _inventory => GetComponentInParent<ItemSlotInventory>();
    private ItemCreator _itemCreator => GetComponentInParent<ItemCreator>();
    private string SavedInventoryJson;


   

    public void SaveInventory()
    {
        var inventorySaveData = new InventorySaveData(_inventory);
        var json = JsonUtility.ToJson(inventorySaveData);
        SavedInventoryJson = json;
        System.IO.File.WriteAllText(Application.dataPath + "/SavedInventory.json", SavedInventoryJson);
        Debug.Log("saved as json: " + json);
#if UNITY_EDITOR
        UnityEditor.AssetDatabase.Refresh();
#endif
    }

    public void LoadInventory()
    {
       

        var saveFile = Application.dataPath + "/SavedInventory.json";
        Debug.Log("file loaded as: " + saveFile);
        string fileContents = File.ReadAllText(saveFile);
        if (string.IsNullOrEmpty(fileContents))
        {
            Debug.Log("no saved inventory");
            return;
        }
        InventorySaveData savedInventory = JsonUtility.FromJson<InventorySaveData>(fileContents);
        Debug.Log("loaded from json: " + savedInventory.SavedItems.Count);
        foreach (var itemSaveData in savedInventory.SavedItems)
        {
            AddItemFromSaveData(itemSaveData);
        }
    }

    private void AddItemFromSaveData(ItemSaveData itemSaveData)
    {
        if(itemSaveData == null)
        {
            Debug.Log("data null");
            return;
        }

        Debug.Log("adding item: " + itemSaveData.ItemID + " / " + itemSaveData.ItemSlotPosition);
        var item = _itemCreator.CreateItemFromID(itemSaveData.ItemID);
        var slot = _inventory.GetGrid().GetSlotAtGridPosition(itemSaveData.ItemSlotPosition);
        _inventory.AddItemIntoSpecificSlot(item, slot, itemSaveData.ItemDirection);
    }
}

