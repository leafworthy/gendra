using System;
using NaughtyAttributes;
using UnityEngine;
using Random = UnityEngine.Random;

public class ItemCreator : MonoBehaviour
{

   public static void AddRandomItemToInventory(ItemSlotInventory inventory)
   {
      var item = CreateRandomItem(inventory);
      if (inventory.PlaceInFirstOpenSlot(item))
      {
        
         return;
      }
      
      item.DestroyItem();
   }

   public static void Add10RandomItems(ItemSlotInventory inventory)
   {
      for (int i = 0; i < 10; i++)
      {
         AddRandomItemToInventory(inventory);
      }
   }

   public static Item CreateRandomItem(ItemSlotInventory inventory)
   {
      var item = Instantiate(Prefabs.I.ItemPrefab, inventory.transform);
      var itemComponent = item.GetComponent<Item>();
      itemComponent.SetIDAndSetupComponents(Random.Range(0, ItemData.Count));
     
      return itemComponent;
   }

   public static Item CreateItemFromID(int id, ItemSlotInventory inventory)
   {
      var item = Instantiate(Prefabs.I.ItemPrefab, inventory.transform);
      var itemComponent = item.GetComponent<Item>();
      itemComponent.SetIDAndSetupComponents(id);
      return itemComponent;
   }
}
