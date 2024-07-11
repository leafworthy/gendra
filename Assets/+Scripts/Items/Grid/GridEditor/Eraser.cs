using UnityEngine;

	[ExecuteInEditMode]
	public class Eraser : MonoBehaviour
	{
		[Range(0, 3)] public float eraserSize = 0.5f;

		private void Update()
		{
			var squareCast = Physics2D.CircleCastAll(transform.position, eraserSize, Vector2.zero);
			if (squareCast.Length > 0)
			{
				foreach (var hit2D in squareCast)
				{
					var stagingItemSpace = hit2D.collider.gameObject.GetComponent<ItemGridSpace>();
					if (stagingItemSpace != null) stagingItemSpace.IsEmptySpace = true;
				}
			}
		}

		private void OnDrawGizmos()
		{
			Gizmos.color = Color.black;
			Gizmos.DrawWireSphere(this.transform.position, eraserSize);
		}
	}
