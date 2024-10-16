using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RightClickToShowInventory : MonoBehaviour
{
    
    public GameObject inventory;

    private void Start()
    {
       MouseManager.OnRightPress += OnMouseRightPress;
    }

    public void OnMouseRightPress()
    {
        Debug.Log("here");
        List<RightClickToShowInventory> GetObjectsAtMousePosition = MouseManager.GetObjectsAtMousePosition<RightClickToShowInventory>();
        if(GetObjectsAtMousePosition.Count <= 0)
        {
            Debug.Log("none");
            return;
        }
        if(GetObjectsAtMousePosition[0] != this)
        {
            Debug.Log("not this");
            return;
        }
        inventory.SetActive(!inventory.activeSelf);
        Debug.Log(inventory.GetComponentInParent<InventoryBackgroundResizer>());
    }

}
