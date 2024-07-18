using UnityEngine;

public class Utils
{
	public static void DrawX(Vector2 pos, float size = .5f)
	{
		Debug.DrawLine(new Vector2(-size, -size) + pos, pos + new Vector2(size, size), Color.red, 1f);
		Debug.DrawLine(new Vector2(-size, size) + pos, pos + new Vector2(size, -size), Color.red, 1f);

	}
}