using UnityEngine;

public class DebugDraw
{
	public static void DrawX(Vector2 pos, float size = .5f, Color color = default)
	{
		if (color == default) color = Color.red;
		Debug.DrawLine(new Vector2(-size, -size) + pos, pos + new Vector2(size, size), color, .5f);
		Debug.DrawLine(new Vector2(-size, size) + pos, pos + new Vector2(size, -size), color, .5f);

	}
}