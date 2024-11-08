using UnityEngine;


	public class DestroyMeEvent : MonoBehaviour
	{
		public GameObject transformToDestroy;

		public void DestroyMe()
		{
			Destroy(transformToDestroy);
		}
	}
