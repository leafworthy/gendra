using UnityEngine;

public class InventoryMovement : MonoBehaviour
{
	private Vector3 _mouseOffset;
	private bool _isDragging;
	private Rigidbody2D _rb => GetComponent<Rigidbody2D>();

	public void SetDragging(bool isDragging)
	{
		_isDragging = isDragging;
		_rb.isKinematic = !isDragging;
		if (!isDragging) return;
		_mouseOffset = (Vector2) transform.position - MouseManager.GetMouseWorldPosition();
	}

	private void Update()
	{
		if(!_isDragging) return;
		_rb.MovePosition(MouseManager.GetMouseWorldPosition() + (Vector2)_mouseOffset);
	}
}