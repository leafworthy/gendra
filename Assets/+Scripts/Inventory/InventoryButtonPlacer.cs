using UnityEngine;

public class InventoryButtonPlacer : MonoBehaviour, InventoryComponent
{
	private InventoryComponents components;
	public GameObject topButtons;
	public GameObject bottomButtons;

	private void RepositionButtons()
	{
		bottomButtons.transform.localPosition = new Vector3(0, -components._gridInfo.Height / 2f, 0);
		topButtons.transform.localPosition = new Vector3(components._gridInfo.Width / 2f, components._gridInfo.Height / 2f, 0);
	}

	public void Setup(InventoryComponents components)
	{
		this.components = components;
		RepositionButtons();
	}
}