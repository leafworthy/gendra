using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveInventoryButton : MonoBehaviour
{
    private InventorySaver _inventorySaver;
    // Start is called before the first frame update
    void Start()
    {
        _inventorySaver = GetComponentInParent<InventorySaver>();
    }

    // Update is called once per frame
    public void SaveInventory()
    {
        _inventorySaver.SaveInventory();
    }
}
