using UnityEditor;
using UnityEngine;

[InitializeOnLoad]
public static class EditorHotkeysTracker
{
	private static bool commandIsPressed;

	static EditorHotkeysTracker()
	{
		SceneView.duringSceneGui += view =>
		{
			var e = Event.current;
			if (Event.current.command) return;
			var mousePosition = GUIUtility.GUIToScreenPoint(Event.current.mousePosition);
			
			if (e.type == EventType.KeyDown)
			{
				var itemGridDesigner = Object.FindObjectOfType<ItemGridDesigner>();

				if (e != null && e.keyCode != KeyCode.None)
					Debug.Log("Key pressed in editor: " + e.keyCode);
				switch (e.keyCode)
				{
				
					case KeyCode.W:
						itemGridDesigner.NextItemDataButtonOnClick();
						break;
					case KeyCode.S:
						itemGridDesigner.PreviousItemDataButtonOnClick();
						break;
					case KeyCode.Q:
						itemGridDesigner.CreateNewData();
						break;
					case KeyCode.K:
						itemGridDesigner.AddColumn();
						break;
					case KeyCode.H:
						itemGridDesigner.RemoveColumn();
						break;
					case KeyCode.U:
						itemGridDesigner.AddRow();
						break;
					case KeyCode.J:
						itemGridDesigner.RemoveRow();
						break;
				}
			}

			
		};
	}
}