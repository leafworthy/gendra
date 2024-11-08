using System;
using System.Collections.Generic;
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
	
	private static RaycastHit2D[] GetHitsAtPosition() => Physics2D.LinecastAll(GetMouseWorldPosition(), GetMouseWorldPosition());
	public static Vector2 GetMouseWorldPosition() => mainCamera.ScreenToWorldPoint(Input.mousePosition);

	public static List<T> GetObjectsAtMousePosition<T>() => GetObjectsAtPosition<T>(GetMouseWorldPosition());

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

	private void Update()
	{
		TestForPress();
		TestForRightPress();
	}

	private void TestForPress()
	{
		if (Input.GetMouseButton(0))
			Press();
		else
		{
			isPressing = false;
			Release();
		}
	}

	private void TestForRightPress()
	{
		if (Input.GetMouseButton(1))
			RightPress();
		else
		{
			isRightPressing = false;
			RightRelease();
		}
	}

	private void Release() => OnRelease?.Invoke();

	private void RightRelease() => OnRightRelease?.Invoke();

	private void Press()
	{
		if (isPressing) return;
		isPressing = true;
		OnPress?.Invoke();
	}

	private void RightPress()
	{
		if (isRightPressing) return;
		isRightPressing = true;
		OnRightPress?.Invoke();
	}
}