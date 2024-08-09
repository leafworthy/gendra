using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;

public class StickToSide : MonoBehaviour
{
    public InventoryBackgroundResizer target;

    [Button]
    public void Stick()
    {
        var bgwidth = target.GetBGWidth();
        transform.position = target.transform.position+ new Vector3(bgwidth*.9f, 0, 0);
    }
}
