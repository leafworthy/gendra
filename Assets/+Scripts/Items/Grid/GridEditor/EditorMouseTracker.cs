using UnityEditor;
using UnityEngine;

[ExecuteAlways]
public class EditorMouseTracker:EditorWindow
{
	[MenuItem("Tools/Get Mouse Position #t")]
	public static void GetMousePosition()
	{
		SceneView.onSceneGUIDelegate += OnSceneGUI;
	}
	private static Vector3 currentMousePosition;

	private static bool brushing;
	private static bool erasing;
	private static void OnSceneGUI(SceneView sceneView)
	{
		var e = Event.current;
		if (e.type == EventType.KeyDown)
		{
			if (e != null && e.keyCode != KeyCode.None)
				Debug.Log("Key pressed in editor: " + e.keyCode);
			switch (e.keyCode)
			{
				case KeyCode.A:
					BrushStart();
					break;
				case KeyCode.E:
					EraseStart();
					break;
			}
		}

		if (e.type == EventType.KeyUp)
		{
			if (e != null && e.keyCode != KeyCode.None)
				Debug.Log("Key pressed in editor: " + e.keyCode);
			switch (e.keyCode)
			{
				case KeyCode.A:
					BrushStop();
					break;
				case KeyCode.E:
					EraseStop();
					break;
			}
		}
		
		
		
		Vector3 distanceFromCam = new Vector3(Camera.main.transform.position.x, Camera.main.transform.position.y, 0);
		Plane plane = new Plane(Vector3.forward, distanceFromCam);

		Ray ray = HandleUtility.GUIPointToWorldRay(Event.current.mousePosition);
		float enter = 0.0f;

		if (plane.Raycast(ray, out enter))
		{
			//Get the point that is clicked
			currentMousePosition = ray.GetPoint(enter);
		}

		if (brushing) EraserAndBrush.I.Brush(currentMousePosition);
		if (erasing) EraserAndBrush.I.Erase(currentMousePosition);
	}

	private static void EraseStop()
	{
		 erasing = false;
		 EraserAndBrush.I.HideEraser();
	}

	private static void BrushStop()
	{ 
		brushing = false;
		EraserAndBrush.I.HideBrush();
	}

	[MenuItem("MyMenu/Brush")]
	public static void BrushStart()
	{
		erasing = false;
		EraserAndBrush.I.Brush(currentMousePosition);
		brushing = true;
	}

	[MenuItem("MyMenu/Erase")]
	public static void EraseStart()
	{ 
		brushing = false;
		EraserAndBrush.I.Erase(currentMousePosition); 
		erasing = true;
	}
}