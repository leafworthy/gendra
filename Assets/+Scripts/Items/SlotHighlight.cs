using UnityEngine;

public class SlotHighlight : MonoBehaviour
{
	private enum FillState
	{
		Empty,
		Hovered,
		Occupied,
		Disabled
	}

	private enum OutlineState
	{
		Empty,
		Highlighted,
		HoveredButOccupied,
		Disabled
	}

	private FillState _FillState;
	private OutlineState _OutlineState;

	public SpriteRenderer Fill_SpriteRenderer;
	public SpriteRenderer Outline_SpriteRenderer;
	private Slot slot => GetComponent<Slot>();
	private bool _isDisabled;
	private Item _hoveringItem;
	private Item _occupyingItem => slot?.GetItem();
	private Slot GetSlot() => slot;

	private void Start()
	{
		SetFillState(FillState.Empty);
		_OutlineState = OutlineState.Empty;
		SetOutlineState(OutlineState.Empty);
	}


	private void SetFillColor(Color color) => Fill_SpriteRenderer.color = color;
	private void SetOutlineColor(Color color) => Outline_SpriteRenderer.color = color;

	private void SetOutlineState(OutlineState newState)
	{
		if (newState == _OutlineState) return;
		_OutlineState = newState;
		switch (_OutlineState)
		{
			case OutlineState.Empty:
				SetOutlineColor(Color.grey);
				break;
			case OutlineState.Highlighted:
				SetOutlineColor(Color.white);
				break;
			case OutlineState.HoveredButOccupied:
				SetOutlineColor(Color.red);
				break;
			case OutlineState.Disabled:
				SetOutlineColor(Color.clear);
				break;
		}
	}

	private void SetFillState(FillState newState)
	{
		if (newState == _FillState) return;
		_FillState = newState;
		switch (_FillState)
		{
			case FillState.Empty:
				SetFillColor(Color.black);
				break;
			case FillState.Hovered:
				if (_hoveringItem == null) return;
				SetFillColor(ColorManager.GetColor(_hoveringItem.GetColor()));
				break;
			case FillState.Occupied:
				if (GetSlot() == null) return;
				if (GetSlot().GetItem() == null) return;
				SetFillColor(ColorManager.GetColorFromCategoryDark(slot.GetItem().GetColor()));
				break;
			case FillState.Disabled:
				SetFillColor(Color.clear);
				break;
		}
	}

	public void SetHighlighted(bool isHighlighted)
	{
		if (_occupyingItem != null)
		{
			SetOutlineState(isHighlighted ? OutlineState.HoveredButOccupied : OutlineState.Empty);
		}
		else
		{
			SetOutlineState(isHighlighted ? OutlineState.Highlighted : OutlineState.Empty);
		}
	}

	public void SetHovered(Item hoveringItem)
	{
		_hoveringItem = hoveringItem;
		if (_hoveringItem == null)
		{
			SetFillState(_occupyingItem == null ? FillState.Empty : FillState.Occupied);
			
		}
		else
			SetFillState(_occupyingItem == null ? FillState.Hovered : FillState.Occupied);
	}

	
}