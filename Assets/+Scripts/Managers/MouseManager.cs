using System;
using System.Collections.Generic;
using UnityEngine;

public class MouseManager : MonoBehaviour
{
	private static Camera mainCamera => Camera.main;

	private static bool isPressing;
	private static bool isRightPressing;

	private MouseInteraction hoveredItem;
	private MouseInteraction draggingItem;

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
		{
			if (isPressing)
			{
				Drag();
				return ;
			}

			isPressing = true;
			Press();
		}
		else
		{
			if (!isPressing)
			{
				Hover();
				return ;
			}

			isPressing = false;
			Release();
		}

		return ;
	}

	private void TestForRightPress()
	{
		if (Input.GetMouseButton(1))
		{
			if (isRightPressing)
			{
				RightDrag();
				return ;
			}

			isRightPressing = true;
			RightPress();
		}
		else
		{
			
			isRightPressing = false;
			RightRelease();
		}
	}

	private void Drag()
	{
		if (draggingItem == null) return;
		draggingItem.OnMouseDragging();
	}

	private void RightDrag()
	{
		if (draggingItem == null) return;
		draggingItem.OnMouseRightDrag();
	}

	private void Release()
	{
		
		var hits = GetHitsAtPosition();
		Debug.Log("release here");
		OnRelease?.Invoke();
		foreach (var hit2D in hits)
		{
			var item = hit2D.collider.GetComponent<MouseInteraction>();
			if (item == null) continue;
			item.OnMouseRelease();
		}

		if (draggingItem == null) return;
		draggingItem.OnMouseRelease();
		draggingItem = null;
	}

	private void RightRelease()
	{
		
		var hits = GetHitsAtPosition();
		OnRightRelease?.Invoke();
		foreach (var hit2D in hits)
		{
			var item = hit2D.collider.GetComponent<MouseInteraction>();
			if (item == null) continue;
			item.OnMouseRightRelease();
		}

		if (draggingItem == null) return;
		draggingItem.OnMouseRightRelease();
		draggingItem = null;
	}

	private void Hover()
	{
		var interactions = GetObjectsAtMousePosition<MouseInteraction>();
		foreach (var interaction in interactions)
		{
			if (hoveredItem == interaction) return;
			if (hoveredItem != null) hoveredItem.OnMouseUnhover();

			hoveredItem = interaction;
			hoveredItem.OnMouseHover();
			return;
		}

		if (hoveredItem == null) return;
		hoveredItem.OnMouseUnhover();
		hoveredItem = null;
	}

	private void Press()
	{
		var hits = GetHitsAtPosition();
		OnPress?.Invoke();
		foreach (var hit2D in hits)
		{
			var item = hit2D.collider.GetComponent<MouseInteraction>();
			if (item == null) continue;
			draggingItem = item;

			draggingItem.OnMousePress();
			return;
		}
	}

	private void RightPress()
	{
		var interactions = GetObjectsAtMousePosition<MouseInteraction>();
		OnRightPress?.Invoke();
		foreach (var interaction in interactions)
		{
			draggingItem = interaction;
			draggingItem.OnMouseRightPress();
			return;
		}
	}

}

