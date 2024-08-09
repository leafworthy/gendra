using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RightClickToShowInventory : MouseInteraction
{
    
    public GameObject inventory;
    public override void OnMouseRightPress()
    {
        inventory.SetActive(!inventory.activeSelf);
        Debug.Log(GetComponentInParent<InventoryBackgroundResizer>());
    }

    public override void OnMousePress()
    {
        inventory.SetActive(false);
        Debug.Log(GetComponentInParent<InventoryBackgroundResizer>());
    }
}
