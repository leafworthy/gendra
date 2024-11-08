using System.Collections.Generic;
using System.Linq;
using NaughtyAttributes;
using UnityEngine;

public interface IGriddable
{
	void SetGridPosition(int x, int y);
}
[ExecuteInEditMode]
public class Grid : MonoBehaviour
{
	private const int size = 1;
	private GameObject gridPrefab;
	public GridInfo gridInfo;
	public GridInfo GetGridInfo() => gridInfo;
	protected List<IGriddable> GetGridObjects() => gridObjects;
	private List<IGriddable> gridObjects = new();

	[Button]
	private void DestroyGrid()
	{
		var allItemSpaces = (from Transform t in transform select t.gameObject).ToList();

		foreach (var itemSpace in allItemSpaces)
		{
			itemSpace.DestroySafely();
		}
	}

	

	public virtual void MakeGrid(GridInfo info, GameObject prefab)
	{
		gridInfo = info;
		gridPrefab = prefab;
		DestroyGrid();

		for (var x = 0; x < info.Width; x++)
		{
			for (var y = 0; y < info.Height; y++)
			{
				var newTile = Instantiate(gridPrefab, (Vector2) transform.position + new Vector2(x * size, y * size), Quaternion.identity, transform);
				var newSpace = newTile.GetComponent<IGriddable>();
				if(newSpace == null) continue;
				newSpace.SetGridPosition(x, y);
				gridObjects.Add(newSpace);
			}
		}
	}
}