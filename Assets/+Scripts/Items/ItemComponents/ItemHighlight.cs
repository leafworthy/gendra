using UnityEngine;

public class ItemHighlight:MonoBehaviour, ItemComponent
{
	private Item _item => GetComponent<Item>();
	private SpriteRenderer _spriteRenderer => GetComponentInChildren<SpriteRenderer>();
	
	private bool isHighlighted;
	private bool isHoveringHighlighted;
	private Animator _animator => GetComponentInChildren<Animator>();
	private static readonly int BirthTrigger = Animator.StringToHash("BirthTrigger");
	private static readonly int IsHighlighted_AnimatorHash = Animator.StringToHash("IsHighlighted");
	private static readonly int IsHovering = Animator.StringToHash("IsHovering");

	public void Setup(ItemData data)
	{
	}

	private void Update()
	{
		if (_item.IsHovering())
		{
			HighlightHovering();
		}
		else
		{
			UnHighlightHovering();
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

	private void HighlightHovering()
	{
		if (isHoveringHighlighted) return;
		isHoveringHighlighted = true;
		_animator.SetBool(IsHovering, true);
		_spriteRenderer.color = Color.white;
		SortingManager.ChangeLayer(_spriteRenderer, SortingManager.ItemHoverLayer);
		SortingManager.SetToFront(_spriteRenderer);
	}

	private void UnHighlightHovering()
	{
		if (!isHoveringHighlighted) return;
		isHoveringHighlighted = false;
		_animator.SetBool(IsHovering, false);
		_spriteRenderer.color = ColorManager.GetColorFromCategoryDark(ColorManager.ItemColor.white);
	}

	
}