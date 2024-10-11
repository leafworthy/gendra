using UnityEngine;

public class MouseInteraction : MonoBehaviour
{
	public virtual void OnMouseHover()
	{
	}

	public virtual void OnMouseUnhover()
	{
	}

	public virtual void OnMousePress()
	{
		//Debug.Log("Mouse is pressing " + gameObject.name);
	}

	public virtual void OnMouseRightPress()
	{
		//Debug.Log("Mouse is pressing " + gameObject.name);
	}

	public virtual void OnMouseRightDrag()
	{
		//Debug.Log("Mouse is pressing " + gameObject.name);
	}


	public virtual void OnMouseRelease()
	{
		//Debug.Log("Mouse is no longer pressing " + gameObject.name);
	}

	public virtual void OnMouseRightRelease()
	{
		//Debug.Log("Mouse is no longer pressing " + gameObject.name);
	}

	public virtual void OnMouseDragging()
	{
		//Debug.Log("Mouse is dragging " + gameObject.name);
	}

}