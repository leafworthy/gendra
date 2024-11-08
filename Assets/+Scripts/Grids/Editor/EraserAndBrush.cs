using System.Linq;
using UnityEngine;

public class EraserAndBrush : Singleton<EraserAndBrush>
{
	private GameObject eraser => FindObjectOfType<Eraser>(true).gameObject;
	private GameObject brush => FindObjectOfType<Brush>(true).gameObject;

	public void Erase(Vector2 mousePosition)
	{
		HideBrush();
		eraser.SetActive(true);
		eraser.transform.position = mousePosition;
	}

	public void Brush(Vector2 mousePosition)
	{
		HideEraser();
		brush.SetActive(true);
		brush.transform.position = mousePosition;
		Debug.Log(mousePosition);
	}

	public void HideEraser()
	{
		eraser.SetActive(false);
	}

	public void HideBrush()
	{
		brush.SetActive(false);
	}
}