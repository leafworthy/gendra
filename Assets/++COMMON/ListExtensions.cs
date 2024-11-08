using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class ListExtensions
{
	public static T GetInterface<T>(this GameObject inObj) where T : class => inObj.GetComponents<Component>().OfType<T>().FirstOrDefault();

	public static IEnumerable<T> GetInterfaces<T>(this GameObject inObj) where T : class => inObj.GetComponents<Component>().OfType<T>();

	public static Transform DestroyAllChildren(this Transform transform)
	{
		foreach (Transform child in transform)
		{
			Object.Destroy(child.gameObject);
		}

		return transform;
	}

	public static List<T> Shuffle<T>(this List<T> list)
	{
		for (var i = 0; i < list.Count; i++)
		{
			Debug.Log("shuffling" + i);
			var temp = list[i];
			var randomIndex = Random.Range(i, list.Count);
			list[i] = list[randomIndex];
			list[randomIndex] = temp;
		}

		return list;
	}

	public static List<T> ClearAllNulls<T>(this List<T> list)
	{
		for (var i = list.Count - 1; i > -1; i--)
		{
			if (list[i] == null)
				list.RemoveAt(i);
		}

		return list;
	}

	public static void DestroySafely(this Object obj)
	{
		if (Application.isPlaying)
			Object.Destroy(obj);
		else
			Object.DestroyImmediate(obj);
	}

	public static T GetRandom<T>(this List<T> list) where T : Object => list.Count == 0 ? null : list[Random.Range(0, list.Count)];

	public static void RemoveNullEntries<T>(this IList<T> list) where T : class
	{
		for (var i = list.Count - 1; i >= 0; i--)
		{
			if (Equals(list[i], null))
				list.RemoveAt(i);
		}
	}

	public static void RemoveDefaultValues<T>(this IList<T> list)
	{
		for (var i = list.Count - 1; i >= 0; i--)
		{
			if (Equals(default(T), list[i]))
				list.RemoveAt(i);
		}
	}
}