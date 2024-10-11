using NaughtyAttributes;
using UnityEngine;

public class ItemCreator : MonoBehaviour
{
   [SerializeField]private ItemSlotInventory Inventory;

   [Button]
   public void AddRandomItem()
   {
      var item = CreateRandomItem();
      Inventory.AddItemIntoEmptySlot(item);
   }

   [Button]
   public void Add10RandomItems()
   {
      for (int i = 0; i < 10; i++)
      {
         AddRandomItem();
      }
   }

   public Item CreateRandomItem()
   {
      var item = Instantiate(Prefabs.I.ItemPrefab, transform);
      var itemComponent = item.GetComponent<Item>();
      itemComponent.SetIDAndSetupComponents(Random.Range(0, ItemData.Count));
      return itemComponent;
   }

   public Item CreateItemFromID(int id)
   {
      var item = Instantiate(Prefabs.I.ItemPrefab, transform);
      var itemComponent = item.GetComponent<Item>();
      itemComponent.SetIDAndSetupComponents(id);
      return itemComponent;
   }
}
