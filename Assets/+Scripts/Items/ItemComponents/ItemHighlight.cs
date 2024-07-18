using UnityEngine;

public class ItemHighlight:MonoBehaviour
{
	private ItemMovement _itemMovement => GetComponent<ItemMovement>();
	private SpriteRenderer _spriteRenderer => GetComponentInChildren<SpriteRenderer>();
	private Animator _animator => GetComponentInChildren<Animator>();
	
	private bool isHighlighted;
	private bool isHoveringHighlighted;
	private static readonly int IsHighlighted_AnimatorHash = Animator.StringToHash("IsHighlighted");
	private static readonly int IsDragging_AnimatorHash = Animator.StringToHash("IsHovering");



	private void Update()
	{
		if (_itemMovement.IsDragging())
		{
			HighlightDragging();
		}
		else
		{
			UnHighlightDragging();
		}
	}

	public void Highlight()
	{
		if (isHoveringHighlighted) return;
		if (isHighlighted) return;
		isHighlighted = true;
		_animator.SetBool(IsHighlighted_AnimatorHash, true);
		_spriteRenderer.color = Color.white;
		SortingManager.ChangeLayer(_spriteRenderer, SortingManager.ItemOccupiedLayer);
		SortingManager.SetToFront(_spriteRenderer);
	}


	public void UnHighlight()
	{
		if (isHoveringHighlighted) return;
		if (!isHighlighted) return;
		isHighlighted = false;
		_animator.SetBool(IsHighlighted_AnimatorHash, false);
		_spriteRenderer.color = ColorManager.GetColorFromCategoryDark(ColorManager.ItemColor.white);
	}

	private void HighlightDragging()
	{
		if (isHoveringHighlighted) return;
		isHoveringHighlighted = true;
		_animator.SetBool(IsDragging_AnimatorHash, true);
		_spriteRenderer.color = Color.white;
		SortingManager.ChangeLayer(_spriteRenderer, SortingManager.ItemHoverLayer);
		SortingManager.SetToFront(_spriteRenderer);
	}

	private void UnHighlightDragging()
	{
		if (!isHoveringHighlighted) return;
		isHoveringHighlighted = false;
		_animator.SetBool(IsDragging_AnimatorHash, false);
		_spriteRenderer.color = ColorManager.GetColorFromCategoryDark(ColorManager.ItemColor.white);
	}

}