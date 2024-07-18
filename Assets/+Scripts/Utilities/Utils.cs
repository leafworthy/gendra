using UnityEngine;

public class Utils
{
	public static void DrawX(Vector2 pos, float size, Color color)
	{
		var topLeft = new Vector2(pos.x - size, pos.y - size);
		var topRight = new Vector2(pos.x + size, pos.y - size);
		var bottomLeft = new Vector2(pos.x - size, pos.y + size);
		var bottomRight = new Vector2(pos.x + size, pos.y + size);
		Debug.DrawLine(topLeft, bottomRight, color, .5f);
		Debug.DrawLine(topRight, bottomLeft, color, .5f);
	}
}