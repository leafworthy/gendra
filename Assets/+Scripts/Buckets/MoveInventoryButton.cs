using UnityEngine;

public class MoveInventoryButton : MonoBehaviour
{
	private InventoryMovement ItemInventoryVisibility => GetComponentInParent<InventoryMovement>();
	private Rigidbody2D _rigidbody2D ;


	public void StartDragging()
	{
		ItemInventoryVisibility.SetDragging(true);
	}

	public void StopDragging()
	{
		ItemInventoryVisibility.SetDragging(false);
	}

}