using UnityEngine;
using UnityEngine.UI;

public class MoveInventoryButton : MonoBehaviour
{
	private InventoryMovement ItemInventoryVisibility => GetComponentInParent<InventoryMovement>();
	private Rigidbody2D _rigidbody2D ;
	public Button MoveButton;

	

	public void StartDragging()
	{
		ItemInventoryVisibility.SetDragging(true);
	}

	public void StopDragging()
	{
		ItemInventoryVisibility.SetDragging(false);
	}

}