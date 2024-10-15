using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SlotHighlight : MonoBehaviour
{
	private Slot slot => GetComponent<Slot>();
	private Item currentHovering;
	private Item currentOccupied;
	public SpriteRenderer Fill_SpriteRenderer;
	public SpriteRenderer Outline_SpriteRenderer;
	private Color originalFillColor = Color.black;
	private Color originalOutlineColor = Color.grey;
	private bool isOccupied;
	private bool isHovered;

	private void Start() => Setup();

	private void Setup()
	{
		originalFillColor = Fill_SpriteRenderer.color;
		originalOutlineColor = Outline_SpriteRenderer.color;
	}

	private void Update()
	{
		if (slot.IsDisabled)
		{
			SetDisabled();
			return;
		}

		if (slot.GetItem() != null)
		{
			SetOccupied(slot.GetItem());
			return;
		}

		if (isOccupied)
		{
			isOccupied = false;
			SetFillColor(originalFillColor);
			SetOutlineColor(originalOutlineColor);
		}

		var point = slot.GetCenter();
		var itemSpaces = MouseManager.GetObjectsAtPosition<ItemGridSpace>(point).Where(x => !x.IsEmptySpace).ToList();

		CheckIsHovered(itemSpaces);
	}

	private void SetFillColor(Color color) => Fill_SpriteRenderer.color = color;
	private void SetOutlineColor(Color color) => Outline_SpriteRenderer.color = color;

	private void SetDisabled()
	{
		SetFillColor(Color.clear);
		SetOutlineColor(Color.clear);
	}

	private void CheckIsHovered(List<ItemGridSpace> itemSpaces)
	{
		if (itemSpaces.Count <= 0)
		{
			SetUnHovered();
			return;
		}

		foreach (var space in itemSpaces)
		{
			if (space.IsEmptySpace) continue;
			if (space.GetItem().IsDragging) SetSlotHoveredBy(space.GetItem());
			return;
		}

		SetUnHovered();
	}

	private void SetHovered(Item item)
	{
		SetFillColor(ColorManager.GetColor(item.GetColor()));
	}

	private void SetUnHovered()
	{
		SetFillColor(originalFillColor);
	}

	private void SetSlotHoveredBy(Item item)
	{
		currentHovering = item;
		SetHovered(currentHovering);
	}

	public void Highlight()
	{
		if (!slot.IsUnoccupied) return;
		SetOutlineColor(Color.white);
	}

	public void UnHighlight()
	{
		if (!slot.IsUnoccupied) return;
		SetOutlineColor(originalOutlineColor);
	}

	private void SetOccupied(Item item)
	{
		if (isOccupied) return;
		isOccupied = true;
		SetFillColor(ColorManager.GetColorFromCategoryDark(item.GetColor()));
		SetOutlineColor(Color.black);
	}
}