using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryDepth : MonoBehaviour, InventoryComponent
{
    // Start is called before the first frame update
    public int currentSortingOrder = 0;
    public bool isInFront;

    private void SwapSpriteSortingLayerName(SpriteRenderer sprite, string sortingLayerName, string otherSortingLayerName)
    {
        sprite.sortingLayerName = isInFront ?  sortingLayerName: otherSortingLayerName;
    }

    private void SwapCanvasSortingLayerName(Canvas canvas, string sortingLayerName, string otherSortingLayerName)
    {
        canvas.sortingLayerName = isInFront ? sortingLayerName : otherSortingLayerName;
    }
    public void Setup(InventoryComponents components)
    {
        return;
        var sprites = GetComponentsInChildren<SpriteRenderer>();
        foreach (var sprite in sprites)
        {
            SwapSpriteSortingLayerName(sprite, "BGInventory", "2_BGInventory");
            SwapSpriteSortingLayerName(sprite, "GridBG", "2_GridBG");
            SwapSpriteSortingLayerName(sprite, "Slots", "2_Slots");
            SwapSpriteSortingLayerName(sprite, "GridSpaces", "2_GridSpaces");
            SwapSpriteSortingLayerName(sprite, "ItemImages", "2_ItemImages");
            
        }
        var canvases = GetComponentsInChildren<Canvas>();
        foreach (var canvas in canvases)
        {
            SwapCanvasSortingLayerName(canvas, "BGInventory", "2_BGInventory");
            SwapCanvasSortingLayerName(canvas, "GridBG", "2_GridBG");
            SwapCanvasSortingLayerName(canvas, "Slots", "2_Slots");
            SwapCanvasSortingLayerName(canvas, "GridSpaces", "2_GridSpaces");
            SwapCanvasSortingLayerName(canvas, "ItemImages", "2_ItemImages");
             
        }
    }
}
