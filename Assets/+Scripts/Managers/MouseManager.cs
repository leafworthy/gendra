using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class MouseManager : MonoBehaviour
{
	private static Camera mainCamera => Camera.main;

	private static bool isPressing;
	private static bool isRightPressing;
	
	public static event Action OnPress;
	public static event Action OnRelease;
	public static event Action OnRightRelease;
	public static event Action OnRightPress;

	public static Vector2 GetMouseWorldPosition()
	{
		return mainCamera.ScreenToWorldPoint(Input.mousePosition);
	}

	public static List<T> GetObjectsAtMousePosition<T>()
	{
		return GetObjectsAtPosition<T>(GetMouseWorldPosition());
	}

	public static List<T> GetObjectsAtPosition<T>(Vector2 position)
	{
		var cast = Physics2D.LinecastAll(position, position);
		var list = new List<T>();
		foreach (var hit in cast)
		{
			 var components = hit.collider.GetComponents<T>();
			 if (components.Length <= 0) continue;
			 list.AddRange(components);
		}

		return list;
	}

	private static string ListString<T>(List<T> list)
	{
		var s = "";
		foreach (var item in list)
		{
			s += item + ", ";
		}

		return s;
	}

	private void Update()
	{
		CheckForPress();
		CheckForRightPress();
	}

	private void CheckForPress()
	{
		if (Input.GetMouseButton(0))
		{
			if (isPressing) return;
			isPressing = true;

			OnPress?.Invoke();
		}
		else
		{
			if (!isPressing) return;
			isPressing = false;
			OnRelease?.Invoke();
		}
	}

	private void CheckForRightPress()
	{
		if (Input.GetMouseButton(1))
		{
			if (isRightPressing) return;
			isRightPressing = true;
			OnRightPress?.Invoke();
		}
		else
		{
			if (!isRightPressing) return;
			isRightPressing = false;

			OnRightRelease?.Invoke();
		}
	}


}